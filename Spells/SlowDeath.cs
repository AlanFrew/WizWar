namespace WizWar1 {
    class SlowDeath : Spell {
        public SlowDeath() {
            Name = "Slow Death";
            Description = "Curse an opponent, causing him to lose 1 life whenever he draws a card--until death";
            ValidCastingTypes.Add(SpellType.Attack);
            ValidTargetTypes.Add(TargetTypes.Wizard);
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.Initialize<SlowDeathEffect>(Caster, this, SpellTarget, new SlowDeathEffect()));
        }
    }
}
