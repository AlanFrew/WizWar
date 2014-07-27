using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class Blunt : Spell {
        public Blunt() {
            Name = "Blunt";
            validCastingTypes.Add(SpellType.Counteraction);
            validTargetTypes.Add(TargetTypes.Effect);
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.New<BluntEffect>(Caster, this, SpellTarget));
        }
    }
}
