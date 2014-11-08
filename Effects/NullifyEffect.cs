namespace WizWar1 {
class NullifyEffect : Effect {
    public override void OnRunChild() {
        (target as ISpell).EffectsWaiting.Clear();
    }
}
}