using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class ThornbushCreation : Obstruction, IDamageable {
    public override void OnEnterChild(Wizard tWizard) {
        GameState.NewEffect(Effect.New<EndTurnEffect>(null, this, tWizard));
        GameState.NewEffect(Effect.New<LostTurnEffect>(null, this, tWizard, 1));
        GameState.NewEffect(Effect.Initialize<DamageEffect>(null, this, tWizard, new DamageEffect(1, DamageType.Physical)));
    }

    private int hitPoints = 5;
    public void TakeDamage(DamageEffect d) {
        hitPoints -= d.Amount;
        if (hitPoints <= 0) {
            Location.creationsHere.Remove(this);
        }
    }
}
}
