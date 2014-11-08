using System.Drawing;

namespace WizWar1 {
class ThornbushCreation : Obstruction, IDamageable {
    public ThornbushCreation() {
        MyImage = Image.FromFile(@"Arena\thornbush.bmp");
    }
    public override void OnEnterChild(Wizard tWizard) {
        GameState.PushEffect(Effect.New<EndTurnEffect>(null, this, tWizard));
        GameState.PushEffect(Effect.New<LostTurnEffect>(null, this, tWizard, 1));
        GameState.PushEffect(Effect.Initialize<DamageEffect>(null, this, tWizard, new DamageEffect(1, DamageType.Physical)));
    }

    private int hitPoints = 5;
    public void TakeDamage(DamageEffect d) {
        hitPoints -= d.Amount;
        if (hitPoints <= 0) {
            Location.creationsHere.Remove(this);
        }
    }

    public override bool IsPassable(Wizard tWizard) {
        return true;
    }
}
}