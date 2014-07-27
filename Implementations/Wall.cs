using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Library;

namespace WizWar1 {
class Wall : Locatable, IWall, IDamageable {
    //public virtual void BecomeWall(Wizard tCaster, double tX, double tY) {
    //    if (this is Creation) {
    //        caster = tCaster;
    //    }
    //    x = tX;
    //    y = tY;
    //    isWall = true;
    //}

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

    //only swaps when two neighbors are next to each other *not* counting wraparound
    public void ArrangeNeighbors() {
        if (firstNeighbor.X == secondNeighbor.X + 1) {
            LibraryFunctions.Swap(ref firstNeighbor, ref secondNeighbor);
            return;
        }
        else if (firstNeighbor.Y == secondNeighbor.Y + 1) {
            LibraryFunctions.Swap(ref firstNeighbor, ref secondNeighbor);
            return;
        }
    }

    public virtual bool IsPassable(Wizard tWizard) {
        return false;
    }

    public bool IsVertical() {
        if (FirstNeighbor.X  == SecondNeighbor.X) {
            return false;
        }

        return true;
    }

    public bool IsHorizontal() {
        return !(IsVertical());
    }

    private int hitPoints = 20;

    public void TakeDamage(DamageEffect d) {
        hitPoints -= d.Amount;
        if (hitPoints >= 0) {
            GameState.BoardRef.RemoveWall(this);
        }
    }
}
}
