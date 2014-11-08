namespace WizWar1 {
class DestroyObjectEvent : Event {
    public ITarget DestroyedObject;

    public DestroyObjectEvent(ITarget tDestroyedObject) {
        DestroyedObject = tDestroyedObject;
    }
}
}
