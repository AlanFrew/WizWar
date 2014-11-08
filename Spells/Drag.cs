namespace WizWar1 {
    class Drag : Spell {
        public Drag() {
            Name = "Drag";
            Description = "Drag an object to your square";
            ValidCastingTypes.Add(SpellType.Neutral);
            ValidTargetTypes.Add(TargetTypes.Item);
            ValidTargetTypes.Add(TargetTypes.Wizard);
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.New<DragEffect>(Caster, this, SpellTarget));
        }
    }
}
