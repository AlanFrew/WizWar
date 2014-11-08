using System;

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
                GameState.EventDispatcher.Notify(new UselessEffectEvent(this));     //dangerous untested code
            }

            foreach (Marker m in markers) {
                if (m is PointBasedMarker) {
                    (m as PointBasedMarker).PointValue = value;
                }
            }
        }
    }
    public DamageType DamageType;

    public DamageEffect(int tAmount, DamageType tDamageType) {
        markers.Add(new PointBasedMarker());
        Amount = tAmount;
        DamageType = tDamageType;
        myEvents.Add(new DamageEvent(this));
    }

    public override void OnRunChild() {
        if (GameState.InitialUltimatum(myEvents[0]) == Redirect.Proceed) {
            if (target is IDamageable) {
                (target as IDamageable).TakeDamage(this);
            }
            else {
                throw new NotSupportedException("Target " + target + " Caster " + Caster + " Source " + source);
            }
        }
    }
}
}
