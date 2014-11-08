namespace WizWar1 {
    class WaterBolt : NumberedSpell {
        public WaterBolt() {
            Name = "Waterbolt";
            Description = "Blast an opponent backwards with a jet of water. If he cannot move, he takes 1 damage for each space";
            ValidCastingTypes.Add(SpellType.Attack);
            ValidTargetTypes.Add(TargetTypes.Wizard);
        }

        public override void OnChildCast() {
            var me = this;
            var direction = DirectionC.AngleToDirection(ShotDirection);

            var fme = new ForceMoveEffect();
            fme.ForceMoveMethod = wizard => {
                for (int i = 0; i < CardValue; i++) {
                    if (wizard.PushOne(direction) == false) {
                        GameState.PushEffect(Effect.Initialize<DamageEffect>(Caster, me, wizard, new DamageEffect(1, DamageType.Magical)));
                    }
                }
            };

            EffectsWaiting.Add(Effect.Initialize<ForceMoveEffect>(Caster, this, SpellTarget, fme));
        }
    }
}
