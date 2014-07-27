using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    interface IListener<X, T> where X : T where T : Event {
        void OnEvent(X tEvent);
    }
}
