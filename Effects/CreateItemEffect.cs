namespace WizWar1 {
class CreateItemEffect<T> : Effect where T : IItem, new() {
    public T ItemToCreate;

    public CreateItemEffect() {
        ItemToCreate = new T();
    }

    public void Initialize() {
        ItemToCreate = new T();
    }

    public override void OnRunChild() {
        Caster.giveItem(ItemToCreate);
    }
}
}
