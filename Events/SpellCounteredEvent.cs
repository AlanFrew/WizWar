namespace WizWar1 {
class SpellCounteredEvent : Event {
    public SpellCounteredEvent(Wizard tController, ISpell tCounter, ISpell tTarget) {
        Controller = tController;
        Source = tCounter;
        EventTarget = tTarget;
    }
}
}
