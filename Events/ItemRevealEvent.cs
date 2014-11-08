namespace WizWar1 {
	public class ItemRevealEvent : Event {
		internal IItem NewItem;

		internal ItemRevealEvent(IItem tNewItem) {
			NewItem = tNewItem;

			NewItem.Creator = GameState.ActivePlayer;
		}
	}
}