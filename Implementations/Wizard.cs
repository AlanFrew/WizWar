using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;

namespace WizWar1
{

class Wizard : Locatable, IDamageable {
    public UIControl myUI;

    public new double X {
        get { return x; }
        set {
            x = value;
            foreach (IItem i in inventory) {
                i.X = x;
            }
        }
    }

    public new double Y {
        get { return y; }
        set {
            foreach (IItem i in inventory) {
                i.Y = y;
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
    public int CardsDiscarded {
        get {
            return cardsDrawn;
        }
        set {
            cardsDrawn = value;
        }
    }

    public bool SetPosition(int tX, int tY) {
        try {
            Square temp = GameState.BoardRef.At(tX, tY);
            foreach (ICreation c in temp.creationsHere) {
                if (c is SolidStone) {
                    return false;
                }
            }
            x = tX;
            y = tY;

        }
        catch (ArgumentOutOfRangeException) {
            return false;
        }
        
        return true;
    }

    public bool MoveOne(Direction tDirection) {
        Board b = GameState.BoardRef;
        Square current = b.At(X, Y);
        Square next = b.At(X, Y).GetNeighbor(tDirection);
        IWall w = b.LookForWall(current, next);

        if (w != null) {
            if (w.IsPassable(this) == false) {
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

        X = next.X;
        Y = next.Y;

        foreach (ICreation c in next.creationsHere) {
            if (c is Obstruction) {
                (c as Obstruction).OnEnterParent(this);
            }
        }

        return true;
    }


    //public bool MoveOneOld(Direction tDirection) {
    //    int[] key = new int[4];
    //    StringBuilder keyString = new StringBuilder();
    //    switch(tDirection) {
    //        case Direction.East:
    //            key[0] = (int)x;
    //            key[1] = (int)y;
    //            key[2] = Library.IndexFixer((int)(x + 1), GameState.BoardReference.board.Length);
    //            key[3] = (int)y;
    //            if (key[0] > key[2]) {
    //                Library.Swap(ref key[0], ref key[2]);
    //            }
    //            foreach (int i in key) {
    //                keyString.Append(i);
    //                keyString.Append(' ');
    //            }
    //            keyString.Length -= 1;
    //            break;
    //        case Direction.West:
    //            key[0] = Library.IndexFixer((int)(x - 1), GameState.BoardReference.board.Length);
    //            key[1] = (int)y;
    //            key[2] = (int)x;
    //            key[3] = (int)y;
    //            if (key[0] > key[2]) {
    //                Library.Swap(ref key[0], ref key[2]);
    //            }
    //            foreach (int i in key) {
    //                keyString.Append(i);
    //                keyString.Append(' ');
    //            }
    //            keyString.Length -= 1;
    //            break;
    //        case Direction.South:
    //            key[0] = (int)x;
    //            key[1] = (int)y;
    //            key[2] = (int)x;
    //            key[3] = Library.IndexFixer((int)(y + 1), GameState.BoardReference.board[(int)x].Length);
    //            if (key[1] > key[3]) {
    //                Library.Swap(ref key[1], ref key[3]);
    //            }
    //            foreach (int i in key) {
    //                keyString.Append(i);
    //                keyString.Append(' ');
    //            }
    //            keyString.Length -= 1;
    //            break;
    //        case Direction.North:
    //            key[0] = (int)x;
    //            key[1] = Library.IndexFixer((int)(y - 1), GameState.BoardReference.board[(int)x].Length);
    //            key[2] = (int)x;
    //            key[3] = (int)y;
    //            if (key[1] > key[3]) {
    //                Library.Swap(ref key[1], ref key[3]);
    //            }
    //            foreach (int i in key) {
    //                keyString.Append(i);
    //                keyString.Append(' ');
    //            }
    //            keyString.Length -= 1;
    //            break;
    //    }

    //    String s = keyString.ToString();

    //    //foreach(KeyValuePair<int[],IWall> w in GameState.BoardReference.horizontal_walls) {
    //    //    String temp = w.ToString();
    //    //}
    //    Dictionary<String, IWall> shoriz = GameState.BoardReference.horizontal_walls;
    //    Dictionary<String, IWall> svert = GameState.BoardReference.vertical_walls;

    //    IWall inTheWay = null;
    //    if (GameState.BoardReference.horizontal_walls.ContainsKey(s)) {
    //        inTheWay = GameState.BoardReference.horizontal_walls[s];
    //    }
    //    else if (GameState.BoardReference.vertical_walls.ContainsKey(s)) {
    //        inTheWay = GameState.BoardReference.vertical_walls[s];
    //    }

    //    if (inTheWay != null && inTheWay.IsPassable(GameState.active_player) == false) {
    //        return false;
    //    }

    //    int oldX = (int)x;
    //    int oldY = (int)y;
    //    switch (tDirection) {
    //        case Direction.East:
    //            x = Library.IndexFixer((int)x + 1, GameState.BoardReference.board.Length);
    //            break;
    //        case Direction.West:
    //            x = Library.IndexFixer((int)x - 1, GameState.BoardReference.board.Length);
    //            break;
    //        case Direction.North:
    //            y = Library.IndexFixer((int)y - 1, GameState.BoardReference.board[(int)x].Length);
    //            break;
    //        case Direction.South:
    //            y = Library.IndexFixer((int)y + 1, GameState.BoardReference.board[(int)x].Length);
    //            break;
    //    }

    //    //GameState.moveSource.notify(new MoveEvent(GameState.active_player, new Point(oldX, oldY), new Point((int)x, (int)y)));

    //    return true;
    //}

    

    private String name;
    public String Name {
        get { return name; }
        set { name = value; }
    }

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
        myUI.myControl.HandOCardsRefresh();
    }

    public void HandRemove(ICard tCard) {
        hand.Remove(tCard);
        myUI.myControl.HandOCardsRefresh();
    }

    public void HandAddRange(List<ICard> tList) {
        hand.AddRange(tList);
        myUI.myControl.HandOCardsRefresh();
    }

    private List<IItem> inventory;
    public IList<IItem> Inventory {
        get {
            return inventory.AsReadOnly();
        }
    }

    public List<Effect> personalEffects;

    public Wizard(UIControl tMyUI) {
        myUI = tMyUI;

        activeTargetType = TargetTypes.Wizard;
        hit_points = 15;
        inventory = new List<IItem>();
        hand = new List<ICard>();
        personalEffects = new List<Effect>();
        MaxHandSize = 20;
    }

    public Wizard(UIControl tMyUI, String tName, int tX, int tY) : this(tMyUI) {
        name = tName;
        x = tX;
        y = tY;
        GameState.allElements.Add(this, this);
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

    public void giveItem(IItem tItem) {
        GameState.eventDispatcher.Notify(Event.New<ItemGainEvent>(false, new ItemGainEvent(tItem)));
        inventory.Add(tItem);
        tItem.OnGainParent(this);
        myUI.myControl.ListOItemsRefresh();
    }

    public bool dropItem(IItem tItem) {
        if (loseItem(tItem) == true) {
            tItem.Location = GameState.BoardRef.At(X, Y);
            return true;
        }
        return false;
    }

    public bool loseItem(IItem tItem) {
        if (inventory.Contains(tItem)) {
            inventory.Remove(tItem);
            tItem.OnLossParent(this);
            myUI.myControl.ListOItemsRefresh();
            return true;
        }

        return false;
    }

    public bool pickUpItem(IItem tItem) {
        if (tItem is Treasure) {
            if (hasItemType(tItem)) {
                return false;
            }
        }
        
        if (GameState.BoardRef.At(X, Y).ItemsHere.Contains(tItem) == false) {
            return false;
        }

        giveItem(tItem);
        this.ContainingSquare.RemoveItem(tItem);
        GameState.TurnCycle();
        return true;
    }
            


    //private EventSource<MoveEvent, MoveEvent> moveSource;

    public void OnEvent(Event e) {
        if (e is LostTurnEvent) {
            GameState.TurnCycle();
        }
    }

    //public void CastSpell(ICard tCard) {
    //    //if (hand.Remove(tCard) == false) {
    //    //    throw new UnreachableException();
    //    //}

    //    GameState.discard.Add(tCard);
    //    //tSpell.OnResolution();
    //    try {
    //        //(tCard as ISpell).BecomeSpell();
    //        (tCard as ISpell).OnCast();
    //        GameState.NewSpell(tCard as ISpell);
    //    }
    //    catch (NullReferenceException) {
    //        //must be an item
    //        (tCard as IItem).OnPlayParent();
    //    }

        
    //}

    public void TakeDamage(DamageEffect d) {
        hit_points -= d.Amount;
        if (hit_points <= 0) {
            OnDeath(d.Caster);
        }
    }

    public void BeginTurn() {
        movesLeft = 3;
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
    }

    public void EndTurn() {
        GameState.TurnCycle();
    }

    public void OnDeath(Wizard killer) {
        GameState.wizards.Remove(this);
        foreach (IItem i in inventory) {
            GameState.BoardRef.At((int)x,(int)y).AddItem(i);
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

    internal void SettleHand() {
        if (CurrentHandSize <= MaxHandSize) {
            return;
        }
        
        myUI.State = UIState.Discarding;

        //if (GameState.InitialUltimatum(Event.New<SettleHandEvent>(true, new SettleHandEvent())) == Redirect.Proceed) {
        //    hand.Remove(Card.NewCard(wc.AdditionalInfo));
        //}

        //SettleHand();
    }
}
}
