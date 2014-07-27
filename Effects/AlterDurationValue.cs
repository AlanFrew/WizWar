using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWar1 {
class AlterDurationValue : Effect {
    public delegate int ValueProcessorDelegate(int durationValue);
    ValueProcessorDelegate ValueProcessor;

    public AlterDurationValue() {
        //empty
    }

    public AlterDurationValue(ValueProcessorDelegate tValueProcessor) {
        ValueProcessor  = tValueProcessor;
    }

    public void Initialize(ValueProcessorDelegate tValueProcessor) {
        ValueProcessor  = tValueProcessor;
    }

    public override void OnRunChild() {
        foreach (Marker m in (target as ISpell).Markers) {
            if (m is DurationBasedMarker) {
                ValueProcessor((m as DurationBasedMarker).DurationBasedValue);
            }
        }
    }
}
}
