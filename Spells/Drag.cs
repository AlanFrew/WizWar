using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class Drag : Spell {
        public Drag() {
            Name = "Drag";
            validTargetTypes.Add(TargetTypes.Item);
            validTargetTypes.Add(TargetTypes.Wizard);
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.New<DragEffect>(Caster, this, SpellTarget));
        }
    }
}
