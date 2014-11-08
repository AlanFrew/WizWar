namespace WizWar1 {
class PowerRunEffect : Effect, IListener<MoveEvent, Event> {
    public PowerRunEffect() {
        markers.Add(new DurationBasedMarker());
    }

    public void OnEvent(MoveEvent moveEvent) {
        moveEvent.Mover.TakeDamage(new DamageEffect(1, DamageType.Physical));

        moveEvent.Mover.MovesLeft++;
    }

    public override void OnRunChild() {
        GameState.EventDispatcher.Register(this);
    }
}
}