using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Dagger : Item {
    public Dagger() {
        itemTargetTypes.Add(TargetTypes.Wizard);
        itemTargetTypes.Add(TargetTypes.Wall);
        itemTargetTypes.Add(TargetTypes.Creation);
    }

    public override void OnActivationChild() {
        Carrier.myUI.State = UIState.FindingTarget;

        ItemThrowEffect ite = null;
        if (ItemTarget is Wall) { //must find which side of wall the item lands on
            Wall w = ItemTarget as Wall;
            if (w.IsVertical()) {
                if (Math.Abs(Carrier.myUI.ItemToUse.ShotDirection) > Math.PI / 2) { //don't need < condition
                    ite = new ItemThrowEffect(Carrier.ContainingSquare, w.SecondNeighbor);
                }
                else {
                    ite = new ItemThrowEffect(Carrier.ContainingSquare, w.FirstNeighbor);
                }
            }
            else { //wall is horizontal
                if (Carrier.myUI.ItemToUse.ShotDirection > 0) {
                    ite = new ItemThrowEffect(Carrier.ContainingSquare, w.FirstNeighbor);
                }
                else {
                    ite = new ItemThrowEffect(Carrier.ContainingSquare, w.SecondNeighbor);
                }
            }
        }
        else { //target is in a square
            ite = new ItemThrowEffect(Carrier.ContainingSquare, GameState.BoardRef.At((ItemTarget as ILocatable).X, (ItemTarget as ILocatable).Y));
        }
        EffectsWaiting.Add(Effect.Initialize<ItemThrowEffect>(Carrier, this, ItemTarget, ite));
        EffectsWaiting.Add(Effect.Initialize<DamageEffect>(Carrier, this, ItemTarget, new DamageEffect(3, DamageType.Physical)));
        //EffectsWaiting.Add(Effect.NewPlus<DaggerAttack>(Carrier, this, ItemTarget));
    }

    public override bool IsValidTargetForItem(ITarget tTarget) {
        return base.IsValidTargetForItem(tTarget);
    }
}
}
