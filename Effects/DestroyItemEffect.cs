namespace WizWar1 {
class DestroyItemEffect : Effect {
    public override void OnRunChild() {
        (target as Item).Destroy(null);
    }
}
}
