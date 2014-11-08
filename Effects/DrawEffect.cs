using System;

namespace WizWar1 {
class DrawEffect : Effect {
    public int Quantity;

    public DrawEffect() {
        throw new NotImplementedException();
    }

    public DrawEffect(int tQuantity) {
        Quantity = tQuantity;
    }

    public DrawEffect(Wizard tCaster, ITarget tTarget, int tQuantity) {
        Quantity = tQuantity;
        Caster = tCaster;
        target = tTarget;
        markers.Add(new PointBasedMarker());
    }

    public override void OnRunChild() {
        Caster.giveCards(new[] {(ICard)target});
    }
}
}
