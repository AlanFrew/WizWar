namespace WizWar1 {
class NumberedSpell : Spell, INumberable {
    private int _cardValue = 1;
    public int CardValue { get { return _cardValue; } set { _cardValue = value; } }
}
}
