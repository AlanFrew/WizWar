namespace WizWar1 {
class DropObject : Spell {
    public DropObject() {
        Name = "Drop Object";
        Description = "Make an object momentarily heavy, causing the carrier to drop it";
        ValidCastingTypes.Add(SpellType.Attack);
        ValidTargetTypes.Add(TargetTypes.Item);
    }

    public override bool IsValidTarget(ITarget tTarget) {
        //AllowTargeting a = new AllowTargeting((tTarget as IItem).Carrier); //not sure this class is valid anymore
        if (GameState.InitialUltimatum(Event.New<TargetingEvent>(true, new TargetingEvent((tTarget as IItem).Carrier, Controller))) == Redirect.Proceed) {
            return true;
        }

        return false;
    }

    public override void OnChildCast() {
        EffectsWaiting.Add(Effect.New<DropObjectEffect>(Caster, this, SpellTarget));
    }
}
}