using Library;

namespace WizWar1 {
class MoveEvent : Event {
    public Wizard Mover = null;
    public DoublePoint Old;
	 public DoublePoint New;

	 public MoveEvent(Wizard tMover, DoublePoint tOld, DoublePoint tNew) {
        Mover = tMover;
        Old = tOld;
        New = tNew;
    }
}
}
