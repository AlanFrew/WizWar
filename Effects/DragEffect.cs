namespace WizWar1 {
class DragEffect : Effect {
    public override void OnRunChild() {
        (target as ILocatable).X = Caster.X;
        (target as ILocatable).Y = Caster.Y;
    }
}
}
