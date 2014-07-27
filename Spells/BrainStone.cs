using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class BrainStone : Spell {
        public BrainStone() {
            Name = "Brainstone";
            validCastingTypes.Add(SpellType.Item);
        }

        public override bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
            return false;
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.New<CreateItemEffect<BrainStoneItem>>(Caster, this, SpellTarget));
        }
    }
}
