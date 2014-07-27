using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Targetable : ITarget {
    //protected List<TargetType> myTargetTypes;
    protected TargetTypes activeTargetType;
    public TargetTypes ActiveTargetType {
        get {
            return activeTargetType;
        }
    }

    public bool IsTargetableAs(TargetTypes tTargetType) {
        switch (tTargetType) {
            case TargetTypes.Wall:
                return this is Wall;
            case TargetTypes.Spell:
                return this is Spell;
            case TargetTypes.Creation:
                return this is Creation;
            case TargetTypes.Card:
                return this is Card;
            default:
                return false;
        }
    }

    public void BecomeTarget(TargetTypes tActiveTargetType) {
        activeTargetType = tActiveTargetType;
    }

    public virtual void RanChild(Effect tEffect) {
        //empty by default
    }

    public void RanParent(Effect tEffect) {

    }
}
}
