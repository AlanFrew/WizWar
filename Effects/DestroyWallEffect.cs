namespace WizWar1 {
class DestroyWallEffect : DestroyEffect {
    public override void OnRunChild() {
        GameState.EventDispatcher.Notify(new DestroyObjectEvent(target));

        (target as IWall).Destroy(this);
    }
}
}
