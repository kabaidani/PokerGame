using System;
using PokerGame.Model;


namespace PokerGame.ViewModel
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
