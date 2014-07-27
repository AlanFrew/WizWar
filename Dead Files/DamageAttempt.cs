using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
    class DamageAttempt : Event {
        public int Amount;
        public DamageType EffectDamageType;

        public DamageAttempt(Wizard tController, ITarget tSource, ITarget tTarget, int tAmount, DamageType tType) {
            Controller = tController;
            Source = tSource;
            EventTarget = tTarget;
            Amount = tAmount;
            EffectDamageType = tType;
        }
    }
}
