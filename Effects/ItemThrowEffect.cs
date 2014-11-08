using System;

namespace WizWar1 {
class ItemThrowEffect : Effect {
    private IItem item;
    private ITarget finalLocation;

    public ItemThrowEffect(IItem tItem, ITarget tFinalLocation) {
        item = tItem;
        finalLocation = tFinalLocation;
    }

    public static Square StayInSquare(IItem tItem, ITarget tTarget) {
        if (tTarget is IWall) {
            var w = tTarget as IWall;

            if (w.IsVertical()) {
                return Math.Abs(tItem.ShotDirection) > Math.PI/2 ? w.SecondNeighbor : w.FirstNeighbor;
            }
            else { //wall is horizontal
                return tItem.ShotDirection > 0 ? w.FirstNeighbor : w.SecondNeighbor;
            }
        }
        else if (tTarget is Wizard) {
            return (tTarget as Wizard).Location;
        }
        else if (tTarget is Square) {
            return (Square) tTarget;
        }
        
        throw new NotImplementedException();
    }

    public override void OnRunChild() {
        item.Carrier.loseItem(item);
        if (finalLocation is Wizard) {
            (finalLocation as Wizard).giveItem(item);
        }
        else if (finalLocation is Square) {
            (finalLocation as Square).AddLocatable(item);
        }
        else {
            throw new NotImplementedException();
        }
    }
}
}
