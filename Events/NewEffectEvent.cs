namespace WizWar1 {
	public class NewEffectEvent : Event {
		internal Effect myEffect;

		internal NewEffectEvent(Effect tEffect) {
			myEffect = tEffect;
		}
	}
}