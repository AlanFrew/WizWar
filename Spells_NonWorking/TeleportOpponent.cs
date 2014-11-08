namespace WizWar1 {
    class TeleportOpponent : Spell {
        public TeleportOpponent() {
            Name = "Teleport Opponent";
            Description = "Forcibly move an opponent to a square of your choosing";
            ValidCastingTypes.Add(SpellType.Attack);
            ValidTargetTypes.Add(TargetTypes.Wizard);
        }

        public override void OnChildCast() {
            var fme = new ForceMoveEffect();
            fme.ForceMoveMethod = wizard => {
                wizard.Location = null;
                //TODO: Finish targeting
            };

            EffectsWaiting.Add(Effect.Initialize<ForceMoveEffect>(Caster, this, SpellTarget, fme));
        }
    }
}
