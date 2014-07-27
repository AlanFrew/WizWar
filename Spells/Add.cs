using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class Add : Spell {
    private StateControlEffect addEffect;

    public Add() {
        Name = "Add";
    }

    public override void OnChildCast() {
        //addEffect = new StateControlEffect(UIState.AddingNumber, UIState.AddingNumber, Caster.myUI);
        //GameState.NewEffect(addEffect);

        caster.myUI.myControl.numberCardsLeft += 1;
    }

        
}
}
