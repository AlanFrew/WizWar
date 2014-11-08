namespace WizWar1 {
class MasterKeyItem : Item, IListener<PassthroughEvent, Event> {
    public MasterKeyItem() {
        ValidTargetTypes.Add(TargetTypes.None);

        GameState.EventDispatcher.Register(this);
    }

    public void OnEvent(PassthroughEvent tEvent)
    {
        if (tEvent.Wizard == Carrier && tEvent.Wall is Door) {
            tEvent.SetFlowControl(Redirect.Proceed, 1.0);
        }
    }
}
}
