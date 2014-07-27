using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DestroyItemEffect : Effect {
    public override void OnRunChild() {
        if ((target as IItem).Carrier != null) {
            (target as IItem).Carrier.loseItem(target as IItem);
        }
        else {
            (target as IItem).Location = null;
        }
    }
}
}
