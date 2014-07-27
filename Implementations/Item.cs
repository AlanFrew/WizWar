using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Item : Locatable, IItem {
    protected String name;
    public String Name {
        get {
            return name;
        }
        set {
            name = value;
        }
    }

    protected Wizard carrier;
    public Wizard Carrier {
        get {
            return carrier;
        }
        set {
            if (carrier == value) {
                return;
            }

            if (value == null) {
                if (carrier != null) {
                    carrier.loseItem(this);
                }

                //location = GameState.BoardReference.At(value.X, value.Y);
            }
                
            carrier = value;
        }
    }

    protected Square location;
    public Square Location {
        get {
            if (Carrier != null) {
                return GameState.BoardRef.At(Carrier.X, Carrier.Y);
            }

            return location;
        }
        set {
            if (location == value) {
                return;
            }

            if (carrier == null) {
                if (location != null) {
                    location.RemoveItem(this);
                }
                value.AddItem(this);
                location = value;
                x = location.X;
                y = location.Y;
            }

            //can't use this property on a carried item; must be dropped() or lost() first
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

    protected List<TargetTypes> itemTargetTypes;

    protected List<Effect> effectsWaiting;
    public List<Effect> EffectsWaiting {
        get {
            return effectsWaiting;
        }
        set {
            effectsWaiting = value;
        }
    }

    protected ITarget itemTarget;
    public ITarget ItemTarget {
        get {
            return itemTarget;
        }
        set {
            itemTarget = value;
        }
    }

    public Item() {
        itemTargetTypes = new List<TargetTypes>();
        requiresLoS = false;
    }

    public virtual bool IsValidTargetTypeForItem(TargetTypes tTargetType) {
        foreach (TargetTypes t in itemTargetTypes) {
            if (tTargetType == t) {
                return true;
            }
        }

        return false;
    }

    public virtual bool IsValidTargetForItem(ITarget tTarget) {
        foreach (TargetTypes t in itemTargetTypes) {
            if (tTarget.ActiveTargetType == t) {
                return true;
            }
        }

        return false;
    }

    public void OnGainParent(Wizard tHolder) {
        OnGainChild(tHolder);
        Carrier = tHolder;
    }

    public virtual void OnGainChild(Wizard tHolder) {
        //empty by default
    }

    public void OnLossParent(Wizard tDropper) {
        OnLossChild(tDropper);
        Carrier = null;
    }

    public virtual void OnLossChild(Wizard tDropper) {
        //empty by default
    }

    public void OnActivationParent() {
        OnActivationChild();
    }

    public virtual void OnActivationChild() {
        //empty by default
    }

    public void OnResolutionParent() {
        OnResolutionChild();
    }

    public virtual void OnResolutionChild() {
        //empty by default
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

    public override string ToString() {
        return GetType().Name;
    }
}
}
