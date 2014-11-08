using System;

namespace WizWar1 {
    class ShieldStone : Spell, IListener<DamageEvent, Event> {
        public ShieldStone() {
            Name = "Shieldstone";
            ValidCastingTypes.Add(SpellType.Item);
            ValidTargetTypes.Add(TargetTypes.None);
        }

        public void OnEvent(DamageEvent damageEvent) {
            damageEvent.myEffect.Amount = Math.Max(0, damageEvent.myEffect.Amount - 1);
        }
    }
}