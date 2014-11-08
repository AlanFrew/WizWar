using System.Drawing;

namespace WizWar1 {
class SolidStoneCreation : Obstruction, IDamageable {
    public SolidStoneCreation() {
        MyImage = Image.FromFile(@"Arena\solidstone.bmp");
    }

    private int hitPoints = 20;
    public void TakeDamage(DamageEffect d) {
        hitPoints -= d.Amount;
        if (hitPoints <= 0) {
            Location.creationsHere.Remove(this);
        }
    }
}
}