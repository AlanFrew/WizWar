using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace WizWar1 {

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
			creationsHere = new List<ICreation>();
			locatablesHere = new List<ILocatable>();
		}

		private List<ILocatable> locatablesHere;
		public IList<ILocatable> LocatablesHere {
			get {
				return locatablesHere.AsReadOnly();
			}

		}

		//private List<ICarriable> carriablesHere;
		public IList<ICarriable> CarriablesHere {
			get {
				return locatablesHere.AsReadOnly().OfType<ICarriable>().ToList();
			}
		}

		//private List<IItem> itemsHere;
		public IList<IItem> ItemsHere {
			get {
				return locatablesHere.AsReadOnly().OfType<IItem>().ToList();
			}
		}

		public void RemoveLocatable(ILocatable locatable) {
			locatablesHere.Remove(locatable);
		}

		public void AddLocatable(ILocatable tItem) {
			if (locatablesHere.Contains(tItem)) {
				return;
			}

			locatablesHere.Add(tItem);
			tItem.Location = this;

			Wizard squareOwner = null;
			foreach (Wizard w in GameState.Wizards) {
				if (w.HomeSquare == this) {
					squareOwner = w;
					break;
				}
			}

			if (tItem.ActiveTargetType == TargetTypes.Treasure && squareOwner != null) {
				int otherPlayersTreasures = 0;
				foreach (ILocatable isItTreasure in locatablesHere) {
					if (isItTreasure is Treasure) {
						if ((isItTreasure as Treasure).Owner != squareOwner) {
							otherPlayersTreasures++;
						}
					}
				}

				if (otherPlayersTreasures >= 2) {
					MessageBox.Show("You won!!");
				}
			}
		}

		public List<ICreation> creationsHere;

		public void RemoveItem(ICarriable tItem) {
			locatablesHere.Remove(tItem);
		}
	}
}