namespace WizWar1 {
class TurnStartEvent : Event {
    public Wizard NextWizard;
    public TurnStartEvent(Wizard tNextWizard) {
        NextWizard = tNextWizard;
    }
}
}
