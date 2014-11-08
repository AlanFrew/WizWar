namespace WizWar1 {
class IllusionWallCreation : CreatedWall {
    public IllusionWallCreation() {
        //this is a hack; not sure where to put image files yet
        myImageVertical = GameState.Wizards[0].myUI.myBoard.vertIllusionWall;
        myImageHorizontal = GameState.Wizards[0].myUI.myBoard.horizIllusionWall;
    }

    public override bool IsPassable(Wizard tWizard) {
        if (tWizard != Creator) {
            return false;
        }

        return true;
    }
}
}
