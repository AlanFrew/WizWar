using System.Linq;

namespace WizWar1 {
    class StoneDead : NumberedSpell {
        public StoneDead() {
            Name = "Stone Dead";
            Description = "Create an energy surge in an opponent's stones, dealing damage equal to the number of stones times the accompanying number card";
            ValidCastingTypes.Add(SpellType.Attack);
            ValidTargetTypes.Add(TargetTypes.Wizard);
        }

        public override void OnChildCast() {
            int stoneCount = (SpellTarget as Wizard).Inventory.Where(item => item is Stone).Count();

            EffectsWaiting.Add(Effect.Initialize<DamageEffect>(Caster, this, SpellTarget, new DamageEffect(stoneCount * CardValue, DamageType.Magical)));
        }
    }
}
