using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DestroyWall : Spell {
    public DestroyWall() {
        Name = "Destroy Wall";
        validCastingTypes.Add(SpellType.Neutral);
        validTargetTypes.Add(TargetTypes.Wall);
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<DestroyWallEffect>(caster, this, spellTarget));
    }

    public override void OnResolution() {
        foreach (Effect e in EffectsWaiting) {
            if (e is DestroyWallEffect) {
                foreach (Wizard w in GameState.wizards) {
                    if (Math.Abs(w.X + w.Y - (SpellTarget as IWall).X - (SpellTarget as IWall).Y) < 1) { 
                        DamageEffect d = Effect.Initialize<DamageEffect>(Caster, this, w, new DamageEffect(4, DamageType.Physical));
                        EffectsWaiting.Add(d);
                    }
                }
            }
        }

        base.OnResolution();
    }
}
}
