using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class MakeCreationEffect<T> : Effect where T : Creation {
    public T myCreation;

    public MakeCreationEffect(T tCreation) {
        myCreation = tCreation;
    }

    public override void OnRunChild() {
        (target as Square).creationsHere.Add(myCreation);
    }
}
}
