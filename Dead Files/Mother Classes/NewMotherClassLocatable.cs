using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
abstract partial class NewMotherClass {
    private double x;
    public double X {
        get {
            return x;
        }
        set {
            x = value;
        }
    }

    private double y;
    public double Y {
        get {
            return y;
        }
        set {
            y = value;
        }
    }

    private Square square;
    public Square Square {
        get {
            return square;
        }
        set {
            square = value;
        }
    }
}
}
