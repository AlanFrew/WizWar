using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class NullifyEffect : Effect {
    public override void OnRunChild() {
        (target as ISpell).EffectsWaiting.Clear();
    }
}
}