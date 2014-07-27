using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class LargeRock : Item {
    public LargeRock() {
        Name = "Large Rock";
        itemTargetTypes.Add(TargetTypes.Wizard);
        itemTargetTypes.Add(TargetTypes.Wall);
        itemTargetTypes.Add(TargetTypes.Creation);
    }

    public override void OnActivationChild() {
        Carrier.myUI.State = UIState.FindingTarget;
        throw new NotImplementedException();
    }
}
}
