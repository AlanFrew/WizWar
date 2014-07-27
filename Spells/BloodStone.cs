using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class BloodStone : ItemCard {
    public BloodStone() {
        Name = "BloodStone";
    }

    public override void OnPlayChild() {
        (PlayTarget as Wizard).giveItem(ItemCard.CreateItem<BloodStoneItem>());
    }
}
}
