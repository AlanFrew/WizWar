namespace WizWar1 {
class MagicWall : Spell {
    public MagicWall(Wizard tCreator, Square tLocation) {
        Name = "Create Wall";
        ValidTargetTypes.Add(TargetTypes.WallSpace);
    }
}
}