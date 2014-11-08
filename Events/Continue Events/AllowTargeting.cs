namespace WizWar1 {
class TargetingEvent : Event {
    public ITarget targeted;

    public TargetingEvent(ITarget tTargeted, Wizard tTargeter) {
        targeted = tTargeted;
        Controller = tTargeter;
    }
}
}
