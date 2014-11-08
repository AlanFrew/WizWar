namespace WizWar1 {
class PickLock : Spell {
    public PickLock() {
        Name = "Pick Lock";
        Description = "Pick an adjacent door lock, allowing you to pass through once";
    }

    public override bool IsValidTarget(ITarget tTarget) {
        if (!(tTarget is Door)) {
            return false;
        }

        return GameState.BoardRef.IsAdjacent(Caster.Location, tTarget as Door);
    }
}
}