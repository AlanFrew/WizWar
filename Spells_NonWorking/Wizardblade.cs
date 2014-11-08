namespace WizWar1 {
class Wizardblade : Spell {
    public Wizardblade() {
        Name = "Wizardblade";
        ValidCastingTypes.Add(SpellType.Item);
        ValidTargetTypes.Add(TargetTypes.None);
    }
}
}
