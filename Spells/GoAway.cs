using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class GoAway : Spell {
    public GoAway() {
        Name = "Go Away";
        validTargetTypes.Add(TargetTypes.Wizard);
        acceptsNumber = true;
    }

    public override void OnChildCast() {
        ForceMoveEffect e = Effect.New<ForceMoveEffect>(Caster, this, SpellTarget);
        e.Initialize(new ForceMoveEffect.ForceMoveDelegate(ForceMoveFunc));
    }

    public void ForceMoveFunc(Wizard tMoved) {
        double xChange = (SpellTarget as Wizard).X - Caster.X;
        double yChange = (SpellTarget as Wizard).Y - Caster.Y;
        var possibleDirections = new List<Direction>();

        if (xChange == 0) {
            if (yChange == 0) {
                foreach (Direction d in Enum.GetValues(typeof(Direction))) {
                    possibleDirections.Add(d);
                }
            }
            else if (yChange > 0) {
                possibleDirections.Add(Direction.East);
            }
        }
        else if (xChange == 0) {
            //this function is incomplete
        }

    }
}
}
