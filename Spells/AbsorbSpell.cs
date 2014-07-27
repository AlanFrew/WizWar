using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class AbsorbSpell : Spell {
        //private NullifyEffect myNullify;
        //private DrawEffect myDraw;

        public AbsorbSpell() {
            Name = "Absorb Spell";
            validCastingTypes.Add(SpellType.Counteraction);
            validTargetTypes.Add(TargetTypes.Spell);
        }

        public override bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
            if ((tTarget as ISpell).SpellTarget == tCaster) {
                if ((tTarget as ISpell).Caster != tCaster) {
                    return true;
                }
            }

            return false;
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.New<CounterSpellEffect>(Caster, this, SpellTarget));
            //EffectsWaiting.Add(Effect.New<DrawEffect>(Caster, this, SpellTarget));
            EffectsWaiting.Add(Effect.Initialize<TakeDiscardEffect>(Caster, this, SpellTarget, new TakeDiscardEffect(SpellTarget as ICard)));
        }
    }
}
