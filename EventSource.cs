using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;
using Library;

namespace WizWar1 {
class EventSource<T> where T : Event {
    //helper class; represents the listeners while dodging template requirements
    abstract class ListenerRewrite {
        abstract public void OnEvent(Event tEvent);
    }

    //the listeners for this event type
    //private List<ListenerRewrite> listeners = new List<ListenerRewrite>(); //trying something crazy
    private RobustList<Object> listeners = new RobustList<Object>();

    //the dispatchers for subtypes; the T doesn't really mean anything for them
    private List<EventSource<T>> childSources;

    //used in order to avoid a "collection modified" exception when a listener deregisters itself in OnEvent()
    //private List<Object> deregisteredListeners;

    //needed for child dispatchers to know their types (dodging template requirements)
    //doesn't need property wrapper because no other class can get a handle to a child
    private Type myType;
    internal Type MyType {
        get {
            return myType;
        }
    }

    //used by client code to create the master dispatcher
    //Probably shouldn't allow creation of an EventSource with no type?
    public EventSource() : this(typeof(Event)) {
        //empty
    }

    //for creating child dispatchers 
    private EventSource(Type tMyType) {
        myType = tMyType;
    }

    public void Notify<X>(X tEvent, IListener<X, T> foo = null) where X : T {
        if (tEvent.GetType() == myType) {
            foreach (Object l in listeners) {
                foreach (Type t in l.GetType().GetInterfaces()) {
                    Type[] temp = t.GetGenericArguments();
                    if (temp.Count() > 0 && temp[0] == tEvent.GetType()) {
                        MethodInfo mi = t.GetMethod("OnEvent", new Type[] { tEvent.GetType() });
                        mi.Invoke(l, new object[] { tEvent });
                    }
                }
            }
        }
        else {
            foreach (EventSource<T> es in childSources) {
                if (tEvent.GetType().IsSubclassOf(es.myType) || tEvent.GetType() == es.myType) {
                    es.Notify(tEvent);
                    return;
                }
            }
        }
    }

    public void Reset() {
        //every source can expect a listener, by there will always be sources with no children
        listeners = new RobustList<Object>();
        childSources = null;
    }

    //an IListener that Registers twice will get two notifications per event
    public void Register<X>(IListener<X, T> tListener) where X : T {
        //the listener is either for this type or for a subtype
        //invalid types in a call result in a compiler error

        //check for exact type match
        if (typeof(X) == myType) {
            //this is the right spot

            //cast to keep X from having to appear in the main templete
            //ListenerHelper is written so the cast always succeeds
            Object newListener = (Object)tListener;
            listeners.Add(newListener);
        }
        else {
            //X is a proper subtype of this EventSource
            //check for a child EventSource that already handles this type

            //these may be required if listeners are not registered in strict parent->child order
            EventSource<T> newIntermediate = null;
            EventSource<T> foundSuperclass = null;

            if (childSources != null) {
                foreach (EventSource<T> child in childSources) {
                    if (typeof(X).IsSubclassOf(child.myType)) {
                        //listener goes farther down in this branch somewhere
                        foundSuperclass = child;
                        //can break here because any orphans would have been caught already
                        break;
                    }
                    else if (child.myType.IsSubclassOf(typeof(X))) {
                        //the new type is a parent of what is already here. Must rejigger
                        newIntermediate = new EventSource<T>(typeof(X));

                        //switch the old child from current level to child of intermediary
                        childSources.Remove(child);
                        newIntermediate.childSources.Add(child);

                        //the loop now continues, soaking up any other children of the new listener
                    }
                    else if (typeof(X) == child.myType) {
                        //the new listener is a direct child of the current EventSource and is the same type as a previous listener
                        child.Register(tListener);
                        return; //untested code
                    }
                }
            }
            else {
                childSources = new List<EventSource<T>>();
            }

            //foundSuperclass OR newIntermediate is null
            if (foundSuperclass != null) {                    
                //the new listener is a child of a child
                foundSuperclass.Register<X>(tListener);
            }
            else {
                if (newIntermediate != null) {
                    //the new listener's source has its own children; must be intermediary
                    //newIntermediate.Register<X>(tListener); //moved down
                }
                else {
                    //no children; not a child of a child; must be direct child
                    newIntermediate = new EventSource<T>(typeof(X));
                }

                newIntermediate.Register<X>(tListener);
                childSources.Add(newIntermediate);
            }
        }
    }

    public void Deregister<X>(IListener<X, T> tListener) where X : T {
        try {
            DeregisterInner<X>(tListener);
        }
        catch (NotFoundException) {
            //oh well
        }
    }

    private bool DeregisterInner<X>(IListener<X, T> tListener) where X : T {
        //tListener is either a listener of this type or a subtype, or it is not registered at all

        if (typeof(X) == myType) {
            if (listeners.Remove(tListener) == true) {
                return true;
            }

            //never gonna find it; exit quickly
            throw new NotFoundException();
        }
        
        //must be a subtype, or not exist
        foreach (EventSource<T> es in childSources) {
            if (typeof(X).IsSubclassOf(es.myType) || typeof(X) == es.myType) {
                if (es.DeregisterInner<X>(tListener) == true) {
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
