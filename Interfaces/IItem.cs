using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
interface IItem : ILocatable {
    Wizard Carrier {
        get;
        set;
    }

    ITarget ItemTarget {
        get;
        set;
    }

    Square Location {
        get;
        set;
    }

    bool RequiresLoS {
        get;
        set;
    }

    double ShotDirection {
        get;
        set;
    }

    bool IsValidTargetTypeForItem(TargetTypes tTargetType);

    bool IsValidTargetForItem(ITarget tTarget);

    void OnGainParent(Wizard tHolder);

    void OnLossParent(Wizard tDropper);

    void OnActivationChild();

    void OnActivationParent();

    void OnResolutionChild();

    void OnResolutionParent();
}
}