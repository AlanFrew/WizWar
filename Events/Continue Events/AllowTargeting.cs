using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class TargetingEvent : ContinueEvent {
    public ITarget targeted;

    public TargetingEvent(ITarget tTarget) {
        targeted = tTarget;
    }
}
}
