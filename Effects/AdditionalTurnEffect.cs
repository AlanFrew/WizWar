namespace WizWar1 {
	class AdditionalTurnEffect : Effect, IListener<TurnStartEvent, Event> {
		public AdditionalTurnEffect() {
			GameState.EventDispatcher.Register<TurnStartEvent>(this);
		}

		public void OnEvent(TurnStartEvent tse) {
			if (tse.IsAttempt) {
				tse.NextWizard = Caster;
				GameState.EventDispatcher.Deregister<TurnStartEvent>(this);
			}
		}

		public override void OnRunChild() {
			//tse.NextWizard = Caster;
			//GameState.EventDispatcher.Register(this);
		}
	}
}