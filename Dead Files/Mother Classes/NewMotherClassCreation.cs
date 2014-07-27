using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
abstract partial class NewMotherClass {
    public Wizard Creator {
        get {
            return caster;
        }
    }
}
}
