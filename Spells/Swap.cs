namespace WizWar1 {
    class Swap : Spell {
        public Swap() {
            Name = "Swap";
            Description = "Change places with an opponent";
            RequiresLoS = false;
            ValidCastingTypes.Add(SpellType.Attack);
            ValidTargetTypes.Add(TargetTypes.Wizard);
        }

        public override void OnChildCast() {
            var fme = new ForceMoveEffect();
            fme.ForceMoveMethod = wizard => {
                var temp = wizard.Location;
                wizard.Location = Caster.Location;
                Caster.Location = temp;
            };

            EffectsWaiting.Add(Effect.Initialize<ForceMoveEffect>(Caster, this, SpellTarget, fme));
        }
    }
}
