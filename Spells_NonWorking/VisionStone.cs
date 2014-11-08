namespace WizWar1 {
    class VisionStone : Spell {
        public VisionStone() {
            Name = "Visionstone";
            ValidCastingTypes.Add(SpellType.Item);
            ValidTargetTypes.Add(TargetTypes.None);
        }

        public override void OnChildCast() {
            base.OnChildCast();
        }
    }
}
