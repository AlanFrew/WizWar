using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class MagicWall : Spell {
    public MagicWall(Wizard tCreator, Square tLocation) {
        Name = "Create Wall";
        validTargetTypes.Add(TargetTypes.WallSpace);
    }
}
}