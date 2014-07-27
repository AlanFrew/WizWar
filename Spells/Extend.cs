using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class Extend : Spell {
        public Extend() {
            Name = "Extend";
            validTargetTypes.Add(TargetTypes.Spell);
        }

        public override bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
            foreach (Marker m in (tTarget as ISpell).Markers) {
                if (m is DurationBasedMarker) {
                    return true;
                }
            }

            return false;
        }

        public override void OnChildCast() {
            AlterDurationValue adv = Effect.New<AlterDurationValue>(Caster, this, SpellTarget);
            AlterDurationValue.ValueProcessorDelegate d = ProcessDurationValueFunc;
            adv.Initialize(d);
            EffectsWaiting.Add(adv);
        }

        private int ProcessDurationValueFunc(int tDuration) {
            return tDuration *= 2;
        }
    }
}
