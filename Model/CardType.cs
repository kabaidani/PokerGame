namespace PokerGame.Model
{
    public struct CardType
    {
        public CardType(CardSuit cardSuit, CardRank cardRank)
        {
            this.cardSuit = cardSuit;
            this.cardRank = cardRank;
        }
        public CardSuit cardSuit;
        public CardRank cardRank;
    }
}
