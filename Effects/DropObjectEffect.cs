namespace WizWar1 {
class DropObjectEffect : Effect {
    public override void OnRunChild() {
        (target as IItem).Carrier.dropItem(target as IItem);
    }
}
}
