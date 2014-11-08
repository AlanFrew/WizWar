namespace WizWar1 {
class WallOfFire : Spell {
    public WallOfFire() {
        Name = "Wall of Fire";
        ValidCastingTypes.Add(SpellType.Neutral);
        ValidTargetTypes.Add(TargetTypes.WallSpace);
    }

    public override bool IsValidTarget(ITarget tTarget) {
        var wallTarget = tTarget as WallSpace;
        return wallTarget.WallHere != null ? true : false;
    }
}
}
