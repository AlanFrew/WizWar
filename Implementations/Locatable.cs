using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    //private Square square;
    public Square ContainingSquare {
        get {
            return GameState.BoardRef.At(x, y);
        }
        set {
            x = value.x;
            y = value.y;
        }
    }
}
}
