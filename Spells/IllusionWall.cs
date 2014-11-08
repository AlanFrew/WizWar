namespace WizWar1
{
class IllusionWall : NumberedSpell {
    public IllusionWall() {
        Name = "Illusion Wall";
        Description = "Create a glamer that opponents believe is a true wall";
        ValidCastingTypes.Add(SpellType.Neutral);
        ValidTargetTypes.Add(TargetTypes.WallSpace);
    }

    public override bool IsValidTarget(ITarget tTarget) {
        var ws = tTarget as WallSpace;
        if (GameState.BoardRef.LookForWall(ws.X, ws.Y) != null) {
            return false;
        }

        return true;
    }

    public override void OnChildCast() {
        var cwe = Effect.New<CreateWallEffect>(Caster, this, SpellTarget, CardValue);
        EffectsWaiting.Add(cwe);
    }
}
}
