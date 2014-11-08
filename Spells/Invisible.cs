namespace WizWar1 {
    class Invisible : NumberedSpell {
        public Invisible() {
            Name = "Invisible";
            Description = "Fade from sight, cackling madly";
            ValidCastingTypes.Add(SpellType.Neutral);
            ValidTargetTypes.Add(TargetTypes.Wizard);
            //AcceptsNumber = true;
        }

        public override bool IsValidTarget(ITarget tTarget) {
            if ((tTarget as Spell).SpellTarget == Caster) {
                return true;
            }
 
            return false;
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.New<InvisibleEffect>(Caster, this, SpellTarget, CardValue));
        }
    }
}
