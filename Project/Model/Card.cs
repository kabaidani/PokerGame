namespace PokerGame.Model
{
    public struct Card
    {
        public Card(CardType cardType, bool isUpSideDown)
        {
            this.cardType = cardType;
            this.isUpSideDown = isUpSideDown;
            signed = false;
        }

        public CardType cardType;
        public bool isUpSideDown;
        public bool signed;
    }
}
