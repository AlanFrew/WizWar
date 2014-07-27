using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
abstract partial class NewMotherClass {
    public virtual void BecomeWall(Wizard tCaster, double tX, double tY) {
        if (validWall == false) {
            return;
        }

        caster = tCaster;
        x = tX;
        y = tY;
        isWall = true;
    }

    protected Square firstNeighbor;
    public Square FirstNeighbor {
        get {
            return firstNeighbor;
        }
        set {
            firstNeighbor = value;
        }
    }

    protected Square secondNeighbor;
    public Square SecondNeighbor {
        get {
            return secondNeighbor;
        }
        set {
            secondNeighbor = value;
        }
    }

    public void ArrangeNeighbors() {
        if (firstNeighbor.X > secondNeighbor.X) {
            Library.Swap(ref firstNeighbor, ref secondNeighbor);
            return;
        }
        else if (firstNeighbor.Y > secondNeighbor.Y) {
            Library.Swap(ref firstNeighbor, ref secondNeighbor);
            return;
        }
    }

    public virtual bool IsPassable(Wizard tWizard) {
        return false;
    }
}
}
