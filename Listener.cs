using System;

namespace WizWar1 {
abstract class Listener {
    public Type MyType { get; set; }

    abstract public void OnEvent(Event tEvent);
}
}