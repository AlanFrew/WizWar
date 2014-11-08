namespace WizWar1 {
    class Number : Cardable {
        public Number() {
            //empty
        }

        public int Value { get; set; }

        public Number(int tValue) {
            Value = tValue;
            //Name = Value.ToString();      //hopefully this won't cause problems
        }
    }
}
