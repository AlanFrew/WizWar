using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class Fireball : Spell {
        public Fireball() {
            Name = "Fireball";
            validTargetTypes.Add(TargetTypes.Wizard);
            validTargetTypes.Add(TargetTypes.Wall);
            validTargetTypes.Add(TargetTypes.Creation);
            validCastingTypes.Add(SpellType.Attack);
        }

        public override void OnChildCast() {
            DamageEffect d = Effect.Initialize<DamageEffect>(Caster, this, SpellTarget, new DamageEffect(5, DamageType.Magical));
            EffectsWaiting.Add(d);

            //if (SpellTarget is Wizard) {
            //    foreach (IItem i in (SpellTarget as Wizard).Inventory) {
            //        if (i is Stone) {
            //            EffectsWaiting.Add(Effect.New<DestroyItemEffect>(Caster, this, SpellTarget));
            //        }
            //    }
            //}
        }

        public override void RanChild(Effect tEffect) {
            if (tEffect is DamageEffect && tEffect.target is Wizard) {
                if ((tEffect as DamageEffect).Amount > 0) {
                    foreach (Item i in (SpellTarget as Wizard).Inventory) {
                        if (i is Stone) {
                            GameState.NewEffect(Effect.New<DestroyItemEffect>(Caster, this, i));
                        }
                    }
                }
            }
        }
    }
}
