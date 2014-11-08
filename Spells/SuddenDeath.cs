namespace WizWar1 {
    class SuddenDeath : Spell {
        public SuddenDeath() {
            Name = "Sudden Death";
            Description = "Assault an opponent's lifeforce, dealing 10 damage";
            ValidCastingTypes.Add(SpellType.Attack);
            ValidTargetTypes.Add(TargetTypes.Wizard);
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.Initialize(Caster, this, SpellTarget, new DamageEffect(10, DamageType.Magical)));
        }
    }
}
