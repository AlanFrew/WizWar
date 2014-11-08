namespace WizWar1 {
    class PowerRun : Spell {
        public PowerRun() {
            Name = "Power Run";
            Description = "Spend your life energy to run at great speed";
            ValidCastingTypes.Add(SpellType.Neutral);
            ValidTargetTypes.Add(TargetTypes.Self);
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.New<PowerRunEffect>(Caster, this, SpellTarget));
        }
    }
}
