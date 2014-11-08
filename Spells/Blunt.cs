namespace WizWar1 {
    class Blunt : Spell {
        public Blunt() {
            Name = "Blunt";
            Description = "Harden your body, reducing incoming damage by half";
            ValidCastingTypes.Add(SpellType.Counteraction);
            ValidTargetTypes.Add(TargetTypes.Effect);
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.New<BluntEffect>(Caster, this, SpellTarget));
        }
    }
}
