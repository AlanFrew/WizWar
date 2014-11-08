namespace WizWar1 {
interface ILocatable : ITarget {
    double X {
        get;
        set;
    }

    double Y {
        get;
        set;
    }

    Square Location {get; set;}
}


}
