namespace WizWar1 {
class Locatable : Targetable, ILocatable {
    protected double x;
    public double X {
        get {
            return x;
        }
        set {
            x = value;
        }
    }

    protected double y;
    public double Y {
        get {
            return y;
        }
        set {
            y = value;
        }
    }

    public Square Location {
        get {
            return GameState.BoardRef.At(x, y);
        }
        set {
            x = value.X;
            y = value.Y;
        }
    }
}
}
