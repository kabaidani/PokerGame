using System;

namespace PokerGame.Model
{
    public class ActionEvenArgs : EventArgs
    {
        public Player Player { get; private set; }

        public ActionEvenArgs(Player player)
        {
            Player = player;
        }
    }
}
