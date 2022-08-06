using System;
using System.Collections.Generic;

namespace PokerGame.Model
{
    public class PossibleActionsEventArgs : EventArgs
    {
        public PossibleActionsEventArgs(List<Action> possibleActions)
        {
            PossibleActions = possibleActions;
        }
        public List<Action> PossibleActions { private set; get; }
    }
}
