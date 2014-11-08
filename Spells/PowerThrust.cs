namespace WizWar1 {
    class PowerThrust : NumberedSpell {
        public PowerThrust() {
            Name = "Powerthrust";
            Description = "Fire a blast of arcane energy, dealing 2 damage plus the accompanying number card";
            ValidCastingTypes.Add(SpellType.Attack);
            ValidTargetTypes.Add(TargetTypes.Wizard);
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.Initialize<DamageEffect>(Caster, this, SpellTarget, new DamageEffect(2 + CardValue, DamageType.Magical)));
        }
    }
}
