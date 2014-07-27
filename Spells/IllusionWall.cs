using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1
{
class IllusionWall : Spell {
    public IllusionWall() {
        Name = "Illusion Wall";
        validTargetTypes.Add(TargetTypes.WallSpace);
    }

    public override bool IsValidSpellTarget(ITarget tTarget, Wizard tCaster) {
        WallSpace ws = tTarget as WallSpace;
        if (GameState.BoardRef.LookForWall(ws.X, ws.Y) != null) {
            return false;
        }

        return true;
    }

    public override void OnChildCast() {
        CreateWallEffect cwe = Effect.New<CreateWallEffect>(Caster, this, SpellTarget);
        EffectsWaiting.Add(cwe);
    }
}
}
