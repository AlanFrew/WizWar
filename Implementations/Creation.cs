using System;
using System.Drawing;

namespace WizWar1 {

class Creation : Locatable, ICreation {
    protected String name;
    public String Name {
        get {
            return name;
        }
        set {
            name = value;
        }
    }

    public Targetable TargetPiece { get; set; }

    public Wizard Creator { get; set; }

    protected bool isCreation;
    public bool IsCreation {
        get {
            return isCreation;
        }
    }
   

    public TargetTypes TargetType { get; set; }

    protected Creation() {
        TargetType = TargetTypes.Creation;
        activeTargetType = TargetTypes.Creation;
    }

    //public override void OnResolution() {
    //    if (SpellTarget is Square) {
    //        X = (SpellTarget as Square).X;
    //        y = (SpellTarget as Square).Y;
    //    }
    //    else {
    //        throw new UnreachableException();
    //    }

    //    Location = GameState.BoardRef.At(X, Y);
    //    BecomeCreation();
    //    location.creationsHere.Add(this as ICreation);

    //}

    public override string ToString() {
        return GetType().Name;
    }

    public static T New<T>(MakeCreationEffect<T> tEffect, T result) where T : Creation {
        result.Creator = tEffect.Caster;
        result.activeTargetType = TargetTypes.Creation;
        if (tEffect.target is Square) {
            result.Location = tEffect.target as Square;
        }
        else {
            throw new NotSupportedException();
        }
        return result;
    }

    public void Initialize(Wizard tCaster, Square tLocation) {
        Creator = tCaster;
        Location = tLocation;
    }

    public void Destroy(DestroyEffect destroyEffect) {
        GameState.Creations.Remove(this);
    }

    public virtual Image MyImage { get; set; }
}
}
