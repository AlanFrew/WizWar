using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Library;

namespace WizWar1{
    
class GameState : IListener<UselessEffectEvent, Event>, IListener<DestroyObjectEvent, Event> {
    public static EventSource<Event> eventDispatcher = new EventSource<Event>();

    public static Dictionary<Targetable, Targetable> allElements = new Dictionary<Targetable, Targetable>();

    public static Random Dice = new Random();

    public static bool Debug = true;

    public static Wizard ActivePlayer;
    public static UIControl PriorityHolder;
    public static UIControl NextPriorityHolder {
        get {
            return wizards[LibraryFunctions.IndexFixer(wizards.IndexOf(PriorityHolder.myWizard) + 1, wizards.Count)].myUI;
        }
    }
    private static int nPlayers = 2;
    public static Deck deck;
    public static List<ICard> discard;
    public static RestrictedList<IStackable> theStack;
    public static RestrictedList<Effect> durationEffects;

    public static List<Wizard> wizards;
    public static List<ICreation> creations;
    public static Form1[] Form1Reference;
    public static List<UIControl> UIReference;

    private static Board board;

    public static Board BoardRef {
        get {
            return board;
        }
        set {
            board = value;
        }
    }

    public static void Initialize() {
        wizards = new List<Wizard>();
        discard = new List<ICard>();
        Form1Reference = new Form1[2];
        UIReference = new List<UIControl>(nPlayers);
        NewGameInitialize();
    }

    public static void NewGameInitialize() {
        board = new Board(2);
        deck = new Deck();
        theStack = new RestrictedList<IStackable>();
        durationEffects = new RestrictedList<Effect>();
        creations = new List<ICreation>();
        
    }

    public static void startNewGame(int tPlayers) {
        NewGameInitialize();
       
        deck.regenerate();

        foreach (Wizard w in wizards) {
            deck.dealCards(w, 10);
        }

        foreach (Wizard w in wizards) {
            w.myUI.myControl.HandOCardsRefresh();
        }

        MapGenerator g = new MapGenerator();
        g.floob();

        BoardRef.readMapFile();
    }

    public static void SetMe(UIControl tMe) {
        Wizard temp = null;
        switch (wizards.Count + 1) {
            case 1: {
                temp = new Wizard(UIReference[0], "Blue Wizard", 2, 2); 
                break;
            }
            case 2: {
                temp = new Wizard(UIReference[1], "Red Wizard", 2, 7);
                break;
            }
            case 3: {
                temp = new Wizard(UIReference[2], "Green Wizard", 7, 2);
                break;
            }
            case 4: {
                temp = new Wizard(UIReference[3], "Yellow Wizard", 7, 7);
                break;
            }
            default:
                throw new UnreachableException();
        }

        wizards.Add(temp);
        tMe.myWizard = temp;


    }

    internal static void TurnCycle() {
        Wizard nextActivePlayer = wizards[LibraryFunctions.IndexFixer(wizards.IndexOf(ActivePlayer) + 1, wizards.Count)];
        TurnStartEvent tse = Event.New<TurnStartEvent>(true, new TurnStartEvent(nextActivePlayer));
        if (GameState.InitialUltimatum(tse) == Redirect.Proceed) {
            ActivePlayer.myUI.HasFinished = false;
            ActivePlayer.myUI.State = UIState.Locked;

            ActivePlayer = nextActivePlayer;
            ActivePlayer.myUI.State = UIState.Normal;   //must come after this player becomes active

            foreach (Effect e in durationEffects) {
                if (e.Caster == ActivePlayer) {
                    e.OnRun();
                    e.duration -= 1;
                }
            }

            durationEffects.RemoveAll(e => e.duration <= 0);

            tse.IsAttempt = false;
            eventDispatcher.Notify(tse);
        }
    }

    private static bool TestForNoDuration(Effect e) {
        if (e.duration <= 0) {
            return true;
        }

        return false;
    }

    internal static void RunSpells() {
        if (theStack.Count != 0) {
            IStackable temp = theStack[theStack.Count - 1];
            theStack.Remove(temp);
            temp.OnRun();
        }
    }

    internal static void NewEffect(Effect tEffect) {
        NewEffectEvent nee = Event.New<NewEffectEvent>(true, new NewEffectEvent(tEffect));
        if (GameState.InitialUltimatum(nee) == Redirect.Proceed) {
            if (tEffect.duration == 0) {
                theStack.PleaseAdd(tEffect);
            }
            else {
                durationEffects.PleaseAdd(tEffect);
            }
        }
        nee.IsAttempt = false;
        eventDispatcher.Notify(nee);

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
        CastEvent ce = Event.New<CastEvent>(true, new CastEvent(tSpell));
        if (GameState.InitialUltimatum(ce) == Redirect.Proceed) {
            tSpell.OnCast();
            theStack.PleaseAdd(tSpell);

            foreach (Wizard w in wizards) {
                w.myUI.myControl.RefreshAll();
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
            eventDispatcher.Notify(ce);
        }
        //normal state and counteraction state are really the same
        //not really, we want to force the player to click continue before taking actions
    }

    internal static void NewItem(ItemCard tItem) {
        ItemRevealEvent ire = Event.New<ItemRevealEvent>(true, new ItemRevealEvent(tItem));
        if (GameState.InitialUltimatum(ire) == Redirect.Proceed) {
            tItem.OnPlayParent();

            foreach (Wizard w in wizards) {
                w.myUI.myControl.RefreshAll();

                if (tItem.Creator != w) {
                    MessageBox.Show(tItem.Creator.Name + " has revealed an item!");
                }
            }
            eventDispatcher.Notify(ire);
        }
    }

    public static void InvalidateAll() {
        foreach (UIControl ui in UIReference) {
            ui.myForm.Invalidate();
        }
    }

    public void OnEvent(UselessEffectEvent tEvent) {
        theStack.Remove(tEvent.me);
        durationEffects.Remove(tEvent.me);
    }

    internal static void KillEffect(Effect tEffect) {
        durationEffects.Remove(tEffect);
    }

    public void OnEvent(DestroyObjectEvent tEvent) {
        if (tEvent.DestroyedObject is IWall) {
            BoardRef.RemoveWall(tEvent.DestroyedObject as IWall);
        }
    }

    public static void RedrawAll() {
        foreach (UIControl UI in UIReference) {
            UI.myForm.Invalidate();
        }
    }

    public static Redirect InitialUltimatum(Event tEvent, bool afterEffects = false) {
        tEvent.IsAttempt = true;
        GameState.eventDispatcher.Notify(tEvent);
        while (tEvent.GetFlowControl() == Redirect.Halt) {
            System.Threading.Thread.Sleep(100);
        }

        if (tEvent.GetFlowControl() == Redirect.Skip) {
            MessageBox.Show("Something has been skipped due to a " + tEvent);
        }

        return tEvent.GetFlowControl();
    }

    internal static Redirect TinyUltimatum(Event tEvent) {
        tEvent.IsAttempt = true;
        eventDispatcher.Notify(tEvent);
        while (tEvent.GetFlowControl() == Redirect.Halt) {
            System.Threading.Thread.Sleep(100);
        }

        if (tEvent.GetFlowControl() == Redirect.Skip) {
            MessageBox.Show("Tiny skip");
        }

        return tEvent.GetFlowControl();
    }
}
}
