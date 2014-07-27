using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class TakeDiscardEffect : Effect {
    ICard cardToRemove;
    public TakeDiscardEffect(ICard tCard) {
        cardToRemove = tCard;
    }

    public override void OnRunChild() {
        GameState.discard.Remove(cardToRemove);
        Caster.giveCard(cardToRemove);
    }
}
}
