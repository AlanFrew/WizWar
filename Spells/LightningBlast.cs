using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class LightningBlast : Spell {
        
    public LightningBlast() {
        Name = "Lightning Blast";
        validCastingTypes.Add(SpellType.Attack);
        validTargetTypes.Add(TargetTypes.Wizard);
        validTargetTypes.Add(TargetTypes.Wall);
        validTargetTypes.Add(TargetTypes.Creation);
        acceptsNumber = true;
    }

    public override void OnChildCast() {
        if (SpellTarget is IDamageable == false) {
            throw new NotSupportedException();
        }
        DamageEffect de = Effect.Initialize<DamageEffect>(Caster, this, SpellTarget, new DamageEffect(CardValue, DamageType.Magical));
        EffectsWaiting.Add(de);
    }

    public override void RanChild(Effect tEffect) {
        if (tEffect is DamageEffect) {
            if ((tEffect as DamageEffect).Amount > 0 && tEffect.target is Wizard) {
                if (tEffect.target == GameState.ActivePlayer) {
                    GameState.NewEffect(Effect.New<EndTurnEffect>(Caster, this, tEffect.target));
                }
                else {
                    GameState.NewEffect(Effect.New<LostTurnEffect>(Caster, this, tEffect.target));
                }
            }
        }
    }
}
}
