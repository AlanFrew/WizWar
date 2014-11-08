namespace WizWar1 {
class RemoveLock : Spell {
    public RemoveLock() {
        Name = "Remove Lock";
        ValidTargetTypes.Add(TargetTypes.Door);
    }

    public override void OnChildCast() {
        (SpellTarget as Door).locked = false;
    }
}
}
