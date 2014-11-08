namespace WizWar1 {
	class DrawEvent : Event {
		public ICard CardDrawn;
		public Wizard Drawer;

		public DrawEvent(ICard tCard, Wizard tDrawer) {
			CardDrawn = tCard;
			Drawer = tDrawer;
		}
	}
}