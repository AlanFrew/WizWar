namespace WizWar1 {
    class WaterWall : NumberedSpell {
        public WaterWall() {
            Name = "Waterwall";
            Description = "Create a towering wall of water that immediately collapses, pushing nearby wizards away";
            ValidCastingTypes.Add(SpellType.Counteraction);
            ValidCastingTypes.Add(SpellType.Neutral);
            ValidTargetTypes.Add(TargetTypes.WallSpace);
            ValidTargetTypes.Add(TargetTypes.Spell);
        }

        public override bool IsValidTarget(ITarget tTarget) {
            if (ActiveSpellType == SpellType.Counteraction) {
                return tTarget is ISpell;
            }

            //else neutral
            if (tTarget is WallSpace && (tTarget as WallSpace).WallHere == null) {
                return true;
            }

            return false;
        }

        public override void OnChildCast() {
            var me = this;
            foreach (Wizard w in GameState.Wizards) {
                var direction = DirectionC.DirectionToTarget(SpellTarget as ILocatable, w);

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
}
