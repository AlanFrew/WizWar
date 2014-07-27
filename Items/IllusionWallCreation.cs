using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class IllusionWallCreation : CreatedWall {
    public IllusionWallCreation() {
        //this is a hack; not sure where to put image files yet
        myImageVertical = GameState.wizards[0].myUI.myForm.vertIllusionWall;
        myImageHorizontal = GameState.wizards[0].myUI.myForm.vertIllusionWall;
    }

    public override bool IsPassable(Wizard tWizard) {
        if (tWizard != Creator) {
            return false;
        }

        return true;
    }
}
}
