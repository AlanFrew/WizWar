using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Library {
public struct DoublePoint {
    public double x;
    public double y;

    public DoublePoint(double tX, double tY) {
        x = tX;
        y = tY;
    }

    public Point ToPoint() {
        return new Point((int)x, (int)y);
    }

    public Point ToPoint(double tX, double tY) {
        return new Point((int)tX, (int)tY);
    }
}
}
