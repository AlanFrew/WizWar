using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using WizWar1.Properties;
using Library;

namespace WizWar1 {

	class Wizard : Locatable, IDamageable, IListener<TurnStartEvent, Event> {
		public UIControl myUI;

		public new double X {
			get { return x; }
			set {
				if (value == x) {
					return;
				}

				x = value;			
			}
		}

		public new double Y {
			get { return y; }
			set {
				if (value == y) {
					return;
				}

				y = value;
			}
		}

		public int hit_points;

		public int MaxHandSize;

		public Square HomeSquare;

		public int CurrentHandSize {
			get {
				return Hand.Count() + inventory.Count();
			}
		}

		private int movesLeft;
		public int MovesLeft {
			get {
				return movesLeft;
			}
			set {
				movesLeft = value;
			}
		}

		private int cardsDrawn = 0;
		public int CardsDrawn {
			get {
				return cardsDrawn;
			}
			set {
				cardsDrawn = value;
			}
		}

		private int cardsDiscarded = 0;
		public int CardsDiscardedByChoice {
			get {
				return cardsDiscarded;
			}
			set {
				cardsDiscarded = value;
			}
		}

		public bool SetPosition(double tX, double tY) {
			try {
				Square newSquare = GameState.BoardRef.At(tX, tY);
				foreach (ICreation c in newSquare.creationsHere) {
					if (c is SolidStoneCreation) {
						return false;
					}
				}

				Square oldSquare = GameState.BoardRef.At(x, y);

				oldSquare.RemoveLocatable(this);
				newSquare.AddLocatable(this);

				foreach (IItem i in inventory) {
					i.X = x;
					i.Y = y;
				}

				X = tX;
				Y = tY;



			}
			catch (ArgumentOutOfRangeException) {
				return false;
			}

			return true;
		}

		public void OnEvent(TurnStartEvent tTurnStartEvent) {
			if (tTurnStartEvent.NextWizard == this && tTurnStartEvent.IsAttempt == false) {
				BeginTurn();
			}
		}

		public bool PushOne(Direction direction) {
			Board b = GameState.BoardRef;
			Square current = b.At(X, Y);
			Square next = b.At(X, Y).GetNeighbor(direction);
			IWall w = b.LookForWall(current, next);

			if (w != null) {
				var pte = new PassthroughEvent(this, w);

				if (w.IsPassable(this) == false) {
					pte.SetFlowControl(Redirect.Skip, 0.1);
				}

				if (GameState.InitialUltimatum(pte) != Redirect.Proceed) {
					return false;
				}
			}

			foreach (ICreation c in next.creationsHere) {
				if (c is Obstruction) {
					if ((c as Obstruction).IsPassable(this) == false) {
						return false;
					}
				}
			}

			var me = new MoveEvent(this, new DoublePoint(X, Y), new DoublePoint(next.X, next.Y));

			if (GameState.InitialUltimatum(me) != Redirect.Proceed) {
				return false;
			}

			SetPosition(next.X, next.Y);

			foreach (ICreation c in next.creationsHere) {
				if (c is Obstruction) {
					(c as Obstruction).OnEnterParent(this);
				}
			}

			return true;
		}

		public bool MoveOne(Direction tDirection) {
			if (MovesLeft == 0) {
				return false;
			}

			Board b = GameState.BoardRef;
			Square current = b.At(X, Y);
			Square next = b.At(X, Y).GetNeighbor(tDirection);
			IWall w = b.LookForWall(current, next);

			if (w != null) {
				var pte = new PassthroughEvent(this, w);

				if (w.IsPassable(this) == false) {
					pte.SetFlowControl(Redirect.Skip, 0.1);
				}

				if (GameState.InitialUltimatum(pte) != Redirect.Proceed) {
					return false;
				}
			}

			foreach (ICreation c in next.creationsHere) {
				if (c is Obstruction) {
					if ((c as Obstruction).IsPassable(this) == false) {
						return false;
					}
				}
			}

			var me = new MoveEvent(this, new DoublePoint(X, Y), new DoublePoint(next.X, next.Y));

			if (GameState.InitialUltimatum(me) != Redirect.Proceed) {
				return false;
			}

			SetPosition(next.X, next.Y);

			foreach (ICreation c in next.creationsHere) {
				if (c is Obstruction) {
					(c as Obstruction).OnEnterParent(this);
				}
			}

			MovesLeft--;

			return true;
		}

		public String Name { get; set; }

		public override String ToString() {
			return Name;
		}

		private List<ICard> hand;
		public ReadOnlyCollection<ICard> Hand {
			get {
				return hand.AsReadOnly();
			}
		}

		public void HandAdd(ICard tCard) {
			hand.Add(tCard);
			myUI.myBoard.HandOCardsRefresh();
		}

		public void HandRemove(ICard tCard) {
			hand.Remove(tCard);
			myUI.myBoard.HandOCardsRefresh();
		}

		public void HandAddRange(List<ICard> tList) {
			hand.AddRange(tList);
			myUI.myBoard.HandOCardsRefresh();
		}

		private List<ICarriable> inventory;
		public IList<ICarriable> Inventory {
			get {
				return inventory.AsReadOnly();
			}
		}

		public List<Effect> personalEffects;

