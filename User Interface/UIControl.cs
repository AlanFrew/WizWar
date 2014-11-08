using System.Windows.Forms;
using System.Drawing;

namespace WizWar1 {
	public class UIControl {
		private static Cursor castingCursor = new Cursor(@"Arena\NewRod.cur");
		private static Cursor waitCursor = new Cursor(@"Arena\Wait.cur");

		private UIState previousState = UIState.Locked;
		public UIState PreviousState {
			get {
				return previousState;
			}
		}

		private bool hasFinished = false;
		public bool HasFinished {
			get {
				return hasFinished;
			}
			set {
				hasFinished = value;
			}
		}

		private UIState state = UIState.Locked;
		public UIState State {
			get {
				return state;
			}
			set {
				if (value == state) {
					return;
				}

				var sce = Event.New<StateChangeEvent>(true, new StateChangeEvent(state, value, myWizard));
				if (GameState.InitialUltimatum(sce) == Redirect.Proceed) {
					myBoard.Cursor = Cursors.Default;

					//actions upon leaving a state go here
					if (state == UIState.CastingSpell) {
						CurrentAimable = null;
					}
					else if (state == UIState.Locked) {
						AttackedThisTurn = false;
					}

					//actions upon entering a state go here
					if (value == UIState.Previous) {
						state = previousState;
					}
					else {
						if (value == UIState.Normal) {
							if (hasFinished == true) {
								State = UIState.TurnComplete;
								return;
							}
							else {
								CurrentAimable = null;
							}
						}
						else if (value == UIState.TurnComplete) {
							CurrentAimable = null;
							AttackedThisTurn = false;
							hasFinished = true;
						}
						else if (value == UIState.CastingSpell) {
							myBoard.Cursor = castingCursor;
						}
						else if (value == UIState.CastQuery) {
							myBoard.Cursor = waitCursor;
						}

						previousState = state;
						state = value;
					}

					////change the cursor for the new state
					//foreach (Form1 form1 in GameState.Form1Reference) {
					//    if (state == UIState.CastingSpell) {
					//        form1.Cursor = castingCursor;
					//    }
					//    else {
					//        form1.Cursor = Cursors.Default;
					//    }
					//}
					sce.IsAttempt = false;
					GameState.EventDispatcher.Notify(sce);


				}
			}
		}

		internal IAimable CurrentAimable;

		internal Number NumberInUse;
		internal bool AttackedThisTurn = false;

		internal BoardForm myBoard;
		//internal ControlPanel myControl;
		internal InfoPane myInfo;
		internal SquareInfoPane mySquareInfo;
		internal Wizard myWizard;

		public UIControl(Point StartLocation) {
			GameState.UiReference.Add(this);

			myBoard = new BoardForm(this);
			myBoard.StartPosition = FormStartPosition.Manual;
			myBoard.Location = StartLocation;
			//myForm.Show();

			//myControl = new ControlPanel(this);
			//myControl.StartPosition = FormStartPosition.Manual;
			//myControl.Location = new Point(myBoard.Location.X + 10, myBoard.Location.Y + 545);
			//myControl.Show();
		}
	}
}