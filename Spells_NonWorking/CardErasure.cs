namespace WizWar1 {
class CardErasure : Spell {
    public CardErasure() {
        Name = "Card Erasure";
        Description = "Erase a spell from an opponent's mind, forcing him to discard it";
        ValidCastingTypes.Add(SpellType.Attack);
        ValidTargetTypes.Add(TargetTypes.Wizard);
    }

    public override void OnChildCast() {
        var w = new WindowCallback();
        var nameTheCard = new CardChooser(w);

        GameState.InitialUltimatum(Event.New<WindowCallback>(true, w)); //cant be used to skip?

        var e = Effect.New<ForceDiscardEffect>(Caster, this, SpellTarget);
        e.Initialize(Card<Cardable>.NewCard(w.AdditionalInfo));
        EffectsWaiting.Add(e);
    }
}
}