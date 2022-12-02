using System;
using System.Collections.Generic;

namespace PokerGame.Model
{
    public class AllPokerPlayerEventArgs : EventArgs
    {
        public AllPokerPlayerEventArgs(List<Player> players)
        {
            Players = players;
        }
        public List<Player> Players { private set; get; }
    }
}
