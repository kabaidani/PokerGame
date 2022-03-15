using System;
using System.Collections.Generic;

namespace PokerGame.Model
{
    public class CommonityCardsEventArgs : EventArgs
    {
        public List<Card> CommonityCards { private set; get; }
        public CommonityCardsEventArgs(List<Card> commonityCards)
        {
            CommonityCards = commonityCards;
        }
    }
}
