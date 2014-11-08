namespace WizWar1 {
class PassthroughEvent : Event {
    public Wizard Wizard;
    public IWall Wall;
    public PassthroughEvent(Wizard tWizard, IWall tWall) {
        Wizard = tWizard;
        Wall = tWall;
    }
}
}