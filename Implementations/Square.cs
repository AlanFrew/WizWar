using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWar1
{

class Square : Locatable {
    private int iD;
    public int ID {
        get { return iD; }
        set { iD = value; }
    }

    //public void setNeighbor(Square tNeighbor, Direction toNeighborDir) {
    //    neighbors[(int)toNeighborDir] = tNeighbor;
    //    tNeighbor.neighbors[(int)DirectionC.oppositeDirection(toNeighborDir)] = this;
    //}

    public Square GetNeighbor(Direction tDirection) {
        switch (tDirection) {
            case Direction.North:
                return GameState.BoardRef.At(X, Y - 1);
            case Direction.South:
                return GameState.BoardRef.At(X, Y + 1);
            case Direction.East:
                return GameState.BoardRef.At(X + 1, Y);
            case Direction.West:
                return GameState.BoardRef.At(X - 1, Y);
            default:
                throw new UnreachableException();
        }
    }

    public IWall LookForWall(Direction tDirection) {
        return GameState.BoardRef.LookForWall(this, GetNeighbor(tDirection));
    }

    public Square() {
        //empty
    }

    public Square(int tX, int tY) {
        x = tX;
        y = tY;
        itemsHere = new List<IItem>();
        creationsHere = new List<ICreation>();
    }

    private List<IItem> itemsHere;
    public IList<IItem> ItemsHere {
        get {
            return itemsHere.AsReadOnly();
        }
    }

    public void AddItem(IItem tItem) {
        if (itemsHere.Contains(tItem) == true) {
            return;
        }

        itemsHere.Add(tItem);
        tItem.Location = this;

        Wizard SquareOwner = null;
        foreach (Wizard w in GameState.wizards) {
            if (w.HomeSquare == this) {
                SquareOwner = w;
                break;
            }
        }

        if (tItem.ActiveTargetType == TargetTypes.Treasure && SquareOwner != null) {
            int OtherPlayersTreasures = 0;
            foreach (IItem isItTreasure in itemsHere) {
                if (isItTreasure is Treasure) {
                    if ((isItTreasure as Treasure).Owner != SquareOwner) {
                        OtherPlayersTreasures++;
                    }
                }
            }

            if (OtherPlayersTreasures >= 2) {
                MessageBox.Show("You won!!");
            }
        }
    }

    public List<ICreation> creationsHere;

    public void RemoveItem(IItem tItem) {
        itemsHere.Remove(tItem);
    }
}
}
