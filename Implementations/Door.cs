namespace WizWar1 {
class Door : Wall, IDamageable {
    public bool locked;

    public Door() {
        locked = true;
        myImageHorizontal = GameState.Wizards[0].myUI.myBoard.horiz_door;
        myImageVertical = GameState.Wizards[0].myUI.myBoard.vert_door;
    }

    public override bool IsPassable(Wizard tWizard) {
        foreach (IItem i in tWizard.Inventory) {
            if (i is MasterKeyItem) {
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
