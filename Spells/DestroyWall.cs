using System;

namespace WizWar1 {
class DestroyWall : Spell {
    public DestroyWall() {
        Name = "Destroy Wall";
        Description = "Blast a wall apart. It deals 2 damage to anyone adjacent";
        ValidCastingTypes.Add(SpellType.Neutral);
        ValidTargetTypes.Add(TargetTypes.Wall);
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<DestroyWallEffect>(caster, this, spellTarget));
    }

    public override void OnResolution() {
        foreach (Effect e in EffectsWaiting) {
            if (e is DestroyWallEffect) {
                foreach (Wizard w in GameState.Wizards) {
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
