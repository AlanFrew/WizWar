using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
abstract class Listener {
    private Type myType;
    public Type MyType {
        get {
            return myType;
        }
        set {
            myType = value;
        }
    }

    abstract public void OnEvent(Event tEvent);

}
}