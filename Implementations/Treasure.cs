using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Treasure : Item, ITreasure {
    public Treasure(Wizard tOwner) {
        owner = tOwner;
        itemTargetTypes.Add(TargetTypes.None);
        //not valid spell or card
    }

    private Wizard owner;
    public Wizard Owner {
        get {
            return owner;
        }
        set {
            owner = value;
        }
    }

    public override string ToString() {
        return owner.Name + "'s Treasure";
    }
}
}
