namespace WizWar1 {
class Obstruction : Creation {
    public virtual bool IsPassable(Wizard tWizard) {
        return false;
    }

    public void OnEnterParent(Wizard tWizard) {
        OnEnterChild(tWizard);
        GameState.RunTheStack();
    }

    public virtual void OnEnterChild(Wizard tWizard) {
        //empty
    }
}
}
