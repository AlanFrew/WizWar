using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Card : Targetable, ICard {
    //not sure how this should interact with CardExtension yet
    //public String Name {
    //    get {
    //        return this.GetName();
    //    }
    //    set {
    //        this.SetName(value);
    //    }
    //}
    
    protected String name;
    public String Name {
        get {
            return name;
        }
        set {
            name = value;
        }
    }

    public Card() {
        //name = this.GetName();
    }

    public override String ToString() {
        //return this.GetName();
        return name;
    }

    public static ICard NewCard<T>() where T : ICard, new() {
        return new T();
    }

    public static ICard NewCard(Type t) {
        if (t.IsSubclassOf(typeof(Card))) {
            return (System.Activator.CreateInstance(t) as Card);
        }

        throw new NotSupportedException();
    }

    public static ICard NewCard(string name) {
        //maybe add some error checking here
        //I think this creates a card based on the name, given as a string
        System.Runtime.Remoting.ObjectHandle o = System.Activator.CreateInstance(null, name);
        return (o.Unwrap() as Card);
    }
}
}
