using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class StateControlEffect : Effect, IListener<StateChangeEvent, Event> {
    public UIState WatchState;
    public UIState DesiredState;
    public UIControl ControlTarget;

    public StateControlEffect(UIState tWatchState, UIState tDesiredState, UIControl tControlTarget) {
        WatchState = tWatchState;
        DesiredState = tDesiredState;
        ControlTarget = tControlTarget;

        GameState.eventDispatcher.Register(this);
        duration = 1000;
    }

    public void OnEvent(StateChangeEvent tEvent) {
        if (tEvent.OldState == WatchState) {
            ControlTarget.State = DesiredState;
            GameState.eventDispatcher.Deregister(this);
            duration = 0;
        }
    }

    public override void OnRunChild() {
        //no effect
    }
}
}
