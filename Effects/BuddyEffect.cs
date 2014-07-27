using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class BuddyEffect : Effect, IListener<TargetingEvent, Event>, IListener<TargetedEvent, Event> {
    public BuddyEffect() {
        duration = Int32.MaxValue - 1;
        GameState.eventDispatcher.Register<TargetingEvent>(this);
        GameState.eventDispatcher.Register<TargetedEvent>(this);
    }

    public override void OnRunChild() {
        duration++;
    }

    public void OnEvent(TargetingEvent tEvent) {
        if (target == tEvent.Controller && tEvent.targeted == Caster) {
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
