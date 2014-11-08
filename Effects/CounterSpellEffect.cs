namespace WizWar1 {
class CounterSpellEffect : Effect {
    public override void OnRunChild() {
        GameState.TheStack.Remove(target as IStackable);
    }
}
}
