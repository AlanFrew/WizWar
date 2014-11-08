using System;

namespace WizWar1 {
class WindowCallback : ContinueEvent {
    public String AdditionalInfo;

    public WindowCallback() {
        SetFlowControl(Redirect.Halt, Double.MaxValue);
    }
}
}
