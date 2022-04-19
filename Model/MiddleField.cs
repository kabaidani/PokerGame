using System;
using System.Collections.Generic;

namespace PokerGame.Model
{
    public class MiddleField
    {
        public List<Card> CommonityCards { private set; get; }
        public int CommonityBet { private set; get; }

        public void ClearTheSection()
        {
            CommonityCards.Clear();
            CommonityBet = 0; //Not sure if it is needed here
        }

        public static Card CardGenerator(Random rand, bool isUpdsideDown = true)
        {
            int tmpCardSuit = rand.Next(1, 4);
            int tmpCardRank = rand.Next(2, 14);
            Card result = new Card(new CardType((CardSuit)tmpCardSuit, (CardRank)tmpCardRank), isUpdsideDown);
            return result;
        }

        public MiddleField()
        {
            CommonityCards = new List<Card>();
        }

        public void UnfoldNextCard()
        {
            if (CommonityCards.Count == 5) return;
            Random rand = new Random();
            CommonityCards.Add(CardGenerator(rand, false));
        }

        public void CollectBets(List<Player> players)
        {
            int ammount = 0;
            foreach(var player in players)
            {
                ammount += player.CollectBet();
            }

            CommonityBet += ammount;
        }
    }
}
