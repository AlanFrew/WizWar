using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class CardErasure : Spell {
        public CardErasure() {
            Name = "Card Erasure";
        }

        public override void OnChildCast() {
            WindowCallback w = new WindowCallback();
            CardChooser nameTheCard = new CardChooser(w);

            GameState.InitialUltimatum(Event.New<WindowCallback>(true, w)); //cant be used to skip?

            ForceDiscardEffect e = Effect.New<ForceDiscardEffect>(Caster, this, SpellTarget);
            e.Initialize(Card.NewCard(w.AdditionalInfo));
            EffectsWaiting.Add(e);
        }
    }
}
