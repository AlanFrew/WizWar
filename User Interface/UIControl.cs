using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WizWar1 {
internal class UIControl {
    private static Cursor castingCursor = new Cursor(@"C:\Users\Alan Frew\My Pictures\Arena\Rod.cur");

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

            StateChangeEvent sce = Event.New<StateChangeEvent>(true, new StateChangeEvent(state, value));
            if (GameState.InitialUltimatum(sce) == Redirect.Proceed) {

                //actions upon leaving a state go here
                if (state == UIState.CastingSpell) {
                    myForm.Cursor = castingCursor;
                    SpellToCast = null;
                }
                else if (state == UIState.Locked) {
                    AttackedThisTurn = false;
                }
                else {
                    myForm.Cursor = Cursors.Default;
                }

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
                            SpellToCast = null;
                            ItemToUse = null;
                        }
                    }
                    else if (value == UIState.TurnComplete) {
                        SpellToCast = null;
                        ItemToUse = null;
                        AttackedThisTurn = false;
                        hasFinished = true;
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
                GameState.eventDispatcher.Notify(sce);
            }
        }
    }

    public ISpell SpellToCast;
    public IItem ItemToUse;

    public Number NumberInUse;
    public bool AttackedThisTurn = false;

    public Form1 myForm;
    public ControlPanel myControl;
    public InfoPane myInfo;
    public SquareInfoPane mySquareInfo;
    public Wizard myWizard;

    public UIControl(Point StartLocation) {
        GameState.UIReference.Add(this);

        myForm = new Form1(this);
        myForm.StartPosition = FormStartPosition.Manual;
        myForm.Location = StartLocation;
        myForm.Show();

        myControl = new ControlPanel(this);
        myControl.StartPosition = FormStartPosition.Manual;
        myControl.Location = new Point(myForm.Location.X, myForm.Location.Y + 550);
        myControl.Show();
    }
}
}
