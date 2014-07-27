using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWar1 {
internal partial class ControlPanel : Form, IListener<NewEffectEvent, Event>, IListener<StateChangeEvent, Event>, 
IListener<ItemRevealEvent, Event>, IListener<CastEvent, Event> {
    private Form1 formReference;
    private List<ICard> deadCards;
    private UIControl myUI;
    internal int numberCardsLeft;
    internal bool passedPriority = false;

    public ControlPanel(UIControl tMyUI) {
        InitializeComponent();

        myUI = tMyUI;
        formReference = myUI.myForm;
        deadCards = new List<ICard>();
        GameState.eventDispatcher.Register<NewEffectEvent>(this);
        GameState.eventDispatcher.Register<StateChangeEvent>(this);
        GameState.eventDispatcher.Register<ItemRevealEvent>(this);
        GameState.eventDispatcher.Register<CastEvent>(this);
        CastButton.Click += new EventHandler(CastButton_OnClick);
        CounteractionButton.Click += new EventHandler(CounteractionButton_Click2);
        SpellTree.AfterSelect += new TreeViewEventHandler(SpellTree_SelectChanged);
    }

    private void ControlPanel_Load(object sender, EventArgs e) {
        HandOCardsRefresh();
        this.Text = myUI.myWizard.ToString();
    }

    private void CastButton_OnClick(object sender, EventArgs e) {
        Object tempref = HandOCards.SelectedItem;

        //Items get a shortcut through the casting process
        if (tempref is ItemCard) {
            (tempref as ItemCard).Creator = myUI.myWizard;

            (tempref as ItemCard).PlayTarget = myUI.myWizard;
            GameState.NewItem(tempref as ItemCard);
            myUI.myWizard.HandRemove(tempref as ICard);
            HandOCardsRefresh();
            return;
        }

        //begin error checking
        if (myUI.State != UIState.Normal || tempref == null || tempref is Number || tempref is Trap) {
            return;
        }

        if (myUI.State != UIState.CastQuery && (tempref as Spell).IsOnlyValidCastingType(SpellType.Counteraction)) {
            return;
        }

        if (myUI.AttackedThisTurn == true && (tempref as Spell).IsOnlyValidCastingType(SpellType.Attack)) {
            return;
        }
        //end error checking
        

        ISpell checkMyType = (tempref as ISpell);

        deadCards.Add((ICard)HandOCards.SelectedItem);
        myUI.myWizard.HandRemove(HandOCards.SelectedItem as ICard);

        myUI.SpellToCast = checkMyType;

        myUI.State = UIState.CastingSpell;
        if (checkMyType.AcceptsNumber == true) {
            NumberButton.Enabled = true;
            numberCardsLeft++;
        }
    }

    private void QuerySquareButton_Click(object sender, EventArgs e) {
        myUI.State = UIState.QueryingSquare;
    }

    private void CancelButton_Click(object sender, EventArgs e) {
        if (myUI.State == UIState.CastingSpell) {
            myUI.State = UIState.Normal;
        }

        myUI.myWizard.HandAddRange(deadCards);
        foreach (ICard c in deadCards) {
            GameState.discard.Add(Card.NewCard(c.GetType()));
        }
        deadCards.Clear();
        HandOCardsRefresh();
    }

    private void EndTurnButton_Click(object sender, EventArgs e) {
        if (myUI.State == UIState.Normal || myUI.State == UIState.TurnComplete) {
            GameState.TurnCycle();
            EndTurnButton.Enabled = false;
        }
    }

    private void ShowInfoButton_Click(object sender, EventArgs e) {
        if (myUI.myInfo == null) {
            myUI.myInfo = new InfoPane(myUI);
            myUI.myInfo.StartPosition = FormStartPosition.Manual;
            myUI.myInfo.Location = new Point(myUI.myForm.Location.X + 300, myUI.myForm.Location.Y + 400);
            myUI.myInfo.Show();
        }
    }

    private void UseItemButton_Click(object sender, EventArgs e) {
        UseItem();
    }

    private void CounteractionButton_Click2(object sender, EventArgs e) {
        if (SpellTree.SelectedNode == null || myUI.State != UIState.CastQuery || HandOCards.SelectedItem == null) {
            return;
        }
        
        ISpell spellToCast = HandOCards.SelectedItem as ISpell;

        if (spellToCast.IsValidCastingType(SpellType.Counteraction) == false) {
            return;
        }

        ITarget spellToTarget = SpellTree.SelectedNode.Tag as ITarget;
        
        if (spellToCast.IsValidSpellTargetParent(spellToTarget, myUI.myWizard)) {
            myUI.SpellToCast = spellToCast;
            myUI.SpellToCast.SpellTarget = spellToTarget;

            deadCards.Add((ICard)HandOCards.SelectedItem);
            myUI.myWizard.HandRemove(HandOCards.SelectedItem as ICard);
            HandOCardsRefresh();

            TargetValidated(spellToTarget, TargetTypes.Spell);  //this second parameter is never used!
        }
    }


    private void ContinueButton_Click(object sender, EventArgs e) {
        ContinueButton.Enabled = false;

        if (myUI.State == UIState.CastQuery) {
            passedPriority = true;

            bool everyonePassed = true;
            foreach (UIControl uic in GameState.UIReference) {
                if (uic.myControl.passedPriority == false) {
                    everyonePassed = false;
                    break;
                }
            }

            if (everyonePassed) {      
                GameState.RunSpells();
                foreach (Wizard w in GameState.wizards) {
                    w.myUI.myControl.StackOSpellsRefresh();
                    w.myUI.myControl.passedPriority = false;   
                    if (GameState.theStack.Count != 0) {
                        myUI.State = UIState.CastQuery;
                        w.myUI.myControl.ContinueButton.Enabled = true;
                    }
                    else {
                        if (w.myUI.PreviousState == UIState.Locked) {
                            w.myUI.State = UIState.Locked;
                        }
                        else {
                            w.myUI.State = UIState.Normal;
                        }
                    }
                }
                GameState.RedrawAll();
                GameState.PriorityHolder = GameState.ActivePlayer.myUI;
            }
            else if (GameState.PriorityHolder == myUI) {
                GameState.PriorityHolder = GameState.NextPriorityHolder;
            }
        }
        else if (myUI.State == UIState.TurnComplete) {
            GameState.TurnCycle();
            foreach (Wizard w in GameState.wizards) {
                w.myUI.myControl.passedPriority = false;
            }
            GameState.PriorityHolder = GameState.ActivePlayer.myUI;
        }
    }

    private void NumberButton_Click(object sender, EventArgs e) {
        if (HandOCards.SelectedItem is Number && myUI.State == UIState.CastingSpell) {
            if (myUI.SpellToCast.AcceptsNumber == true && numberCardsLeft > 0) {
                myUI.SpellToCast.CardValue += (HandOCards.SelectedItem as Number).Value;

                numberCardsLeft--;
                if (numberCardsLeft < 1) {
                    NumberButton.Enabled = false;
                }
            }
            deadCards.Add((ICard)HandOCards.SelectedItem);
            myUI.myWizard.HandRemove(HandOCards.SelectedItem as ICard);
            HandOCardsRefresh();
        }
    }

    private void DropItemButton_Click(object sender, EventArgs e) { //hopefully only enabled when there is something selected to drop
        myUI.myWizard.dropItem(ListOItems.SelectedItem as IItem);
    }

    public void OnEvent(NewEffectEvent tEvent) {
        if (tEvent.IsAttempt == false) {
            if (tEvent.myEffect.target == myUI.myWizard) {
                MessageBox.Show("New effects have entered the stack that target you.");
                RefreshAll();
            }
        }
    }

    public void OnEvent(StateChangeEvent tEvent) {
        if (tEvent.IsAttempt == true) {
            return;
        }

        //don't compare things to the "Previous" state literally!
        UIState testNewState;
        if (tEvent.NewState == UIState.Previous) {
            testNewState = myUI.PreviousState;
        }
        else {
            testNewState = tEvent.NewState;
        }

        //there are some duplicates here; it's organized this way for logical simplicity
        if (myUI.myWizard == GameState.ActivePlayer) {
            //disable old buttons
            if (tEvent.OldState == UIState.Normal) {
                EndTurnButton.Enabled = false;
                CastButton.Enabled = false;
                UseItemButton.Enabled = false;
                DropItemButton.Enabled = false;
                DrawButton.Enabled = false;
                DiscardButton.Enabled = false;
            }

            if (tEvent.OldState == UIState.TurnComplete) {
                EndTurnButton.Enabled = false;
            }

            if (tEvent.OldState == UIState.CastingSpell) {
                numberCardsLeft = 0;
                CancelCastButton.Enabled = false;
                NumberButton.Enabled = false;
            }

            if (tEvent.OldState == UIState.Drawing) {
                DrawButton.Enabled = false;
                EndTurnButton.Enabled = false;
            }

            if (tEvent.OldState == UIState.Discarding) {
                DiscardButton.Enabled = false;
            }

            //enable new buttons
            if (testNewState == UIState.Normal) {
                EndTurnButton.Enabled = true;
            }

            if (testNewState == UIState.CastingSpell) {
                CancelCastButton.Enabled = true;
            }

            if (testNewState == UIState.TurnComplete) {
                EndTurnButton.Enabled = true;
            }

            if (testNewState == UIState.Normal || testNewState == UIState.Drawing) {
                DrawButton.Enabled = true;
                EndTurnButton.Enabled = true;
            }

            if (testNewState == UIState.Normal || testNewState == UIState.Discarding) {
                DiscardButton.Enabled = true;
            }
        }
        else {
            EndTurnButton.Enabled = false;
        }
    }

    public void OnEvent(ItemRevealEvent tEvent) {
        if (tEvent.EventTarget == myUI.myWizard) {
            ListOItemsRefresh();
        }
    }

    public void OnEvent(CastEvent tEvent) {
        passedPriority = false;
        ContinueButton.Enabled = true;
    }

    private void ListOItems_SelectedIndexChanged(object sender, EventArgs e) {
        if (ListOItems.SelectedIndex == -1) {
            DropItemButton.Enabled = false;
        }
        else {
            DropItemButton.Enabled = true;
        }
    }

    private void HandOCards_SelectedValueChanged(object sender, EventArgs e) {
        if (HandOCards.SelectedItem != null) {
            if (myUI.myWizard == GameState.ActivePlayer) {
                if (myUI.State == UIState.Normal) {
                    CastButton.Enabled = true;
                    return;
                }
            }
        }
        CastButton.Enabled = false;
    }

    private void SpellTree_SelectChanged(object sender, TreeViewEventArgs e) {
        if (SpellTree.SelectedNode == null) {
            CounteractionButton.Enabled = false;
            DiscardButton.Enabled = false;
        }
        else {
            if (myUI.State == UIState.CastQuery) {
                if (HandOCards.SelectedItem is Spell) {
                    if ((HandOCards.SelectedItem as Spell).IsValidSpellTargetParent(SpellTree.SelectedNode.Tag as ITarget, myUI.myWizard)) {
                        CounteractionButton.Enabled = true;
                    }
                }
            }
            else if (myUI.State == UIState.Discarding) {
                DiscardButton.Enabled = true;
            }
        }
    }

    internal void TargetValidated(ITarget tTarget, TargetTypes tTargetType) {
        ISpell spell = myUI.SpellToCast;
        spell.Caster = myUI.myWizard;
        spell.SpellTarget = tTarget;

        if (myUI.SpellToCast.ActiveSpellType == SpellType.Attack) {
            if (myUI.AttackedThisTurn == true) {
                CancelButton_Click(this, new EventArgs());
                return;
            }
            else {
                myUI.AttackedThisTurn = true;
            }
        }

        foreach (ICard c in deadCards) {
            GameState.discard.Add(Card.NewCard(c.GetType()));
        }
        deadCards.Clear();
        GameState.NewSpell(spell); 
    }

    private void UseItem() {
        if (ListOItems.SelectedItem != null) {
            (ListOItems.SelectedItem as Item).OnActivationChild();
        }
    }

    public void RefreshAll() {
        HandOCardsRefresh();

        StackOSpellsRefresh();

        ListOItemsRefresh();
    }

    public void HandOCardsRefresh() {
        HandOCards.Items.Clear();
        foreach (ICard card in myUI.myWizard.Hand) {
            HandOCards.Add(card);
        }
        CastButton.Enabled = false;
    }

    public void StackOSpellsRefresh() {
        //the stack is organized by spell, but individual effects can be targeted. Hence the tree structure
        SpellTree.Nodes.Clear();
        foreach (IStackable s in GameState.theStack) {
            TreeNode tn = new TreeNode();
            tn.Tag = s;
            tn.Text = s.ToString();
            SpellTree.Nodes.Add(tn);
            if (s is Spell == false) {
                return;
            }

            foreach (Effect e in (s as Spell).EffectsWaiting) {
                TreeNode child = new TreeNode();
                child.Tag = e;
                child.Text = e.ToString();
                tn.Nodes.Add(child);
            }
        }

        CounteractionButton.Enabled = false;

        if (SpellTree.Nodes.Count > 0) {
            ContinueButton.Enabled = true;
        }
        else {
            ContinueButton.Enabled = false;
        }
    }

    public void ListOItemsRefresh() {
        ListOItems.Items.Clear();
        foreach (IItem i in myUI.myWizard.Inventory) {
            ListOItems.Items.Add(i);
        }
        UseItemButton.Enabled = false;
    }

    private void DrawButton_Click(object sender, EventArgs e) {
        if (myUI.myWizard.CurrentHandSize < myUI.myWizard.MaxHandSize && myUI.myWizard.CardsDrawn < 2) {
            GameState.deck.dealCards(myUI.myWizard, 1);
            myUI.State = UIState.Drawing;
        }

        if (myUI.myWizard.CurrentHandSize >= myUI.myWizard.MaxHandSize || myUI.myWizard.CardsDrawn >= 2) {
            DrawButton.Enabled = false;
        }

        HandOCardsRefresh();
    }

    private void DiscardButton_Click(object sender, EventArgs e) {
        if (HandOCards.SelectedItem == null) {
            return;
        }

        myUI.State = UIState.Discarding;
        myUI.myWizard.TakeCard(HandOCards.SelectedItem as ICard);
        myUI.myWizard.CardsDiscarded++;

        if (myUI.myWizard.CardsDiscarded >= 2 && myUI.myWizard.CurrentHandSize >= myUI.myWizard.MaxHandSize) {
            DiscardButton.Enabled = false;
        }

        HandOCardsRefresh();
    }
}
}
