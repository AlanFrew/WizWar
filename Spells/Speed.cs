namespace WizWar1 {
    class Speed : Spell {
        public Speed() {
            Name = "Speed";
            Description = "Take another turn after this one";
            ValidCastingTypes.Add(SpellType.Neutral);
            ValidTargetTypes.Add(TargetTypes.None);
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.Initialize<AdditionalTurnEffect>(Caster, this, SpellTarget, new AdditionalTurnEffect()));
        }
    }
}
