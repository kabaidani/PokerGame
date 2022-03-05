using System;

namespace PokerGame.Model
{
    public class PokerPlayerEventArgs : EventArgs
        {
            public PokerPlayerEventArgs(Player player)
            {
                Player = player;
            }
            public Player Player { private set; get; }
        }
}
