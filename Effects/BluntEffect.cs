namespace WizWar1 {
class BluntEffect : Effect {
    public override void OnRunChild() {
        foreach (Marker m in (target as Effect).markers) {
            if (m is PointBasedMarker) {
                (m as PointBasedMarker).PointValue /= 2;
            }
        }
    }
}
}
