using System;
using System.Collections.Generic;
using System.Text;

namespace PokerGame.Model
{
    public class PokerModel
    {
        private Tuple<Card, Card> h;
        public MainPlayer p;

        public void Task()
        {
            h = new Tuple<Card, Card>(
                                      new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false, true),
                                      new Card(new CardType(CardSuit.CLUB, CardRank.FOUR),false, true));
            p = new MainPlayer("str", "str2", 2000, h);
        }
    }
}
