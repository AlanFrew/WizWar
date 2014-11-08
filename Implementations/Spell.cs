using System;
using System.Collections.Generic;
using System.Linq;
using Library;

namespace WizWar1 {
class Spell : Cardable, ISpell {
    //public string Name { get; set; }

    //public string Description { get; set; }

    public List<TargetTypes> ValidTargetTypes { get; set; }
 
    public List<SpellType> ValidCastingTypes { get; private set; }

    protected Wizard caster;
    public Wizard Caster {
        get {
            return caster;
        }
        set {
            if (caster != null) {
                foreach (Effect e in EffectsWaiting) {
                    if (e.Caster == caster) {
                        e.Caster = value;
                    }
                }
            }
            caster = value;
            Controller = value;
        }
    }

    public bool RequiresLoS { get; set; }

    protected ITarget spellTarget;
    public virtual ITarget SpellTarget {
        get {
            return spellTarget;
        }
        set {
            //make sure the individual effects switch targets if the whole spell does
            foreach (Effect e in EffectsWaiting) {
                if (e.target == spellTarget) {
                    e.target = value;
                }
            }

            spellTarget = value;

            switch(spellTarget != null ? spellTarget.ActiveTargetType : TargetTypes.None) {
            case TargetTypes.Effect:
            case TargetTypes.Spell:
                ActiveSpellType = SpellType.Counteraction;
                break;
            case TargetTypes.Wizard:
                if (spellTarget != Caster) {
                    ActiveSpellType = SpellType.Attack;
                }
                else if (Caster != GameState.ActivePlayer) {
                    ActiveSpellType = SpellType.Counteraction;
                }
                else {
                    ActiveSpellType = SpellType.Neutral;
                }
                break;
            case TargetTypes.None:
            case TargetTypes.Square:
            case TargetTypes.WallSpace:
                ActiveSpellType = SpellType.Neutral;
                break;
            case TargetTypes.Creation:
                if (IsValidCastingType(SpellType.Attack)) {
                    ActiveSpellType = SpellType.Attack;
                }
                else {
                    ActiveSpellType = SpellType.Neutral;
                }
                break;   
            case TargetTypes.Undef:
                throw new NotFoundException();
            default:
                throw new UnreachableException();
            }
        }
    }

    public SpellType ActiveSpellType { get; private set; }

    //public bool AcceptsNumber { get; set; }

    //public int CardValue { get; set; }
    
    public List<Marker> Markers { get; set; }

    public List<Effect> EffectsWaiting { get; set; }

    public List<Effect> StatusEffects = new List<Effect>();

    public Spell() {
        RequiresLoS = true;
        //AcceptsNumber = false;
        //CardValue = 1;
        activeTargetType = TargetTypes.Spell;
        ValidTargetTypes = new List<TargetTypes>();
        ValidCastingTypes = new List<SpellType>();
        EffectsWaiting = new List<Effect>();
        Markers = new List<Marker>();
    }

    public Spell RecursiveCopy() {
        var result = (Spell)MemberwiseClone();
        result.Markers = Markers.RecursiveCopyList();
        result.EffectsWaiting = EffectsWaiting.RecursiveCopyList();
        result.StatusEffects = StatusEffects.RecursiveCopyList();
        return result;
    }

    public bool IsValidCastingType(SpellType tSpellType) {
        foreach (SpellType s in ValidCastingTypes) {
            if (tSpellType == s) {
                return true;
            }
        }

        return false;
    }

    public bool IsOnlyValidCastingType(SpellType tSpellType) {
        bool otherFound = false;
        bool validFound = false;
        foreach (SpellType s in ValidCastingTypes) {
            if (tSpellType == s) {
                validFound = true;
            }
            else {
                otherFound = true;
            }
        }
        if (otherFound == false && validFound) {
            return true;
        }

        return false;
    }

    //perhaps I should only have one version on this function (no parent) because there is a danger of duplicate events
    public bool IsValidTargetParent(ITarget tTarget) {
        if (IsValidTargetType(tTarget.ActiveTargetType) == false) {
            return false;
        }

        if (IsValidTarget(tTarget) == true) {
            if (GameState.InitialUltimatum(new TargetingEvent(tTarget, Controller)) == Redirect.Proceed) {
                return true;
            }
        }
        return false;
    }

    public virtual bool IsValidTarget(ITarget tTarget) {
        foreach (TargetTypes t in ValidTargetTypes) {
            if (tTarget.ActiveTargetType == t) {
                return true;
            }
        }

        return false;
    }

    public virtual bool IsValidTargetType(TargetTypes tTargetType) {
        foreach (TargetTypes t in ValidTargetTypes) {
            if (t == tTargetType) {
                return true;
            }
        }

        return false;
    }

    public void OnRun() {
        OnResolution();
        foreach (Effect e in EffectsWaiting) {
            if (e.markers.Any(m => m is DurationBasedMarker)) {
                GameState.PushEffect(e);
            }
            e.OnRun();
        }
    }

    public virtual void OnResolution() {
        //empty
    }

    public void OnCast() {
        //GameState.eventDispatcher.notify(new CastEvent(this));        //think this is done elsewhere
        OnChildCast();
        //if (AcceptsNumber == true) {
        //    if (CardValue == 0) {
         //       CardValue = 1;
        //    } 
        //}
    }

    public virtual void OnChildCast() {
        throw new NotImplementedException();
    }

    public override string ToString() {
        if (Caster != null) {
            return Name + " cast by " + Caster.Name;
        }

        return Name;
    }

    public double ShotDirection { get; set; }

    public object Aimable {
        get {
            return this;
        }
    }

    public ITarget Target {
        get {
            return SpellTarget;
        }
        set {
            SpellTarget = value;
        }
    }

    public Wizard Controller { get; set; }
}
}
