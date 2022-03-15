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

        public DelegateCommand FoldButtonCommand { get; set; }
        public DelegateCommand CallOrCheckButtonCommand { get; set; }
        public DelegateCommand RaiseButtonCommand { get; set; }

        public event EventHandler<PokerPlayerEventArgs> ShowCardsEvent;
        public event EventHandler<PlayersEventArg> InitCharacters;
        public event EventHandler<PlayersEventArg> CardAllocationEvent;

        public event EventHandler<CommonityCardsEventArgs> UnFoldCardEvent;
        public event EventHandler<ActionEvenArgs> PlayerActionEvent;
        public event EventHandler<PokerPlayerEventArgs> SignPlayerEvent;


        public PlayerDatas LeftTopCharacter;
        public PlayerDatas MiddleTopCharacter;
        public PlayerDatas RightTopCharacter;
        public PlayerDatas RightBottomCharacter;
        public PlayerDatas MainPlayer;
        public PlayerDatas LeftBottomCharacter;

        public string CheckOrCallButtonContext
        {
            get
            {
                if( _model.getActualLicitBet() != 0)
                {
                    return "Call (" + _model.getActualLicitBet().ToString() + ")";
                }else
                {
                    return "Check";
                }
            }
        }

        public string RaiseBetTextValue // need to set a setter for input value
        {
            get
            {
                return _model.p.RaiseBet.ToString();
            }
        }

        public int RaiseBetValue
        {
            get
            {
                return _model.p.RaiseBet;
            }

            set
            {
                if(value != _model.p.RaiseBet)
                {
                    _model.p.RaiseBet = value;
                    OnPropertyChanged("RaiseBetTextValue");
                    OnPropertyChanged("MinRaiseBetValue");
                    OnPropertyChanged("MaxRaiseBetValue");
                    OnPropertyChanged("CheckOrCallButtonContext");
                }
            }
        }

        public int MinRaiseBetValue
        {
            get
            {
                return _model.getActualLicitBet();
            }
        }

        public int MaxRaiseBetValue
        {
            get
            {
                return _model.p.Money;
            }
        }




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

            FoldButtonCommand = new DelegateCommand(p => _model.MainPlayerAction(Model.Action.FOLD)); // p meanse mainplayer
            CallOrCheckButtonCommand = new DelegateCommand(t => CallOrCheckCommand(t));
            RaiseButtonCommand = new DelegateCommand(p => _model.MainPlayerAction(Model.Action.RAISE));

            _model.UnfoldCardEvent += OnUnFoldCardEvent;
            _model.PlayerActionEvent += OnPlayerActionEvent;
            _model.SignPlayerEvent += OnSignPlayerEvent;

            LeftTopCharacter = new PlayerDatas(_model.playerContainer[0]);
            MiddleTopCharacter = new PlayerDatas(_model.playerContainer[1]);
            RightTopCharacter = new PlayerDatas(_model.playerContainer[2]);
            RightBottomCharacter = new PlayerDatas(_model.playerContainer[3]);
            MainPlayer = new PlayerDatas(_model.playerContainer[4]);
            LeftBottomCharacter = new PlayerDatas(_model.playerContainer[5]);
        }

        private void OnSignPlayerEvent(object sender, PokerPlayerEventArgs e)
        {
            if (SignPlayerEvent != null)
            {
                SignPlayerEvent(this, e);
            }
        }

        private void OnPlayerActionEvent(object sender, ActionEvenArgs e)
        {
            if (PlayerActionEvent != null)
            {
                PlayerActionEvent(this, e);
            }
        }


        private void CallOrCheckCommand(object o)
        {
            string buttonContext = Convert.ToString(o);
            if(buttonContext == "Check")
            {
                _model.MainPlayerAction(Model.Action.CHECK);
            }else
            {
                _model.MainPlayerAction(Model.Action.CALL);
            }
        }

        public void OnUnFoldCardEvent(Object sender, CommonityCardsEventArgs e)
        {
            if (UnFoldCardEvent != null)
            {
                UnFoldCardEvent(this, e);
            }
        }

        public void InitCharacterEventRaise()
        {
            OnInitCharacters(_model.playerContainer);
        }

        //private void TestCommand(Player player)
        //{
        //    //_model.TestUnFoldMiddleCards();
        //    OnShowCardsEvent(player);
        //}
    }
}
