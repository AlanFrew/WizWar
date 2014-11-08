namespace WizWar1 {
class Thornbush : Spell {
    public Thornbush() {
        Name = "Thornbush";
        Description = "Create a thick web of thorns, damaging anyone who attempts to pass and causing them to miss a turn";
        ValidTargetTypes.Add(TargetTypes.Square);
        ValidCastingTypes.Add(SpellType.Neutral);
    }

    public override bool IsValidTarget(ITarget tTarget) {
        foreach (ICreation c in (tTarget as Square).creationsHere) {
            if (c is Obstruction) {
                return false;
            }
        }

        return true;
    }

    public override void OnChildCast() {
        var temp = new MakeCreationEffect<ThornbushCreation>(new ThornbushCreation());
        var cte = Effect.Initialize<MakeCreationEffect<ThornbushCreation>>(Caster, this, SpellTarget, temp);
        EffectsWaiting.Add(cte);
    }

    public override void OnResolution() {
        var temp = EffectsWaiting[0] as MakeCreationEffect<ThornbushCreation>;
        temp.MyCreation.Initialize(temp.Caster, temp.target as Square);
    }
}
}
