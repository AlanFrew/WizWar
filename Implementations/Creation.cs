using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {

class Creation : Targetable, ICreation {
    protected String name;
    public String Name {
        get {
            return name;
        }
        set {
            name = value;
        }
    }

    protected Targetable targetPiece;
    public Targetable TargetPiece {
        get {
            return targetPiece;
        }
        set {
            targetPiece = value;
        }
    }

    protected Wizard creator;
    public Wizard Creator {
        get { return creator; }
        set { creator = value; }
    }

    protected bool isCreation;
    public bool IsCreation {
        get {
            return isCreation;
        }
    }
    
    #region Duplicated Code
    #region From Locatable

    protected double x;
    public double X {
        get {
            return x;
        }
        set {
            x = value;
        }
    }

    protected double y;
    public double Y {
        get {
            return y;
        }
        set {
            y = value;
        }
    }

    public Square Location {
        get {
            return GameState.BoardRef.At(x, y);
        }
        set {
            x = value.X;
            y = value.Y;
        }
    }

    #endregion
    #endregion

    protected TargetTypes targetType;
    public TargetTypes TargetType {
        get {
            return targetType;
        }
        set {
            TargetType = value;
        }
    }

    protected Creation() {
        targetType = TargetTypes.Creation;
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

    public void BecomeCreation() {
        if (x < 0 || y < 0 || Location == null) {
            throw new InvalidTypeException();
        }

        isCreation = true;
    }

    public override string ToString() {
        return this.GetType().Name;
    }

    public static T New<T>(MakeCreationEffect<T> tEffect, T result) where T : Creation {
        result.creator = tEffect.Caster;
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
        creator = tCaster;
        Location = tLocation;
    }
}
}
