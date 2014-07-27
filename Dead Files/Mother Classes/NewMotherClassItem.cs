using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
abstract partial class NewMotherClass {
    protected Wizard carrier;
    public Wizard Carrier {
        get {
            return carrier;
        }
        set {
            carrier = value;
        }
    }

    protected List<TargetTypes> itemTargetTypes;

    public bool IsValidTargetTypeForItem(TargetTypes tTargetType) {
        foreach (TargetTypes t in itemTargetTypes) {
            if (tTargetType == t) {
                return true;
            }
        }

        return false;
    }

    public bool IsValidTargetForItem(ITarget tTarget) {
        foreach (TargetTypes t in itemTargetTypes) {
            if (tTarget.ActiveTargetType == t) {
                return true;
            }
        }

        return false;
    }

    public virtual void OnUse() {
        throw new InvalidTypeException();
    }
}
}
