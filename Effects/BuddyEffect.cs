using System;
using System.Linq;

namespace WizWar1 {
class BuddyEffect : Effect, IListener<TargetingEvent, Event>, IListener<TargetedEvent, Event> {
    private DurationBasedMarker duration;

    public BuddyEffect() {
        duration = new DurationBasedMarker(Int32.MaxValue);

        markers.Add(duration);

        GameState.EventDispatcher.Register<TargetingEvent>(this);
        GameState.EventDispatcher.Register<TargetedEvent>(this);
    }

    public override void OnRunChild() {
        duration.DurationBasedValue++;
    }

    public void OnEvent(TargetingEvent tEvent) {
        if (tEvent.Controller == this.target && tEvent.targeted == this.Caster) {
            tEvent.SetFlowControl(Redirect.Skip, Double.MaxValue);
        }
    }

    public void OnEvent(TargetedEvent tEvent) {
        if (tEvent.TargetedObject == target) {
            GameState.KillEffect(this);
        }
    }
}
}