using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DrawEffect : Effect {
    public int quantity;

    public DrawEffect() {
        throw new NotImplementedException();
    }

    public DrawEffect(int tQuantity) {
        quantity = tQuantity;
    }

    public DrawEffect(Wizard tCaster, ISpell tSource, ITarget tTarget, int tQuantity) {
        quantity = tQuantity;
        Caster = tCaster;
        target = tTarget;
        duration = 0;
        markers.Add(new PointBasedMarker());
    }

    public override void OnRunChild() {
        Caster.giveCards(new ICard[1] {(ICard)target});
    }
}
}
