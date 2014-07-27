using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class BrainStoneItem : Stone {
    public override void OnGainChild(Wizard tHolder) {
        tHolder.MaxHandSize += 2;
        GameState.deck.dealCards(tHolder, 2);
    }

    public override void OnLossChild(Wizard tDropper) {
        tDropper.MaxHandSize -= 2;
        tDropper.SettleHand();
    }
}
}
