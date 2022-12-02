namespace PokerGame.Model
{
    public struct Hand
    {
        public Card leftHand;
        public Card rightHand;

        public Hand(Card leftHand, Card rightHand)
        {
            this.leftHand = leftHand;
            this.rightHand = rightHand;
        }
    }
}
