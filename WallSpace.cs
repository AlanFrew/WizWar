using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class WallSpace : Targetable, ITarget {
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

    public WallSpace(double tX, double tY) {
        x = tX;
        y = tY;
    }
}
}
