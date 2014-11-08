namespace WizWar1 {
class Buddy : Spell {
    public Buddy() {
        Name = "Buddy";
        Description = "Make friends with an opponent, preventing him from attacking you as long as you don't attack him";
        ValidCastingTypes.Add(SpellType.Neutral);
        ValidTargetTypes.Add(TargetTypes.Wizard);
    }

    //public override bool IsValidSpellTarget(ITarget tTarget) {
    //    return base.IsValidSpellTarget(tTarget);
    //}

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<BuddyEffect>(Caster, this, SpellTarget));
    }
}
}
