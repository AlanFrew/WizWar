namespace WizWar1 {
    class SoulStone : Spell {
        public SoulStone() {
            Name = "Soulstone";
            Description = "Create a translucent grey stone that hides part of your soul, preventing all magical damage when your life total is 3 or less";
            ValidCastingTypes.Add(SpellType.Item);
            ValidTargetTypes.Add(TargetTypes.None);
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(new CreateItemEffect<SoulStoneItem>());
        }
    }
}
