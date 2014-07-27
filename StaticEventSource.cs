using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WizWar1 {
class StaticEventSource<T> where T : Event {
    //helper class; represents the listeners while dodging template requirements
    private class ListenerRewrite {
        internal Type listenerType;
        public void OnEvent(Event tEvent){}
    }

    //the listeners for this event type
    private List<ListenerRewrite> listeners = new List<ListenerRewrite>();

    //the dispatchers for subtypes; the T doesn't really mean anything for them
    private List<StaticEventSource<T>> childSources;

    //needed for child dispatchers to know their types (dodging template requirements)
    //doesn't need property wrapper because no other class can get a handle to a child
    private Type myType;
    internal Type MyType {
        get {
            return myType;
        }
    }

    //used by client code to create the master dispatcher
    public StaticEventSource() {
        //empty
    }

    //for child dispatchers 
    private StaticEventSource(Type tMyType) {
        myType = tMyType;
    }

    public void notify(T tEvent) {
        if (tEvent.GetType() == myType) {
            foreach (ListenerRewrite l in listeners) {
                string s = "OnEvent";
                MethodInfo m = l.listenerType.GetMethod(s);
                T[] t = new T[1];
                t[0] = tEvent;
                m.Invoke(this, t);
            }
        }
        else {
            foreach (StaticEventSource<T> es in childSources) {
                if (tEvent.GetType().IsSubclassOf(es.myType)) {
                    es.notify(tEvent);
                    return;
                }
            }
        }
    }

    public void Reset() {
        //every source can expect a listener, by there will always be sources with no children
        listeners = new List<ListenerRewrite>();
        childSources = null;
    }

    //an IListener that Registers twice will get two notifications per event
    public void Register<X, A>() where X : T where A : IListener<X, T> {
        //the listener is either for this type or for a subtype
        //invalid types in a call result in a compiler error

        //check for exact type match
        if (typeof(X) == myType) {
            //this is the right spot

            //cast to keep X from having to appear in the main templete
            //ListenerHelper is written so the cast always succeeds
            ListenerRewrite newListener = new ListenerRewrite();
            newListener.listenerType = typeof(A);
            listeners.Add(newListener);
        }
        else {
            //X is a proper subtype of this EventSource
            //check for a child EventSource that already handles this type

            //these may be required if listeners are not registered in strict parent->child order
            StaticEventSource<T> newIntermediate = null;
            StaticEventSource<T> foundSuperclass = null;

            if (childSources != null) {
                foreach (StaticEventSource<T> child in childSources) {
                    if (typeof(X).IsSubclassOf(child.myType)) {
                        //listener goes in this branch somewhere
                        foundSuperclass = child;
                        //can break here because any orphans would have been caught already
                        break;
                    }
                    else if (child.myType.IsSubclassOf(typeof(X))) {
                        //the new type is a parent of what is already here. Must rejigger
                        newIntermediate = new StaticEventSource<T>(typeof(X));

                        //switch the old child from current level to child of intermediary
                        childSources.Remove(child);
                        newIntermediate.childSources.Add(child);

                        //the loop now continues, soaking up any other children of the new listener
                    }
                }
            }
            else {
                childSources = new List<StaticEventSource<T>>();
            }

            //foundSuperclass OR newIntermediate is null
            if (foundSuperclass != null) {
                //the new listener is a child only of the current dispatcher
                foundSuperclass.Register<X, A>();
            }
            else {
                if (newIntermediate != null) {
                    //the new listener's source has its own children; must be intermediary
                    newIntermediate.Register<X, A>();
                }
                else {
                    //no children; not a child of a child; must be direct child
                    newIntermediate = new StaticEventSource<T>(typeof(X));
                }

                childSources.Add(newIntermediate);
            }
        }
    }

    public void Deregister<X, A>() where X : T where A : IListener<X, T> {
        try {
            DeregisterCandy<X, A>();
        }
        catch (NotFoundException) {
            //oh well
        }
    }

    private bool DeregisterCandy<X, A>() where X : T where A : IListener<X, T> {
        //tListener is either a listener of this type or a subtype, or it is not registered at all

        if (typeof(X) == myType) {
            foreach (ListenerRewrite l in listeners) {
                if (l.listenerType == typeof(A)) {
                    listeners.Remove(l);
                    return true;
                }
            }

            //never gonna find it; exit quickly
            throw new NotFoundException();
        }

        //must be a subtype, or not exist
        foreach (StaticEventSource<T> es in childSources) {
            if (typeof(X).IsSubclassOf(es.myType)) {
                if (es.DeregisterCandy<X, A>() == true) {
                    return true;
                }

                throw new NotFoundException();
            }
        }

        //the listener has no eventsource of the corresponding type. It must not be registered
        throw new NotFoundException();
    }
}
}

