namespace WizWar1 {
class ItemGainEvent : Event {
    public IItem NewItem;

    public ItemGainEvent(IItem tNewItem) {
        NewItem = tNewItem;
    }
}
}
