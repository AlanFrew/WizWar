namespace WizWar1 {
    class PointBasedMarker : Marker {
        public int PointValue = 0;
        
        public PointBasedMarker() {
            //empty
        }

        public PointBasedMarker(int tPointValue) {
            PointValue = tPointValue;
        }
    }
}
