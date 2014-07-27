using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class StaticListenerHelper<X> where X : Event {

    virtual public void OnEvent(Event tEvent) {
        //empty
    }
}
}
