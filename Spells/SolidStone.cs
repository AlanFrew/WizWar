using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class SolidStone : Spell {
    public SolidStone() {
        Name = "Fill Square with Stone";
        //itemName = "Solid Stone";
        validTargetTypes.Add(TargetTypes.Square);
    }
}
}
