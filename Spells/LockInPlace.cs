namespace WizWar1 {
    class LockInPlace : NumberedSpell {
        public LockInPlace() {
            Name = "Lock In Place";
            Description = "Bind an opponent's feet with magical chains, preventing him from all movement";
            ValidCastingTypes.Add(SpellType.Neutral);
            ValidTargetTypes.Add(TargetTypes.Wizard);
        }

        public override void OnChildCast() {
            var e = Effect.Initialize<SnareEffect>(Caster, this, SpellTarget, new SnareEffect(CardValue));
            EffectsWaiting.Add(e);
        }
    }
}
