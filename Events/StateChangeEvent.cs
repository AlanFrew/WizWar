using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class StateChangeEvent : Event {
    public UIState OldState;
    public UIState NewState;

    public StateChangeEvent(UIState tOldState, UIState tNewState) {
        OldState = tOldState;
        NewState = tNewState;
    }
}
}
