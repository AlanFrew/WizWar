namespace WizWar1 {
    internal interface IListener<X, T> where X : T where T : Event {
        void OnEvent(X tEvent);
    }
}
