using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace PokerGame.Model
{
    public class PokerModel
    {
        public List<Player> playerContainer;
        private Tuple<Card, Card> h;
        public MainPlayer p;

        public void GeneratePlayers()
        {
            playerContainer = new List<Player>();
            playerContainer.Add(new BotPlayer(CharacterTypes.JOSEPH, 2000, new Tuple<Card, Card>(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false), new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false))));
            playerContainer.Add(new BotPlayer(CharacterTypes.OLAF, 2000, new Tuple<Card, Card>(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false), new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false))));
            h = new Tuple<Card, Card>(
                                      new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false),
                                      new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            p = new MainPlayer("str", CharacterTypes.BOB, 2000, h);
            playerContainer.Add(p);
            
        }
    }
}
