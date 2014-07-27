using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class DamageEffect : Effect {
    public int Amount {
        get {
            foreach (Marker m in markers) {
                if (m is PointBasedMarker) {
                    return (m as PointBasedMarker).PointValue;
                }
            }

            throw new UnreachableException();
        }
        set {
            if (value <= 0) {
                GameState.eventDispatcher.Notify(new UselessEffectEvent(this));     //dangerous untested code
            }

            foreach (Marker m in markers) {
                if (m is PointBasedMarker) {
                    (m as PointBasedMarker).PointValue = value;
                }
            }
        }
    }
    public DamageType damageType;
    //public ITarget Source;
    //public DamageEvent myEvent;

    public DamageEffect(int tAmount, DamageType tDamageType) {
        markers.Add(new PointBasedMarker());
        Amount = tAmount;
        damageType = tDamageType;
        myEvents.Add(new DamageEvent(this));
    }

    //public void Initialize(ITarget tSource, int tAmount, DamageType tType) {
    //    Source = tSource;
    //    amount = tAmount;
    //    damageType = tType;
    //}

    public override void OnRunChild() {
        //GameState.eventDispatcher.notify(new DamageEvent(Caster, Source, target, Amount, damageType)); //think this goes elsewhere
        if (GameState.InitialUltimatum(myEvents[0]) == Redirect.Proceed) {
            if (target is IDamageable) {
                (target as IDamageable).TakeDamage(this);
            }
            else {
                throw new NotSupportedException();
            }
        }
    }
}
}
