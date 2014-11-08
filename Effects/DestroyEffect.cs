namespace WizWar1 {
internal class DestroyEffect : Effect {
    public override void OnRunChild() {
        GameState.EventDispatcher.Notify(new DestroyObjectEvent(target));
    }
}
}