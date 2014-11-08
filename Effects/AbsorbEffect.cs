namespace WizWar1 {
class AbsorbEffect : Effect {
    public AbsorbEffect() {
        markers.Add(new PointBasedMarker());
    }

    public override void OnRunChild() {
        foreach (Marker m in (target as Effect).markers) {
            if (m is PointBasedMarker) {
                (m as PointBasedMarker).PointValue -= 3;
                if ((m as PointBasedMarker).PointValue < 0) {
                    (m as PointBasedMarker).PointValue = 0;
                }
            }
        }
    }
}
}
