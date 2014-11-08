namespace WizWar1 {
    interface ITreasure : ILocatable, ICarriable {
        Wizard Owner {
            get;
            set;
        }
    }
}
