using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class WizardBladeItem : Item {
    public WizardBladeItem() {
        itemTargetTypes.Add(TargetTypes.Wizard);
        itemTargetTypes.Add(TargetTypes.Wall);
        itemTargetTypes.Add(TargetTypes.Creation);
    }

    public override void OnActivationChild() {
        throw new NotImplementedException();
    }
}
}
