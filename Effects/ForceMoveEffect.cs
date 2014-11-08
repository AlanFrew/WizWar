namespace WizWar1 {
class ForceMoveEffect : Effect {
    public delegate void ForceMoveDelegate(Wizard tWizard);
    public ForceMoveDelegate ForceMoveMethod;

    public override void OnRunChild() {
        ForceMoveMethod(target as Wizard);
    }

    public void Initialize(ForceMoveDelegate tDelegate) {
        ForceMoveMethod = tDelegate;
    }
}
}
