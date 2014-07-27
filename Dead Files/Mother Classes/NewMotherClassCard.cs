using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
abstract partial class NewMotherClass {
    protected String name;
    public String Name {
        get {
            return name;
        }
        set {
            name = value;
        }
    }

    public override String ToString() {
        return name;
    }
}
}
