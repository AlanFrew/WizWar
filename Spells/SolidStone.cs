namespace WizWar1 {
class SolidStone : Spell {
    public SolidStone() {
        Name = "Fill with Stone";
        Description = "Create a massive stone block that prevents all movement and line of sight";
        ValidTargetTypes.Add(TargetTypes.Square);
        ValidCastingTypes.Add(SpellType.Neutral);
    }

    public override bool IsValidTarget(ITarget tTarget) {
        if ((tTarget as Square).creationsHere.Count != 0) {
            return false;
        }

        foreach (Wizard w in GameState.Wizards) {
            if (w.Location == tTarget as Square) {
                return false;
            }
        }

        return true;
    }

    public override void OnChildCast() {
        var cte = new MakeCreationEffect<SolidStoneCreation>(new SolidStoneCreation());
        cte = Effect.Initialize(Caster, this, SpellTarget, cte);
        EffectsWaiting.Add(cte);
    }

    public override void OnResolution() {
        var temp = EffectsWaiting[0] as MakeCreationEffect<SolidStoneCreation>;
        temp.MyCreation.Initialize(temp.Caster, temp.target as Square);
    }
}
}
