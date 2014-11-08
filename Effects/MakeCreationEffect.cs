namespace WizWar1 {
class MakeCreationEffect<T> : Effect where T : Creation {
    public T MyCreation;

    public MakeCreationEffect(T tCreation) {
        MyCreation = tCreation;
    }

    public override void OnRunChild() {
        (target as Square).creationsHere.Add(MyCreation);
    }
}
}