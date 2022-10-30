using System;
using System.Collections.Generic;

namespace PokerGame.Model
{
    public class MiddleField
    {
        private Dictionary<Player, int> _playersBet;
        private List<Player> _players;
        private Deck _deck;

        public List<Card> CommonityCards { private set; get; }
        public int CommonityBet { private set; get; }

        public MiddleField(Deck deck, List<Player> players)
        {
            CommonityCards = new List<Card>();
            _deck = deck;
            _playersBet = new Dictionary<Player, int>();
            _players = new List<Player>();
            foreach (var player in players)
            {
                _playersBet.Add(player, 0);
                _players.Add(player);
            } 
        }

        public void ClearTheSection()
        {
            CommonityCards.Clear();
            CommonityBet = 0; //Not sure if it is needed here
        }

        public Card CardGenerator(Random rand, bool isUpdsideDown = true)
        { 
            Card result = _deck.getCard();
            result.isUpSideDown = isUpdsideDown;
            return result;
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
                int playerBet = player.CollectBet();
                _playersBet[player] += playerBet;
                ammount += playerBet;
            }
            CommonityBet += ammount;
        }

        private int HighestBetCalculator()
        {
            int highestBet = 0;
            foreach (var playersBet in _playersBet)
            {
                if (highestBet < playersBet.Value)
                {
                    highestBet = playersBet.Value;
                }
            }
            return highestBet;
        }

        public bool PrizeDistribution(List<Player> winners)
        {
            int commonityBetConst = CommonityBet;
            int heighestBet = HighestBetCalculator(); //The highest bet is the normal bet
            int partsNumber = winners.Count;
            List<Player> fullBetPlayers = new List<Player>();
            List<Player> partBetPlayers = new List<Player>();
            foreach (var player in winners)
            {
                if(_playersBet[player] == heighestBet)
                {
                    fullBetPlayers.Add(player);
                }else
                {
                    partBetPlayers.Add(player);
                }
            }
            foreach(var player in partBetPlayers)
            {
                double multiplier = 1.0 / partsNumber;
                double multiplier2 = Convert.ToDouble(_playersBet[player]) / heighestBet;
                double prize = commonityBetConst * multiplier * multiplier2;
                player.gainPrize(Convert.ToInt32(Math.Floor(prize)));
                CommonityBet -= Convert.ToInt32(Math.Floor(prize));
            }
            foreach(var player in fullBetPlayers)
            {
                int prize = CommonityBet / fullBetPlayers.Count;
                player.gainPrize(prize);
            }
            if (fullBetPlayers.Count > 0)
            {
                CommonityBet = 0;
                foreach(var player in _players)
                {
                    _playersBet[player] = 0;
                }

                return false;
            } return true;
        }


    }
}
