using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Library;
using WizWar1.Properties;

namespace WizWar1 {

	class GameState : IListener<UselessEffectEvent, Event>, IListener<DestroyObjectEvent, Event> {
		public static EventSource<Event> EventDispatcher = new EventSource<Event>();

		public static Dictionary<Targetable, Targetable> AllElements = new Dictionary<Targetable, Targetable>();

		public static Random Dice = new Random();

		public static bool Debug = true;

		public static Wizard ActivePlayer;
		public static UIControl PriorityHolder;
		public static UIControl NextPriorityHolder {
			get {
				return Wizards[LibraryFunctions.IndexFixer(Wizards.IndexOf(PriorityHolder.myWizard) + 1, Wizards.Count)].myUI;
			}
		}
		private static int nPlayers;
		public static Deck Deck;
		public static List<ICard> Discard;
		public static RestrictedList<IStackable> TheStack;
		public static RobustList<Effect> DurationEffects;

		public static List<Wizard> Wizards;
		public static List<ICreation> Creations;
		public static BoardForm[] Form1Reference;
		public static List<UIControl> UiReference;

		public static Board BoardRef { get; set; }

		public static void Initialize(int tPlayers) {
			nPlayers = tPlayers;

			Wizards = new List<Wizard>();
			Discard = new List<ICard>();
			Form1Reference = new BoardForm[nPlayers];
			UiReference = new List<UIControl>(nPlayers);
			//NewGameInitialize();
		}

		public static void NewGameInitialize(int players) {
			BoardRef = new Board(players);
			Deck = new Deck();
			TheStack = new RestrictedList<IStackable>();
			DurationEffects = new RobustList<Effect>();
			Creations = new List<ICreation>();

			Wizards.Add(new Wizard(UiReference[0], "Blue Wizard", 2, 2));
			Wizards.Add(new Wizard(UiReference[1], "Red Wizard", 2, 7));

			for (int i = 0; i < UiReference.Count; i++) {
				UiReference[i].myWizard = Wizards[i];
				UiReference[i].myBoard.Show();
				//UiReference[i].myControl.Show();
			}
		}

		public static void StartNewGame(int humans, int computers) {
			NewGameInitialize(humans + computers);

			Deck.Regenerate();

			if (Settings.Default.GameMode == "SinglePlayer") {
				Deck.DealCards(Wizards[0], 7);

				for (int i = 0; i < 50; i++) {
					Wizards[1].giveCard(new Card<Fireball>());
				}
			}
			else {
				foreach (Wizard w in Wizards) {
					Deck.DealCards(w, 39);
				}
			}

			foreach (Wizard w in Wizards) {
				w.myUI.myBoard.HandOCardsRefresh();
			}

			var g = new MapGenerator();
			g.ParseMapFile();

			BoardRef.ReadMapFile();

			ActivePlayer = Wizards[Wizards.Count - 1];

			TurnCycle();
		}

		internal static void TurnCycle() {
			Wizard nextActivePlayer = Wizards[LibraryFunctions.IndexFixer(Wizards.IndexOf(ActivePlayer) + 1, Wizards.Count)];
			var tse = Event.New<TurnStartEvent>(true, new TurnStartEvent(nextActivePlayer));
			if (InitialUltimatum(tse) == Redirect.Proceed) {
				ActivePlayer.myUI.HasFinished = false;
				ActivePlayer.myUI.State = UIState.Locked;

				ActivePlayer = tse.NextWizard;
				ActivePlayer.myUI.State = UIState.Normal;   //must come after this player becomes active

				foreach (Effect e in DurationEffects) {
					if (e.Caster == ActivePlayer) {
						e.OnRun();
						var duration = e.markers.First(marker => marker is DurationBasedMarker) as DurationBasedMarker;

						duration.DurationBasedValue -= 1;
					}
				}

				//TODO: make sure robustList isn't causing weird behavior as effects are removed
				foreach (Effect e in DurationEffects) {
					foreach (Marker m in e.markers) {
						if (m is DurationBasedMarker && (m as DurationBasedMarker).DurationBasedValue == 0.0) {
							DurationEffects.Remove(e);
						}
					}
				}

				tse.IsAttempt = false;
				EventDispatcher.Notify(tse);
			}
		}

		internal static void RunTheStack() {
			while (TheStack.Count != 0) {
				IStackable temp = TheStack[TheStack.Count - 1];
				TheStack.Remove(temp);
				temp.OnRun();
			}
		}

