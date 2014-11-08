namespace WizWar1 {
    class SpeedStone : Spell {
        public SpeedStone() {
            Name = "Speedstone";
            Description = "A small crackling stone lightens your step, allowing you to move an additional space each turn";
            ValidCastingTypes.Add(SpellType.Item);
            ValidTargetTypes.Add(TargetTypes.None);
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.Initialize<CreateItemEffect<SpeedStoneItem>>(Caster, this, SpellTarget, new CreateItemEffect<SpeedStoneItem>()));
        }
    }
}
