using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DropObjectEffect : Effect {
    public override void OnRunChild() {
        (target as IItem).Carrier.dropItem(target as IItem);
    }
}
}
