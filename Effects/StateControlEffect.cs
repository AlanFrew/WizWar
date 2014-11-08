using System.Linq;

namespace WizWar1 {
class StateControlEffect : Effect, IListener<StateChangeEvent, Event> {
    public UIState WatchState;
    public UIState DesiredState;
    public UIControl ControlTarget;

    public StateControlEffect(UIState tWatchState, UIState tDesiredState, UIControl tControlTarget) {
        WatchState = tWatchState;
        DesiredState = tDesiredState;
        ControlTarget = tControlTarget;

        GameState.EventDispatcher.Register(this);
        var duration = markers.First(marker => marker is DurationBasedMarker) as DurationBasedMarker;
        duration.DurationBasedValue = 1000;
    }

    public void OnEvent(StateChangeEvent tEvent) {
        if (tEvent.OldState == WatchState) {
            ControlTarget.State = DesiredState;
            GameState.EventDispatcher.Deregister(this);

            var duration = markers.First(marker => marker is DurationBasedMarker) as DurationBasedMarker;
            duration.DurationBasedValue = 0;
        }
    }

    public override void OnRunChild() {
        //no effect
    }
}
}
