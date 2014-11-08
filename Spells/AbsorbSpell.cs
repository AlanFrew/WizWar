namespace WizWar1 {
    class AbsorbSpell : Spell {
        //private NullifyEffect myNullify;
        //private DrawEffect myDraw;

        public AbsorbSpell() {
            Name = "Absorb Spell";
            Description = "Spin a magical net to capture an incoming spell; counter the spell and add it to your hand";
            ValidCastingTypes.Add(SpellType.Counteraction);
            ValidTargetTypes.Add(TargetTypes.Spell);
        }

        public override bool IsValidTarget(ITarget tTarget) {
            if ((tTarget as ISpell).SpellTarget == Caster) {
                if ((tTarget as ISpell).Caster != Caster) {
                    return true;
                }
            }

            return false;
        }

        public override void OnChildCast() {
            EffectsWaiting.Add(Effect.New<CounterSpellEffect>(Caster, this, SpellTarget));
            //EffectsWaiting.Add(Effect.New<DrawEffect>(Caster, this, SpellTarget));
            EffectsWaiting.Add(Effect.Initialize<TakeDiscardEffect>(Caster, this, SpellTarget, new TakeDiscardEffect(SpellTarget as ICard)));
        }
    }
}
