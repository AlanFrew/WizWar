namespace WizWar1 {
    class ThoughtSteal : Spell {
        public ThoughtSteal() {
            Name = "ThoughtSteal";
            ValidCastingTypes.Add(SpellType.Attack);
            ValidTargetTypes.Add(TargetTypes.Wizard);
        }

        //TODO: UI work
    }
}
