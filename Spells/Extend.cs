namespace WizWar1 {
    class Extend : Spell {
        public Extend() {
            Name = "Extend";
            Description = "Double the duration of a spell";
            ValidCastingTypes.Add(SpellType.Neutral);
            ValidTargetTypes.Add(TargetTypes.Spell);
        }

        public override bool IsValidTarget(ITarget tTarget) {
            foreach (Marker m in (tTarget as ISpell).Markers) {
                if (m is DurationBasedMarker) {
                    return true;
                }
            }

            return false;
        }

        public override void OnChildCast() {
            AlterDurationValue adv = Effect.New<AlterDurationValue>(Caster, this, SpellTarget);
            AlterDurationValue.ValueProcessorDelegate d = ProcessDurationValueFunc;
            adv.Initialize(d);
            EffectsWaiting.Add(adv);
        }

        private int ProcessDurationValueFunc(int tDuration) {
            return tDuration *= 2;
        }
    }
}
