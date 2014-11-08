namespace WizWar1 {
class Treasure : Locatable, ITreasure {
    public Treasure(Wizard tOwner) {
        owner = tOwner;
        //itemTargetTypes.Add(TargetTypes.None);
        //not valid spell or card
    }

    public Wizard Carrier { get; set; }

    private Wizard owner;
    public Wizard Owner {
        get {
            return owner;
        }
        set {
            owner = value;
        }
    }

    public override string ToString() {
        return owner.Name + "'s Treasure";
    }
}
}