		public Wizard(UIControl tMyUI) {
			myUI = tMyUI;

			activeTargetType = TargetTypes.Wizard;
			hit_points = 15;
			inventory = new List<ICarriable>();
			hand = new List<ICard>();
			personalEffects = new List<Effect>();
			MaxHandSize = Settings.Default.MaxHandSize;
		}

		public Wizard(UIControl tMyUI, String tName, int tX, int tY) : this(tMyUI) {
			Name = tName;

			SetPosition(tX, tY);

			GameState.AllElements.Add(this, this);

			GameState.EventDispatcher.Register(this);
		}

		public void giveCards(ICard[] tCards) {
			for (int i = 0; i < tCards.Length; ++i) {
				if (tCards[i] != null) {
					HandAdd(tCards[i]);
				}
			}

			SettleHand();
		}

		public void giveCard(ICard tCard) {
			giveCards(new ICard[1] { tCard });
		}

		public bool hasItemType<T>(T tItem) {
			foreach (IItem i in inventory) {
				if (i is T) {
					return true;
				}
			}
			return false;
		}

		public void giveItem(ICarriable tItem) {
			GameState.EventDispatcher.Notify(Event.New<ItemGainEvent>(false, new ItemGainEvent(tItem as IItem)));

			inventory.Add(tItem);

			tItem.Carrier = this;

			if (tItem is IItem) {
				(tItem as IItem).OnGainParent(this);
			}

			myUI.myBoard.ListOItemsRefresh();
		}

		public bool dropItem(ICarriable tItem) {
			if (loseItem(tItem)) {
				tItem.Location = GameState.BoardRef.At(X, Y);
				return true;
			}
			return false;
		}

		public bool loseItem(ICarriable tItem) {
			if (inventory.Contains(tItem)) {
				inventory.Remove(tItem);

				if (tItem is IItem) {
					(tItem as IItem).OnLossParent(this);
				}

				tItem.Carrier = null;

				myUI.myBoard.ListOItemsRefresh();
				return true;
			}

			return false;
		}

		public bool pickUpItem(ICarriable tItem) {
			if (tItem is Treasure) {
				if (hasItemType(tItem)) {
					return false;
				}
			}

			if (GameState.BoardRef.At(X, Y).CarriablesHere.Contains(tItem) == false) {
				return false;
			}

			giveItem(tItem);
			Location.RemoveItem(tItem);
			GameState.TurnCycle();
			return true;
		}

		public void OnEvent(Event e) {
			if (e is LostTurnEvent) {
				GameState.TurnCycle();
			}
		}

		public void TakeDamage(DamageEffect d) {
			GameState.EventDispatcher.Notify(new HealthChangeEvent { EventTarget = this, Amount = -d.Amount, IsAttempt = false });

			hit_points -= d.Amount;

			if (hit_points <= 0) {
				OnDeath(d.Caster);
			}
		}

		public void BeginTurn() {
			movesLeft = Settings.Default.MovesPerTurn;

			CardsDrawn = 0;

			bool endflag = false;
			//foreach (Effect e in personalEffects) {
			//    e.OnRun();

			//    if (e is LostTurnEffect) {
			//        if (e.duration > 0) {
			//            endflag = true;
			//            e.duration--;
			//        }
			//        else {
			//            personalEffects.Remove(e);
			//        }
			//    }
			//}

			if (endflag == true) {
				GameState.TurnCycle();
			}

			if (Settings.Default.GameMode == "SinglePlayer" && this.Name == "Red Wizard"){
				RunAI();
			}
		}

		public void EndTurn() {
			GameState.TurnCycle();
		}

		public void OnDeath(Wizard killer) {
			GameState.Wizards.Remove(this);
			foreach (IItem i in inventory) {
				GameState.BoardRef.At((int)x, (int)y).AddLocatable(i);
			}
			inventory = null;

			foreach (ICard c in Hand) {
				killer.HandAdd(c);
			}
			hand = null;
		}

		internal void TakeCard(ICard c) {
			HandRemove(c);
		}

		internal bool SettleHand() {
			if (CurrentHandSize <= MaxHandSize) {
				return true;
			}

			myUI.State = UIState.Discarding;

			return false;
			//if (GameState.InitialUltimatum(Event.New<SettleHandEvent>(true, new SettleHandEvent())) == Redirect.Proceed) {
			//    hand.Remove(Card.NewCard(wc.AdditionalInfo));
			//}

			//SettleHand();
		}

		public void Destroy(DestroyEffect destroyEffect) {
			if (GameState.ActivePlayer == this) { GameState.TurnCycle(); }

			GameState.Wizards.Remove(this);
		}

		public void RunAI() {
			if (GameState.BoardRef.TestLoSNew(GameState.Wizards[0].X - GameState.ActivePlayer.X, GameState.Wizards[0].Y - GameState.ActivePlayer.Y, myUI)){
				myUI.myBoard.StartCastingFireball(GameState.Wizards[0].Location);
				

				//if (spellToCast.IsValidTarget(GameState.Wizards[0])) {
				//	myUI.CurrentAimable = spellToCast;
				//	myUI.myBoard.TargetValidated(GameState.Wizards[0], TargetTypes.Wizard);
				//}
			}
		}
	}
}