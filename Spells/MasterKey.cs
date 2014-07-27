using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class MasterKey : Spell {
    public MasterKey() {
        Name = "Master Key";
        validTargetTypes.Add(TargetTypes.None);
    }
}
}
