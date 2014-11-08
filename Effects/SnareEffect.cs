namespace WizWar1 {
class SnareEffect : Effect, IListener<MoveEvent, Event> {

    public SnareEffect(int tDuration) {
        var duration = new DurationBasedMarker {DurationBasedValue = tDuration};
        markers.Add(duration);
    }

    public void OnEvent(MoveEvent tEvent) {
        if (tEvent.Mover == target) {
            tEvent.SetFlowControl(Redirect.Skip, 1.0);
        }
    }

    public override void OnRunChild()
    {
        GameState.EventDispatcher.Register(this);
    }
}
}
