namespace WizWar1 {
    class NoSpellToCastError : Error {
        public NoSpellToCastError() {
            explanation = "You have not selected a spell to cast";
        }
    }
}
