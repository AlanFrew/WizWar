namespace WizWar1 {
public class Event {
    //these variables are only used sometimes...hope to eliminate
    internal Wizard Controller;
    internal ITarget EventTarget;
    internal ITarget Source;
    public bool IsAttempt = true;
    //public Effect myEffect;

    //start control flow management
    private double currentPriority = 0.0;

    private Redirect flowController = Redirect.Proceed;
    public Redirect GetFlowControl() {
        return flowController;
    }

    public bool SetFlowControl(Redirect tValue, double tPriority) { //returns true iff value is changed
        if (flowController != tValue) {
            if (tPriority > currentPriority) {
                flowController = tValue;
                return true;
            }
        }
        return false;
    }

    internal static T New<T>(bool tIsAttempt, Effect tEffect) where T : Event, new() {
        var result = new T {IsAttempt = tIsAttempt};

        return result;
    }

    internal static T New<T>(bool tIsAttempt, T tEvent) where T : Event {
        return tEvent;
    }
}
}
