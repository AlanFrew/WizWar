using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;

namespace WizWar1 {
class Spell : Card, ISpell, ICopiable<Spell>, IStackable {
    protected List<TargetTypes> validTargetTypes;
    protected List<SpellType> validCastingTypes;
    internal List<SpellType> ValidCastingTypes {
        get {
            return validCastingTypes;
        }
    }

    protected Wizard caster;
    public Wizard Caster {
        get {
            return caster;
        }
        set {
            if (caster != null) {
                foreach (Effect e in effectsWaiting) {
                    if (e.Caster == caster) {
                        e.Caster = value;
                    }
                }
            }
            caster = value;
        }
    }

    protected bool requiresLoS;
    public bool RequiresLoS {
        get {
            return requiresLoS;
        }
        set {
            requiresLoS = value;
        }
    }

    protected ITarget spellTarget;
    public virtual ITarget SpellTarget {
        get {
            return spellTarget;
        }
        set {
            //make sure the individual effects switch targets if the whole spell does
            if (spellTarget != null) {
                foreach (Effect e in effectsWaiting) {
                    if (e.target == spellTarget) {
                        e.target = value;
                    }
                }
            }

            spellTarget = value;

            switch(spellTarget.ActiveTargetType) {
            case TargetTypes.Spell:
                activeSpellType = SpellType.Counteraction;
                break;
            case TargetTypes.Wizard:
                if (spellTarget != Caster) {
                    activeSpellType = SpellType.Attack;
                }
                else if (Caster != GameState.ActivePlayer) {
                    activeSpellType = SpellType.Counteraction;
                }
                else {
                    activeSpellType = SpellType.Neutral;
                }
                break;
            case TargetTypes.Square:
                activeSpellType = SpellType.Neutral;
                break;
            case TargetTypes.None:
                activeSpellType = SpellType.Item;
                break;
            case TargetTypes.WallSpace:
                activeSpellType = SpellType.Neutral;
                break;
            case TargetTypes.Creation:
                if (IsValidCastingType(SpellType.Attack)) {
                    activeSpellType = SpellType.Attack;
                }
                else {
                    activeSpellType = SpellType.Neutral;
                }
                break;
            case TargetTypes.Effect:
                activeSpellType = SpellType.Counteraction;
                break;
            case TargetTypes.Undef:
                throw new NotFoundException();
            default:
                throw new UnreachableException();
            }
        }
    }

    protected SpellType activeSpellType = SpellType.Undef;
    public SpellType ActiveSpellType {
        get {
            return activeSpellType;
        }
    }

    //protected bool isSpell;
    //public bool IsSpell {
    //    get {
    //        return isSpell;
    //    }
    //}

    protected bool acceptsNumber;
    public bool AcceptsNumber {
        get {
            return acceptsNumber;
        }
    }

    protected int cardValue;
    public int CardValue {
        get {
            return cardValue;
        }
        set {
            cardValue = value;
        }
    }

    protected List<Marker> markers = new List<Marker>();
    public List<Marker> Markers {
        get {
            return markers;
        }
        set {
            markers = value;
        }
    }

    protected List<Effect> effectsWaiting = new List<Effect>();
    public List<Effect> EffectsWaiting  {
        get {
                return effectsWaiting;
        }
        set {
            effectsWaiting = value;
        }
    }

    public List<Effect> StatusEffects = new List<Effect>();

    public Spell() {
        requiresLoS = true;
        acceptsNumber = false;
        cardValue = 1;
        activeTargetType = TargetTypes.Spell;
        validTargetTypes = new List<TargetTypes>();
        validCastingTypes = new List<SpellType>();
    }

    public Spell RecursiveCopy() {
        //Spell temp = new Spell();
        //temp.Caster = Caster;
        //temp.SpellTarget = SpellTarget;
        //temp.activeSpellType = activeSpellType;
        ////temp.isSpell = IsSpell;
        //temp.acceptsNumber = AcceptsNumber;
        //temp.CardValue = CardValue;
        //temp.Markers = Markers.RecursiveCopyList();
        //temp.EffectsWaiting = EffectsWaiting.RecursiveCopyList();
        //temp.StatusEffects = StatusEffects.RecursiveCopyList();

        //return temp;
        Spell result = (Spell)this.MemberwiseClone();
        result.Markers = Markers.RecursiveCopyList();
        result.EffectsWaiting = EffectsWaiting.RecursiveCopyList();
        result.StatusEffects = StatusEffects.RecursiveCopyList();
        return result;
    }

    public bool IsValidCastingType(SpellType tSpellType) {
        foreach (SpellType s in validCastingTypes) {
            if (tSpellType == s) {
                return true;
            }
        }

        return false;
    }

    public bool IsOnlyValidCastingType(SpellType tSpellType) {
        bool otherFound = false;
        bool validFound = false;
        foreach (SpellType s in validCastingTypes) {
            if (tSpellType == s) {
                validFound = true;
            }
            else {
                otherFound = true;
            }
        }
        if (otherFound == false && validFound == true) {
            return true;
        }

        return false;
    }

    //perhaps I should only have one version on this function (no parent) because there is a danger of duplicate events
    public bool IsValidSpellTargetParent(ITarget tTarget, Wizard tCaster) {
        if (IsValidSpellTargetType(tTarget.ActiveTargetType) == false) {
            return false;
        }

        if (IsValidSpellTarget(tTarget, tCaster) == true) {
            TargetingEvent a = new TargetingEvent(tTarget); //this structure might be deprecated
            if (GameState.InitialUltimatum(a) == Redirect.Proceed) {
                return true;
            }
        }
        return false;
    }

    public virtual bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
        foreach (TargetTypes t in validTargetTypes) {
            if (tTarget.ActiveTargetType == t) {
                return true;
            }
        }

        return false;
    }

    public virtual bool IsValidSpellTargetType(TargetTypes tTargetType) {
        foreach (TargetTypes t in validTargetTypes) {
            if (t == tTargetType) {
                return true;
            }
        }

        return false;
    }

    public void OnRun() {
        

        //bool WasEffectPushed = false;
        //sending NewEffect code to OnCast()
        //foreach (Effect e in EffectsWaiting) {
        //    GameState.NewEffect(e);
        //    //WasEffectPushed = true;
        //}

        //if (WasEffectPushed) {
        //    GameState.eventDispatcher.notify(new NewEffectEvent());
        //}

        OnResolution();
        foreach (Effect e in EffectsWaiting) {
            e.OnRun();
        }
    }

    public virtual void OnResolution() {
        //empty
    }

    public void OnCast() {
        //GameState.eventDispatcher.notify(new CastEvent(this));        //think this is done elsewhere
        OnChildCast();
        if (AcceptsNumber == true) {
            if (CardValue == 0) {
                CardValue = 1;
            } 
        }

        //foreach (Effect e in effectsWaiting) {
        //    GameState.NewEffect(e);
        //}
    }

    public virtual void OnChildCast() {
        throw new NotImplementedException();
    }

    //moved to ITarget
    //public void Ran(Effect tEffect) {
    //    //empty by default
    //}

    //public void BecomeSpell() {
    //    if (caster == null || activeTargetType == TargetTypes.Undef || spellTarget == null) {
    //        throw new NotReadyException();
    //    }

    //    isSpell = true;
    //}

    public override string ToString() {
        if (Caster != null) {
            return Name + " cast by " + Caster.Name;
        }

        return Name;
    }

    private double shotDirection;
    public double ShotDirection {
        get {
            return shotDirection;
        }
        set {
            shotDirection = value;
        }
    }
}
}
