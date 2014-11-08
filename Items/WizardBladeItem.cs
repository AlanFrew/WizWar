using System;

namespace WizWar1 {
class WizardBladeItem : NumberedItem {
    public WizardBladeItem() {
        ValidTargetTypes.Add(TargetTypes.Wizard);
        ValidTargetTypes.Add(TargetTypes.Wall);
        ValidTargetTypes.Add(TargetTypes.Creation);
    }

    public override void OnActivationChild() {
        throw new NotImplementedException();
    }
}
}
