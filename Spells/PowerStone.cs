namespace WizWar1 {
    class PowerStone : Spell {
        public PowerStone() {
            Name = "Powerstone";
            Description = "Create a small pulsing stone that empowers your spells, adding 1 to all number cards played";
            ValidCastingTypes.Add(SpellType.Item);
            ValidTargetTypes.Add(TargetTypes.None);
        }

        public override bool IsValidTarget(ITarget tTarget) {
            return false;
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.New<CreateItemEffect<PowerStoneItem>>(Caster, this, SpellTarget));
        }
    }
}
