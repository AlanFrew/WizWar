namespace WizWar1 {
class SpeedStoneItem : Item, IListener<TurnStartEvent, Event> {
    public SpeedStoneItem() {
        GameState.EventDispatcher.Register<TurnStartEvent>(this);
    }

    public void OnEvent(TurnStartEvent tse) {
        if (tse.NextWizard == Carrier) {
            Carrier.MovesLeft++;
        }
    }
}
}
