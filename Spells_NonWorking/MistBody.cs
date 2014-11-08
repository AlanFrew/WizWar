namespace WizWar1 {
    class MistBody : NumberedSpell {
        public MistBody() {
            Name = "Mist Body";
            Markers.Add(new DurationBasedMarker());
        }

        public override void OnChildCast() {
            DamageEffect de = Effect.Initialize<DamageEffect>(Caster, this, SpellTarget, new DamageEffect(CardValue, DamageType.Magical));
            EffectsWaiting.Add(de);
        }
    }
}
