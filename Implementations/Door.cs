using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Door : Wall, IDamageable {
    public bool locked;

    public Door() {
        locked = true;
        myImageHorizontal = GameState.wizards[0].myUI.myForm.horiz_door;
        myImageVertical = GameState.wizards[0].myUI.myForm.vert_door;
    }

    public override bool IsPassable(Wizard tWizard) {
        foreach (IItem i in tWizard.Inventory) {
            if (i is MasterKey) {
                return true;
            }
        }

        return false;
    }

    private int hitPoints = 15;

    public new void TakeDamage(DamageEffect d) {
        hitPoints -= d.Amount;
        if (hitPoints <= 0) {
            GameState.BoardRef.RemoveWall(this);
        }
    }
}
}
