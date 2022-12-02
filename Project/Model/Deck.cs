using System;
using System.Collections.Generic;

namespace PokerGame.Model
{
    public class Deck
    {
        private List<Card> _cards;
        
        public Deck()
        {
            _cards = new List<Card>();
            Refresh();
        }

        public void Refresh()
        {
            for(int i = 2; i<15; i++)
            {
                for(int j = 1; j < 5; j++)
                {
                    CardSuit cardSuit = (CardSuit)j;
                    CardRank cardRank = (CardRank)i;
                    Card card = new Card(new CardType(cardSuit, cardRank), true);
                    _cards.Add(card);
                }
            }
        }

        public Card getCard()
        {
            Random random = new Random();
            int rnd = random.Next(0, _cards.Count);
            Card result = _cards[rnd];
            _cards.Remove(result);
            return result;
        }

    }
}