		internal static void PushEffect(Effect tEffect) {
			var nee = Event.New<NewEffectEvent>(true, new NewEffectEvent(tEffect));
			if (InitialUltimatum(nee) == Redirect.Proceed) {
				if (!tEffect.markers.Any(marker => marker is DurationBasedMarker)) {
					TheStack.PleaseAdd(tEffect);
				}
				else {
					DurationEffects.Add(tEffect);
				}
			}
			nee.IsAttempt = false;
			EventDispatcher.Notify(nee);

			////if you want to stop it altogether, you missed your chance?
			//foreach (Event e in tEffect.myEvents) {
			//    e.IsAttempt = true;
			//    eventDispatcher.Notify(e);
			//    e.IsAttempt = false;
			//    eventDispatcher.Notify(e);
			//}
		}

		//similar event handling occurs in MyControl. Standardize?
		internal static void NewSpell(ISpell tSpell) {
			var ce = Event.New<CastEvent>(true, new CastEvent(tSpell));
			if (InitialUltimatum(ce) == Redirect.Proceed) {
				tSpell.OnCast();
				TheStack.PleaseAdd(tSpell);

				foreach (Wizard w in Wizards) {
					w.myUI.myBoard.RefreshAll();
					w.myUI.State = UIState.CastQuery;

					if (tSpell.Caster != w) {
						if (Debug == false) {
							MessageBox.Show(tSpell.Caster.Name + " cast a spell!");
						}
						continue;
					}

					//w.myUI.State = UIState.Normal; //only applies to the caster
				}
				ce.IsAttempt = false;
				EventDispatcher.Notify(ce);
			}
			//normal state and counteraction state are really the same
			//not really, we want to force the player to click continue before taking actions
		}

		//similar event handling occurs in MyControl. Standardize?
		internal static void NewSpell(IItemUsage tItem) {
			var ce = Event.New<ItemUseEvent>(true, new ItemUseEvent(tItem.Item));
			if (InitialUltimatum(ce) == Redirect.Proceed) {
				//tItem.OnActivationChild();    //figure out whether this means revealing, or using, or targeting or what

				TheStack.PleaseAdd(tItem);

				foreach (Wizard w in Wizards) {
					w.myUI.myBoard.RefreshAll();
					w.myUI.State = UIState.CastQuery;

					if (tItem.Item.Controller != w) {
						if (Debug == false) {
							MessageBox.Show(tItem.Item.Controller.Name + " used an item!");
						}
					}

					//w.myUI.State = UIState.Normal; //only applies to the caster
				}
				ce.IsAttempt = false;
				EventDispatcher.Notify(ce);
			}
			//normal state and counteraction state are really the same
			//not really, we want to force the player to click continue before taking actions
		}

		internal static void NewItem(IItem tItem) {
			var ire = Event.New<ItemRevealEvent>(true, new ItemRevealEvent(tItem));
			if (InitialUltimatum(ire) == Redirect.Proceed) {
				tItem.OnActivationParent();

				foreach (Wizard w in Wizards) {
					w.myUI.myBoard.RefreshAll();

					if (tItem.Creator != w) {
						MessageBox.Show(tItem.Creator.Name + " has revealed an item!");
					}
				}
				EventDispatcher.Notify(ire);
			}
		}

		public static void InvalidateAll() {
			foreach (UIControl ui in UiReference) {
				ui.myBoard.Invalidate();
			}
		}

		public void OnEvent(UselessEffectEvent tEvent) {
			TheStack.Remove(tEvent.me);
			DurationEffects.Remove(tEvent.me);
		}

		internal static void KillEffect(Effect tEffect) {
			DurationEffects.Remove(tEffect);
		}

		public void OnEvent(DestroyObjectEvent tEvent) {
			if (tEvent.DestroyedObject is IWall) {
				BoardRef.RemoveWall(tEvent.DestroyedObject as IWall);
			}
		}

		public static void RedrawAll() {
			foreach (UIControl ui in UiReference) {
				ui.myBoard.Invalidate();
			}
		}

		public static Redirect InitialUltimatum(Event tEvent, bool afterEffects = false) {
			tEvent.IsAttempt = true;
			EventDispatcher.Notify(tEvent);
			while (tEvent.GetFlowControl() == Redirect.Halt) {
				Thread.Sleep(100);
			}

			if (tEvent.GetFlowControl() == Redirect.Skip) {
				MessageBox.Show("Something has been skipped due to a " + tEvent);
			}

			return tEvent.GetFlowControl();
		}

		internal static Redirect TinyUltimatum(Event tEvent) {
			tEvent.IsAttempt = true;
			EventDispatcher.Notify(tEvent);
			while (tEvent.GetFlowControl() == Redirect.Halt) {
				Thread.Sleep(100);
			}

			if (tEvent.GetFlowControl() == Redirect.Skip) {
				MessageBox.Show("Tiny skip");
			}

			return tEvent.GetFlowControl();
		}
	}
}