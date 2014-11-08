using System;
using System.Runtime.Remoting;

namespace WizWar1 {
class Card<T> : Targetable, ICard where T : class, ICardable, new()  {
    private T wrappedCard;

    public Card() {
        WrappedCard = new T();

        (WrappedCard as Cardable).OriginalCard = this;
    }

    public ICardable WrappedCard { get { return wrappedCard;} set { wrappedCard = value as T; } }

    public String Name {
        get { return wrappedCard.Name; }
        set { wrappedCard.Name = value; }
    }

    public override string ToString() {
        return wrappedCard.Name;
    }

    public string Description {
        get { return wrappedCard.Description; }
        set { wrappedCard.Description = value; }
    }

    public static ICard NewCard<U>() where U : ICard, new() {
        return new U();
    }

    //public static ICard NewCard(Type t) {
    //    if (t.IsSubclassOf(typeof(Card))) {
    //        return (Activator.CreateInstance(t) as Card);
    //    }

    //    throw new NotSupportedException();
    //}

    public static ICard NewCard(string name) {
        //maybe add some error checking here
        //I think this creates a card based on the name, given as a string
        ObjectHandle o = Activator.CreateInstance(null, name);
        return (o.Unwrap() as ICard);
    }

    public ICard RecursiveCopy() {
        return (Card<T>)MemberwiseClone();
    }
}
}
