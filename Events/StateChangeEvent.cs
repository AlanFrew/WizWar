namespace WizWar1 {
public class StateChangeEvent : Event {
    public UIState OldState;
    public UIState NewState;

    internal StateChangeEvent(UIState tOldState, UIState tNewState, ITarget tEventTarget) {
        OldState = tOldState;
        NewState = tNewState;
        EventTarget = tEventTarget;
    }
}
}
