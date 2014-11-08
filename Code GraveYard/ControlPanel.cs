using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace WizWar1 {
	public partial class ControlPanel2 : UserControl, IListener<NewEffectEvent, Event>, IListener<StateChangeEvent, Event>,
	IListener<ItemRevealEvent, Event>, IListener<CastEvent, Event>, IListener<ItemUseEvent, Event> {
		private int lastX;
		private int lastY;
		private BoardForm formReference;
		private List<ICard> deadCards;
		private UIControl myUI;
		internal int numberCardsLeft;
		internal bool passedPriority = false;
		//private WrappedListBox<ICard> HandOCards;
		private ICard selectedCard;

		//protected override void OnPaint(PaintEventArgs e) {
		//	base.OnPaint(e);

			

		//	foreach (Control c in Controls) {
		//		c.Invalidate(true);
		//	}
		//}

		private CardPreview cardPreview = new CardPreview();


		//public ControlPanel(UIControl tMyUI) {
		//	InitializeComponent();

		//	myUI = tMyUI;

		//	formReference = myUI.myBoard;

		//	deadCards = new List<ICard>();

			

		//	GameState.EventDispatcher.Register<NewEffectEvent>(this);
		//	GameState.EventDispatcher.Register<StateChangeEvent>(this);
		//	GameState.EventDispatcher.Register<ItemRevealEvent>(this);
		//	GameState.EventDispatcher.Register<CastEvent>(this);
		//	GameState.EventDispatcher.Register<ItemUseEvent>(this);

		//	CastButton.Click += CastButton_OnClick;
		//	CounteractionButton.Click += CounteractionButton_Click2;
		//	SpellTree.AfterSelect += SpellTree_SelectChanged;

		//	cardPreview.Visible = false;
		//}

		private void ControlPanel_Load(object sender, EventArgs e) {
			HandOCardsRefresh();

			Text = myUI.myWizard.ToString();
		}

		private void OnMouseMove(object sender, MouseEventArgs e) {
			if (e.X == lastX && e.Y == lastY) {
				return;
			}
			else {
				lastX = e.X;
				lastY = e.Y;
			}

			if (sender == HandOCards) {
				var listBox = (WrappedListBox<ICard>)sender;

				var point = new Point(e.X, e.Y);

				int hoverIndex = listBox.IndexFromPoint(point);

				if (hoverIndex >= 0
				&& hoverIndex < listBox.Items.Count
				&& point.Y >= 0
				&& point.Y <= listBox.ClientRectangle.Height) {
					selectedCard = (listBox.Items[hoverIndex] as ICard);


					if (selectedCard.WrappedCard is ISpell) {
						var castingTypes = String.Join("/", (selectedCard.WrappedCard as ISpell).ValidCastingTypes.Select(spellType => spellType.ToString()).ToArray());

						toolTip1.Show(castingTypes, listBox, e.X + 20, e.Y + 10);		//this is necessary to card preview to work
					}
					else {
						toolTip1.Show(selectedCard.WrappedCard.GetType().ToString().Replace("WizWar1.", string.Empty), listBox, e.X + 20, e.Y + 10);
					}

					cardPreview.Location = new Point(myUI.myBoard.Location.X + 265, myUI.myBoard.Location.Y + 40);

					cardPreview.CardText.Text = selectedCard.Description;
					cardPreview.CardTitle.Text = selectedCard.Name;

					cardPreview.Visible = true;

					HandOCards.Focus();
				}
				else {
					//toolTip1.Hide(HandOCards);

					//cardPreview.Visible = false;
					//toolTip1.Show("Inner" + sender.ToString(), HandOCards, e.X + 20, e.Y);
					//Invalidate();
				}
			}
			else {
				toolTip1.Hide(HandOCards);
				//toolTip1.Show("Outer" + sender.ToString(), HandOCards, e.X + 20, e.Y + 20);


				cardPreview.Visible = false;

				Invalidate();
			}
		}

		private void CastButton_OnClick(object sender, EventArgs e) {
			object tempref = HandOCards.SelectedItem.WrappedCard;

			//begin error checking
			if (myUI.State != UIState.Normal || !(tempref is ISpell)) {
				return;
			}

			if (myUI.State != UIState.CastQuery && (tempref as ISpell).IsOnlyValidCastingType(SpellType.Counteraction)) {
				return;
			}

			if (myUI.AttackedThisTurn == true && (tempref as ISpell).IsOnlyValidCastingType(SpellType.Attack)) {
				return;
			}
			//end error checking


			var checkMyType = (tempref as ISpell);

			//deadCards.Add((ICard)HandOCards.SelectedItem);
			//myUI.myWizard.HandRemove(HandOCards.SelectedItem as ICard);

			myUI.CurrentAimable = checkMyType;

			myUI.State = UIState.CastingSpell;

			if (myUI.CurrentAimable.ValidTargetTypes.Contains(TargetTypes.None)) {
				ContinueButton.Enabled = true;
			}

			if (checkMyType is INumberable) {
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
				GameState.Discard.Add(c);
			}
			deadCards.Clear();
			HandOCardsRefresh();
		}

		private void EndTurnButton_Click(object sender, EventArgs e) {
			//if (myUI.State == UIState.Normal || myUI.State == UIState.TurnComplete) {
			if (!myUI.myWizard.SettleHand()) {
				InstructionText.Text = "Discard cards until you are under your maximum hand size";
				return;
			}

			EndTurnButton.Enabled = false;		//TODO: Consider deleting this line altogether
			GameState.TurnCycle();
			
		}

		private void ShowInfoButton_Click(object sender, EventArgs e) {
			if (myUI.myInfo == null) {
				myUI.myInfo = new InfoPane(myUI);
				myUI.myInfo.StartPosition = FormStartPosition.Manual;
				myUI.myInfo.Location = new Point(myUI.myBoard.Location.X + 300, myUI.myBoard.Location.Y + 400);
				myUI.myInfo.Show();
			}
		}

		private void UseItemButton_Click(object sender, EventArgs e) {
			Object tempref = ListOItems.SelectedItem;

			//Items get a shortcut through the casting process
			//if (tempref is ItemCard) {
			//    (tempref as ItemCard).Creator = myUI.myWizard;

			//    (tempref as ItemCard).PlayTarget = myUI.myWizard;
			//    GameState.NewItem(tempref as ItemCard);
			//    myUI.myWizard.HandRemove(tempref as ICard);
			//    HandOCardsRefresh();
			//    return;
			//}

			//begin error checking
			if (myUI.State != UIState.Normal || tempref == null || tempref is Number || tempref is Trap) {
				return;
			}

			if (myUI.AttackedThisTurn == true && (tempref as Item).IsOnlyValidTargetTypeForItem(TargetTypes.Wizard)) {
				MessageBox.Show("You already attacked this turn");
				return;
			}
			//end error checking

			var checkMyType = (tempref as IItem);

			myUI.CurrentAimable = checkMyType;

			myUI.State = UIState.UsingItem;

			if (myUI.CurrentAimable.ValidTargetTypes.Contains(TargetTypes.None)) {
				ContinueButton.Enabled = true;
			}

			if (checkMyType is INumberable) {
				NumberButton.Enabled = true;
				numberCardsLeft++;
			}
		}

		private void CounteractionButton_Click2(object sender, EventArgs e) {
			if (SpellTree.SelectedNode == null || myUI.State != UIState.CastQuery || HandOCards.SelectedItem == null) {
				return;
			}

			var spellToCast = HandOCards.SelectedItem.WrappedCard as ISpell;

			if (spellToCast.IsValidCastingType(SpellType.Counteraction) == false) {
				return;
			}

			spellToCast.Caster = myUI.myWizard;

			var spellToTarget = SpellTree.SelectedNode.Tag as ITarget;

			if (spellToCast.IsValidTargetParent(spellToTarget)) {
				myUI.CurrentAimable = spellToCast;
				myUI.CurrentAimable.Target = spellToTarget;

				deadCards.Add((ICard)HandOCards.SelectedItem);
				myUI.myWizard.HandRemove(HandOCards.SelectedItem as ICard);
				HandOCardsRefresh();

				TargetValidated(spellToTarget, TargetTypes.Spell); //this second parameter is never used!
			}
			else {
				spellToCast.Caster = null;
			}
		}


		private void ContinueButton_Click(object sender, EventArgs e) {
			ContinueButton.Enabled = false;

			if (myUI.State == UIState.CastQuery) {
				passedPriority = true;

				bool everyonePassed = true;
				foreach (UIControl uic in GameState.UiReference) {
					if (uic.myBoard.passedPriority == false) {
						everyonePassed = false;
						break;
					}
				}

				if (everyonePassed) {
					GameState.RunTheStack();
					foreach (Wizard w in GameState.Wizards) {
						w.myUI.myBoard.StackOSpellsRefresh();
						//w.myUI.myControl.ListOItemsRefresh();
						w.myUI.myBoard.passedPriority = false;
						if (GameState.TheStack.Count != 0) {
							myUI.State = UIState.CastQuery;
							//w.myUI.myBoard.ContinueButton.Enabled = true;
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
				foreach (Wizard w in GameState.Wizards) {
					w.myUI.myBoard.passedPriority = false;
				}
				GameState.PriorityHolder = GameState.ActivePlayer.myUI;
			}
			else if (myUI.State == UIState.CastingSpell || myUI.State == UIState.UsingItem) {
				TargetValidated(null, TargetTypes.None);
			}
		}

		private void NumberButton_Click(object sender, EventArgs e) {
			if (HandOCards.SelectedItem.WrappedCard is Number && myUI.State == UIState.CastingSpell) {
				if (myUI.CurrentAimable is INumberable && numberCardsLeft > 0) {
					(myUI.CurrentAimable as INumberable).CardValue = (HandOCards.SelectedItem.WrappedCard as Number).Value;

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
			if (tEvent.IsAttempt || tEvent.EventTarget != myUI.myWizard) {
				return;
			}

			InstructionText.Text = string.Empty;

			//don't compare things to the "Previous" state literally!
			UIState testNewState;
			if (tEvent.NewState == UIState.Previous) {
				testNewState = myUI.PreviousState;
			}
			else {
				testNewState = tEvent.NewState;
			}

			//start by disabling buttons, then enable the ones used in the curent state
			EndTurnButton.Enabled = false;
			CastButton.Enabled = false;
			UseItemButton.Enabled = false;
			DrawButton.Enabled = false;
			DiscardButton.Enabled = false;
			DropItemButton.Enabled = false;
			ContinueButton.Enabled = false;
			CancelCastButton.Enabled = false;
			NumberButton.Enabled = false;
			DrawButton.Enabled = false;
			ContinueButton.BackColor = Color.LightYellow;

			//there are some duplicates here; it's organized this way for logical simplicity
			if (myUI.myWizard == GameState.ActivePlayer) {
				//if (tEvent.OldState == UIState.Normal) {               
				//    CastButton.Enabled = false;
				//    UseItemButton.Enabled = false;
				//    DropItemButton.Enabled = false;
				//    DrawButton.Enabled = false;
				//    DiscardButton.Enabled = false;
				//}

				if (tEvent.OldState == UIState.CastingSpell) {
					numberCardsLeft = 0;
					//CancelCastButton.Enabled = false;
					//NumberButton.Enabled = false;
				}

				//if (tEvent.OldState == UIState.Drawing) {
				//    DrawButton.Enabled = false;
				//}

				//if (tEvent.OldState == UIState.Discarding) {
				//    DiscardButton.Enabled = false;
				//}

				//enable new buttons
				if (testNewState == UIState.Normal) {
					DiscardButton.Enabled = true;
					EndTurnButton.Enabled = true;
				}

				if (testNewState == UIState.Drawing) {
					EndTurnButton.Enabled = true;

					if (myUI.myWizard.CardsDrawn < 2) {
						DrawButton.Enabled = true;
					}
				}

				if (testNewState == UIState.CastingSpell) {
					CancelCastButton.Enabled = true;
				}

				if (testNewState == UIState.TurnComplete) {
					//if (myUI.myWizard.CurrentHandSize > myUI.myWizard.MaxHandSize) {
					//    myUI.State = UIState.Discarding;

					//    return;
					//}

					EndTurnButton.Enabled = true;
				}

				if (testNewState == UIState.Normal || testNewState == UIState.Discarding) {
					if (myUI.myWizard.CurrentHandSize < myUI.myWizard.MaxHandSize) {
						DrawButton.Enabled = true;
					}

					DiscardButton.Enabled = true;
				}
			}

			if (testNewState == UIState.CastQuery) {
				ContinueButton.BackColor = Color.LightSalmon;
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

		public void OnEvent(ItemUseEvent tEvent) {
			passedPriority = false;
			ContinueButton.Enabled = true;
		}

		private void ListOItems_SelectedIndexChanged(object sender, EventArgs e) {
			if (ListOItems.SelectedIndex == -1) {
				UseItemButton.Enabled = false;
				DropItemButton.Enabled = false;
			}
			else {
				UseItemButton.Enabled = true;
				DropItemButton.Enabled = true;
			}
		}

		private void HandOCards_SelectedIndexChanged(object sender, EventArgs e) {
			CastButton.Enabled = false;

			if (HandOCards.SelectedItem != null) {
				if (myUI.myWizard == GameState.ActivePlayer) {
					if (myUI.State == UIState.Normal) {
						if (HandOCards.SelectedItem.WrappedCard is Spell) {
							if ((HandOCards.SelectedItem.WrappedCard as Spell).IsOnlyValidCastingType(SpellType.Counteraction)) {
								CastButton.Enabled = GameState.TheStack.Count != 0;
							}
							else {
								CastButton.Enabled = true;
							}
						}
					}
				}
			}
		}

		private void SpellTree_Clicked(object sender, EventArgs e) {
			SpellTree_SelectChanged(sender, new TreeViewEventArgs(SpellTree.SelectedNode));
		}

		private void SpellTree_SelectChanged(object sender, TreeViewEventArgs e) {
			if (SpellTree.SelectedNode == null) {
				CounteractionButton.Enabled = false;
				DiscardButton.Enabled = false;
			}
			else {
				if (myUI.State == UIState.CastQuery) {
					if (HandOCards.SelectedItem != null && HandOCards.SelectedItem.WrappedCard is ISpell) {
						//if ((HandOCards.SelectedItem as Spell).IsValidTargetParent(SpellTree.SelectedNode.Tag as ITarget)) {
						if ((HandOCards.SelectedItem.WrappedCard as ISpell).IsValidCastingType(SpellType.Counteraction)) {
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
			//if (myUI.CurrentAimable is INumberable)
			//{
			//    var card = myUI.CurrentAimable as INumberable;
			//    card.CardValue = Math.Min(card.CardValue, 1);
			//}

			if (myUI.CurrentAimable is ISpell) {
				TargetSpellValidated(tTarget, tTargetType);
			}
			else {
				TargetItemValidated(tTarget, tTargetType);
			}
		}

		internal void TargetSpellValidated(ITarget tTarget, TargetTypes tTargetType) {
			var spell = myUI.CurrentAimable as ISpell;
			spell.Caster = myUI.myWizard;
			spell.Target = tTarget;

			if (spell.ActiveSpellType == SpellType.Attack) {
				if (myUI.AttackedThisTurn == true) {
					CancelButton_Click(this, new EventArgs());
					return;
				}
				else {
					myUI.AttackedThisTurn = true;
				}
			}

			myUI.myWizard.HandRemove(spell.OriginalCard);

			deadCards.Add(spell.OriginalCard);

			foreach (ICard c in deadCards) {
				GameState.Discard.Add(c.RecursiveCopy());
			}

			deadCards.Clear();

			GameState.NewSpell(spell);
		}

		internal void TargetItemValidated(ITarget tTarget, TargetTypes tTargetType) {
			var item = myUI.CurrentAimable as IItem;
			item.Target = tTarget;

			if (item.Target is Wizard) {
				if (myUI.AttackedThisTurn) {
					CancelButton_Click(this, new EventArgs());
					return;
				}
				else {
					myUI.AttackedThisTurn = true;
				}
			}

			foreach (ICard c in deadCards) {
				GameState.Discard.Add(c.RecursiveCopy());
			}
			deadCards.Clear();

			GameState.NewSpell(item.UseItem());
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
			foreach (IStackable s in GameState.TheStack) {
				var tn = new TreeNode();
				tn.Tag = s;
				tn.Text = s.ToString();
				SpellTree.Nodes.Add(tn);

				if (!(s is ISpellOrItemUsage)) {
					return;
				}

				foreach (Effect e in (s as ISpellOrItemUsage).EffectsWaiting) {
					var child = new TreeNode();
					child.Tag = e;
					child.Text = e.ToString();
					tn.Nodes.Add(child);
				}
			}

			CounteractionButton.Enabled = false;

			ContinueButton.Enabled = SpellTree.Nodes.Count > 0;
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
				GameState.Deck.DealCards(myUI.myWizard, 1);
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

			if (myUI.myWizard.CurrentHandSize <= myUI.myWizard.MaxHandSize) {
				EndTurnButton.Enabled = true;

				if (myUI.myWizard.CurrentHandSize < myUI.myWizard.MaxHandSize && myUI.myWizard.CardsDrawn < 2) {
					DrawButton.Enabled = true;

					myUI.myWizard.CardsDiscardedByChoice++;
				}

				if (myUI.myWizard.CardsDiscardedByChoice >= 2) {
					myUI.State = UIState.Drawing;
				}
			}

			HandOCardsRefresh();
		}
	}
}