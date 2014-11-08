using System;

namespace WizWar1 {
class LightningBlast : NumberedSpell {
        
    public LightningBlast() {
        Name = "Lightning Blast";
        Description = "Stagger an opponent with lightning, damaging and stunning him for 1 turn";
        ValidCastingTypes.Add(SpellType.Attack);
        ValidTargetTypes.Add(TargetTypes.Wizard);
        ValidTargetTypes.Add(TargetTypes.Wall);
        ValidTargetTypes.Add(TargetTypes.Creation);
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
                    GameState.PushEffect(Effect.New<EndTurnEffect>(Caster, this, tEffect.target));
                }
                else {
                    GameState.PushEffect(Effect.New<LostTurnEffect>(Caster, this, tEffect.target));
                }
            }
        }
    }
}
}
