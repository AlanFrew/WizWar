using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WizWar1 {
class MoveEvent : Event {
    public Wizard Mover = null;
    public Point Old;
    public Point New;

    public MoveEvent(Wizard tMover, Point tOld, Point tNew) {
        Mover = tMover;
        Old = tOld;
        New = tNew;
    }
}
}
