namespace WizWar1 {
    class BrainStone : Spell {
        public BrainStone() {
            Name = "Brainstone";
            Description = "Create a small, flickering stone that quickens thought when worn on the brow";
            ValidCastingTypes.Add(SpellType.Item);
            ValidTargetTypes.Add(TargetTypes.None);
        }

        public override bool IsValidTarget(ITarget tTarget) {
            return false;
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.New<CreateItemEffect<BrainStoneItem>>(Caster, this, SpellTarget));
        }
    }
}
