using System;
using System.Collections.Generic;
using System.Text;
using PokerGame.Model;
using System.Collections;


namespace PokerGame.ViewModel
{

    public static class Characters
    {
        public static string Charcter1 = "LeftTopCharacter";
        public static string Charcter2 = "MiddleTopCharacter";
        public static string Charcter3 = "RightTopCharacter";
        public static string Charcter4 = "RightBottomCharacter";
        public static string Charcter5 = "LeftBottomCharacter";
        public static string MainPlayer = "MainPlayer";
    }

    public class PokerViewModel : ViewModelBase
    {
        private PokerModel _model;
        public DelegateCommand TestButtonCommand { get; set; }

        public event EventHandler<PokerPlayerEventArgs> ShowCardsEvent;
        public event EventHandler<PlayersEventArg> InitCharacters;
        public event EventHandler<PlayersEventArg> CardAllocationEvent;

        private void OnCardAllocationEvent()
        {
            if (CardAllocationEvent != null)
            {
                CardAllocationEvent(this, new PlayersEventArg(_model.playerContainer));
            }
        }
        private void OnCardAllocation(object sender, EventArgs e)
        {
            OnCardAllocationEvent();
        }

        private void OnInitCharacters(List<Player> players)
        {
            if(InitCharacters != null)
            {
                InitCharacters(this, new PlayersEventArg(players));
            }
        }

        private void OnShowCardsEvent(Player player)
        {
            if(ShowCardsEvent != null)
            {
                ShowCardsEvent(this, new PokerPlayerEventArgs(player));
            }
        }

        public PokerViewModel(PokerModel model)
        {
            _model = model;
            _model.GeneratePlayers();
            _model.CardAllocation += OnCardAllocation;
            TestButtonCommand = new DelegateCommand(p => TestCommand(_model.p));
        }

        public void InitCharacterEventRaise()
        {
            OnInitCharacters(_model.playerContainer);
        }

        private void TestCommand(Player player)
        {
            OnShowCardsEvent(player);
        }
    }
}
