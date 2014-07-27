using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class TargetedEvent : Event {
    public ITarget TargetedObject;

    public TargetedEvent(ITarget tTargetedObject) {
        TargetedObject = tTargetedObject;
    }
}
}
