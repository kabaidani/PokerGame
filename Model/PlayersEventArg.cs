using System;
using System.Collections.Generic;

namespace PokerGame.Model
{
    public class PlayersEventArg : EventArgs // Export in a separate class
    {
        public PlayersEventArg(List<Player> players)
        {
            Players = players;
        }
        public List<Player> Players { get; private set; }
    }
}
