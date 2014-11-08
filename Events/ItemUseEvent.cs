namespace WizWar1 {
	public class ItemUseEvent : Event {
		internal IItem ItemBeingUsed;

		internal ItemUseEvent(IItem tItemBeingUsed) {
			ItemBeingUsed = tItemBeingUsed;
		}
	}
}