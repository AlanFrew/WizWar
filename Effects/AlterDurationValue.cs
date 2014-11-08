namespace WizWar1 {
class AlterDurationValue : Effect {
    public delegate int ValueProcessorDelegate(int durationValue);
    ValueProcessorDelegate valueProcessor;

    public AlterDurationValue() {
        //empty
    }

    public AlterDurationValue(ValueProcessorDelegate tValueProcessor) {
        valueProcessor  = tValueProcessor;
    }

    public void Initialize(ValueProcessorDelegate tValueProcessor) {
        valueProcessor  = tValueProcessor;
    }

    public override void OnRunChild() {
        foreach (Marker m in (target as ISpell).Markers) {
            if (m is DurationBasedMarker) {
                valueProcessor((m as DurationBasedMarker).DurationBasedValue);
            }
        }
    }
}
}
