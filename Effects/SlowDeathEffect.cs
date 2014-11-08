using System;

namespace WizWar1 {
	class SlowDeathEffect : Effect, IListener<DrawEvent, Event> {
		public SlowDeathEffect() {
			markers.Add(new DurationBasedMarker(Int32.MaxValue));
		}

		public void OnEvent(DrawEvent drawEvent) {
			if (drawEvent.Drawer == target && drawEvent.IsAttempt == false) {
				(target as Wizard).TakeDamage(new DamageEffect(1, DamageType.Magical));
			}
		}

		public override void OnRunChild() {
			GameState.EventDispatcher.Register(this);
		}
	}
}