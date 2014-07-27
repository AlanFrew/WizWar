using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DispelCreation : Spell {
    public DispelCreation() {
        Name = "Dispel Creation";
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<DispelCreationEffect>(Caster, this, SpellTarget));
    }

    public override void OnResolution() {
        base.OnResolution();
    }
}
}
