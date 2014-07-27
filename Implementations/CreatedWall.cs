using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Library;

namespace WizWar1 {
class CreatedWall : Creation, ICreation, IWall {
    protected Image myImageVertical = null;
    protected Image myImageHorizontal = null;
    public Image MyImage {
        get {
            if (IsVertical()) {
                return myImageVertical;
            }
            else {
                return myImageHorizontal;
            }
        }
        set {
        }
    }

    public virtual void BecomeWall(Wizard tCaster, double tX, double tY) {
        if (this is Creation) {
            Creator = tCaster;
        }
        x = tX;
        y = tY;
        isWall = true;
    }

    protected bool isWall;
    public bool IsWall {
        get {
            return isWall;
        }
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
            LibraryFunctions.Swap(ref firstNeighbor, ref secondNeighbor);
            return;
        }
        else if (firstNeighbor.Y > secondNeighbor.Y) {
            LibraryFunctions.Swap(ref firstNeighbor, ref secondNeighbor);
            return;
        }
    }

    public virtual bool IsPassable(Wizard tWizard) {
        return false;
    }

    public bool IsVertical() {
        if (FirstNeighbor.X == SecondNeighbor.X) {
            return false;
        }

        return true;
    }

    public bool IsHorizontal() {
        return !(IsVertical());
    }
}
}
