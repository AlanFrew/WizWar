namespace WizWar1 {
	class ExtraActionEffect : Effect {
		public override void OnRunChild() {
			//(target as Wizard).myUI.myControl.numberCardsLeft++;
			(target as Wizard).myUI.myBoard.numberCardsLeft++;
		}
	}
}