using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class ItemCard : Card {
    protected Wizard creator;
    public Wizard Creator {
        get {
            return creator;
        }
        set {
            creator = value;
        }
    }

    protected ITarget playTarget;
    public virtual ITarget PlayTarget {
        get {
            return playTarget;
        }
        set {
            playTarget = value;
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

    protected List<Effect> effectsWaiting = new List<Effect>();
    public List<Effect> EffectsWaiting {
        get {
            return effectsWaiting;
        }
        set {
            effectsWaiting = value;
        }
    }

    public void OnPlayParent() {
        OnPlayChild();
    }

    public virtual void OnPlayChild() {
        //GameState.eventDispatcher.notify(new ItemRevealEvent(this));
        //Creator.giveItem(ItemCard.CreateItem<BloodStoneItem>());
    }

    protected static IItem CreateItem<T>() where T : Item, new() {
        return new T();
    }
}
}
