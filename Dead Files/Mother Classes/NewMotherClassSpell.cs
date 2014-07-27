using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
abstract partial class NewMotherClass {
    protected List<TargetTypes> validTargets;
    protected List<SpellType> validCastingTypes;

    protected Wizard caster;
    public Wizard Caster {
        get {
            return caster;
        }
        set {
            caster = value;
        }
    }

    protected ITarget target;
    public ITarget Target {
        get {
            return target;
        }
        set {
            target = value;
        }
    }

    protected SpellType activeSpellType;
    public SpellType ActiveSpellType {
        get {
            return activeSpellType;
        }
        set {
            activeSpellType = value;
        }
    }

    public bool IsValidCastingType(SpellType tSpellType) {
        foreach (SpellType s in validCastingTypes) {
            if (tSpellType == s) {
                return true;
            }
        }

        return false;
    }

    public bool IsValidSpellTarget(ITarget tTarget) {
        foreach (TargetTypes t in validTargets) {
            if (tTarget.ActiveTargetType == t) {
                return true;
            }
        }

        return false;
    }

    public bool IsValidSpellTargetType(TargetTypes tTargetType) {
        foreach (TargetTypes t in validTargets) {
            if (t == tTargetType) {
                return true;
            }
        }

        return false;
    }

    public virtual void OnResolution() {
        if (ValidSpell == false) {
            throw new InvalidTypeException();
        }

        if (ValidCreation) {
            if (Target is Square) {
                X = (Target as Square).X;
                y = (Target as Square).Y;
            }
            else {
                throw new NotImplementedException();
            }

            Square = GameState.BoardRef.At(X, Y);
            BecomeCreation();
            Square.creationsHere.Add(this as ICreation);
        }

    }
}
}
