namespace WizWar1 {
public class CastEvent : Event {
	internal ISpell SpellBeingCast;

	internal CastEvent(ISpell tSpellBeingCast) {
        SpellBeingCast = tSpellBeingCast;
    }
}
}
