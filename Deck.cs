using System;
using System.Collections.Generic;
using System.Linq;
using WizWar1.Properties;

namespace WizWar1 {
internal class Deck {
    private List<ICard> cards;

    public Deck() {
        cards = new List<ICard>();
    }

    public void Shuffle() {
        var rng = new Random();

        for (int n = cards.Count; n > 1; n--) {
            // Pick a random element to move to the end
            int k = rng.Next(n); // 0 <= k <= n - 1.
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

    public void DealCards(Wizard tRecipient, int cardsToDeal, bool applyLimit = true) {
        if (cardsToDeal > 2) {
            applyLimit = false;
        }

        if (applyLimit == true) {
            if (tRecipient.CardsDrawn >= 2) {
                return;
            }
        }

        var nee = Event.New<NewEffectEvent>(true, new NewEffectEvent(new DrawEffect(cardsToDeal)));
        if (GameState.InitialUltimatum(nee) == Redirect.Proceed) {
            int limit;
            if (applyLimit == true) {
                limit = Math.Min(Math.Max(2 - tRecipient.CardsDrawn, 0), cardsToDeal);
            }
            else {
                limit = cardsToDeal;
            }
            var temp = new ICard[limit];
            for (int i = 0; i < temp.Length; i++) {
                if (applyLimit == true) {
                    tRecipient.CardsDrawn++;
                }

                var de = Event.New<DrawEvent>(true, new DrawEvent(cards.ElementAt(0), tRecipient));
                if (GameState.InitialUltimatum(de) == Redirect.Proceed) {
                    temp[i] = cards.ElementAt(0);
                    cards.RemoveAt(0);

                    de.IsAttempt = false;
                    GameState.EventDispatcher.Notify(de);
                }
            }
            tRecipient.giveCards(temp);

            nee.IsAttempt = false;
            GameState.EventDispatcher.Notify(nee);
        }
    }

    internal void Regenerate() {
        cards = new List<ICard>();
        for (int i = 0; i < 3; ++i) {
            cards.Add(new Card<Absorb>());
            cards.Add(new Card<AbsorbSpell>());
            cards.Add(new Card<Amplify>());
            cards.Add(new Card<AntiAnti>());
            cards.Add(new Card<Blind>());
            cards.Add(new Card<BloodStone>());
            cards.Add(new Card<Blunt>());
            cards.Add(new Card<BrainStone>());
            cards.Add(new Card<Buddy>());
            //cards.Add(new Card<CardErasure>());
            cards.Add(new Card<Dagger>());
            cards.Add(new Card<DestroyWall>());
            cards.Add(new Card<DispelCreation>());
            cards.Add(new Card<Drag>());
            cards.Add(new Card<DropObject>());
            cards.Add(new Card<Extend>());
            cards.Add(new Card<Fireball>());
            cards.Add(new Card<LargeRock>());
            cards.Add(new Card<FullReflection>());
            cards.Add(new Card<FullShield>());
            cards.Add(new Card<IllusionWall>());
            cards.Add(new Card<Invisible>());
            cards.Add(new Card<LargeRock>());
            cards.Add(new Card<LightningBlast>());
            cards.Add(new Card<LockInPlace>());
            cards.Add(new Card<MasterKey>());
            cards.Add(new Card<PowerRun>());
            cards.Add(new Card<PowerStone>());
            cards.Add(new Card<PowerThrust>());
            cards.Add(new Card<SlowDeath>());
            cards.Add(new Card<SolidStone>());
            cards.Add(new Card<SoulStone>());
            cards.Add(new Card<Speed>());
            cards.Add(new Card<StoneDead>());
            cards.Add(new Card<SuddenDeath>());
            cards.Add(new Card<Swap>());
            //cards.Add(new Card<TeleportOpponent>());
            cards.Add(new Card<Thornbush>());
            cards.Add(new Card<WaterWall>());
            cards.Add(new Card<WaterBolt>());
            var numberCard = new Card<Number>();
            numberCard.WrappedCard = new Number();
            (numberCard.WrappedCard as Number).Value = 5;
            //cards.Add();
        }

        foreach (ICard c in cards) {
            if (c.WrappedCard is Spell == false) {
                return;
            }

            if ((c.WrappedCard as Spell).ValidCastingTypes.Count == 0) {
                throw new NotImplementedException();
            }

            if ((c.WrappedCard as Spell).ValidTargetTypes.Count == 0) {
                throw new NotImplementedException();
            }
        }

        if (Settings.Default.ShuffleCards) {
            Shuffle();
        }
    }
}
}