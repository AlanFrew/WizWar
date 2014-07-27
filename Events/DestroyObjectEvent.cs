using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DestroyObjectEvent : Event {
    public ITarget DestroyedObject;

    public DestroyObjectEvent(ITarget tDestroyedObject) {
        DestroyedObject = tDestroyedObject;
    }
}
}
