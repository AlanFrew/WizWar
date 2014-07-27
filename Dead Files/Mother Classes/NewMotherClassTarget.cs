using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
abstract partial class NewMotherClass {
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
                return isWall;
            case TargetTypes.Spell:
                return isSpell;
            case TargetTypes.Creation:
                return isCreation;
            case TargetTypes.Card:
                return isCard;
            default:
                return false;
        }
    }
}
}
