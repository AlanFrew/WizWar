namespace WizWar1 {
    class Fireball : Spell {
        public Fireball() {
            Name = "Fireball";
            Description = "Fling a swirling ball of flame at an opponent. His stones burst apart";
            ValidTargetTypes.Add(TargetTypes.Wizard);
            ValidTargetTypes.Add(TargetTypes.Wall);
            ValidTargetTypes.Add(TargetTypes.Creation);
            ValidCastingTypes.Add(SpellType.Attack);
        }

        public override void OnChildCast() {
            var d = Effect.Initialize<DamageEffect>(Caster, this, SpellTarget, new DamageEffect(5, DamageType.Magical));
            EffectsWaiting.Add(d);

            //if (SpellTarget is Wizard) {
            //    foreach (IItem i in (SpellTarget as Wizard).Inventory) {
            //        if (i is Stone) {
            //            EffectsWaiting.Add(Effect.New<DestroyItemEffect>(Caster, this, SpellTarget));
            //        }
            //    }
            //}
        }

        public override void RanChild(Effect tEffect) {
            if (tEffect is DamageEffect && tEffect.target is Wizard) {
                if ((tEffect as DamageEffect).Amount > 0) {
                    foreach (Item i in (SpellTarget as Wizard).Inventory) {
                        if (i is Stone) {
                            GameState.PushEffect(Effect.New<DestroyItemEffect>(Caster, this, i));
                        }
                    }
                }
            }
        }
    }
}
