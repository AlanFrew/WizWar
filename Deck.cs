using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1
{

class Deck
{
    private List<ICard> cards;

    public Deck() {
        cards = new List<ICard>();
    }

    public void shuffle() {
        Random rng = new Random();

        for (int n = cards.Count; n > 1; n--) {
            // Pick a random element to move to the end
            int k = rng.Next(n);  // 0 <= k <= n - 1.
            // Simple swap of variables
            ICard temp = cards.ElementAt(k);
            cards.RemoveAt(k);
            int tt = cards.Count;
            ICard ttemp = cards.ElementAt(n - 2);
            cards.Insert(k, ttemp);
            cards.RemoveAt(n - 1);
            cards.Insert(n - 1, temp);
        }
    }

    public void dealCards(Wizard tRecipient, int cardsToDeal, bool applyLimit = true) {
        if (cardsToDeal > 2) {
            applyLimit = false;
        }

        if (applyLimit == true) {
            if (tRecipient.CardsDrawn >= 2) {
                return;
            }
        }

        NewEffectEvent nee = Event.New<NewEffectEvent>(true, new NewEffectEvent(new DrawEffect(cardsToDeal)));
        if (GameState.InitialUltimatum(nee) == Redirect.Proceed) {
            int limit;
            if (applyLimit == true) {
                limit = Math.Min(Math.Max(2 - tRecipient.CardsDrawn, 0), cardsToDeal);
            }
            else {
                limit = cardsToDeal;
            }
            ICard[] temp = new ICard[limit];
            for (int i = 0; i < temp.Length; i++) {
                if (applyLimit == true) {
                    tRecipient.CardsDrawn++;
                }

                DrawEvent de = Event.New<DrawEvent>(true, new DrawEvent(cards.ElementAt(0)));
                if (GameState.InitialUltimatum(de) == Redirect.Proceed) {
                    temp[i] = cards.ElementAt(0);
                    cards.RemoveAt(0);
                    
                    de.IsAttempt = false;
                    GameState.eventDispatcher.Notify(de);
                }
            }
            tRecipient.giveCards(temp);

            nee.IsAttempt = false;
            GameState.eventDispatcher.Notify(nee);
        }
    }
    
    internal void regenerate() {
        cards = new List<ICard>();
        for (int i = 0; i < 2; ++i) {
            cards.Add(new DestroyWall());
            cards.Add(new Absorb());
            cards.Add(new LightningBlast());
            cards.Add(new FullReflection());
            cards.Add(new FullShield());
            cards.Add(new Thornbush());
            cards.Add(new Number(3));
            cards.Add(new Fireball());
            cards.Add(new AbsorbSpell());
            cards.Add(new IllusionWall());
            cards.Add(new BloodStone());
            cards.Add(new Add());
        }
        
        foreach (Card c in cards) {
            if (c is Spell == false) {
                return;
            }

            if ((c as Spell).ValidCastingTypes.Count == 0) {
                throw new NotImplementedException();
            }
        }

        if (GameState.Debug == false) {
            shuffle();
        }
    }
}
}
