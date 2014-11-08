namespace WizWar1 {
class TargetedEvent : Event {
    public ITarget TargetedObject;

    public TargetedEvent(ITarget tTargetedObject) {
        TargetedObject = tTargetedObject;
    }
}
}
