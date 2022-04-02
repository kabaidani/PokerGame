using System;
using System.Collections.Generic;
using System.Text;
using PokerGame.Model;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Media;

namespace PokerGame.ViewModel
{

    public class PokerViewModel : ViewModelBase
    {
        private PokerModel _model;

        public DelegateCommand FoldButtonCommand { get; set; }
        public DelegateCommand CallOrCheckButtonCommand { get; set; }
        public DelegateCommand RaiseButtonCommand { get; set; }

        public event EventHandler<PlayersEventArg> InitCharacters;

        public PlayerDatas LeftTopCharacter;
        public PlayerDatas MiddleTopCharacter;
        public PlayerDatas RightTopCharacter;
        public PlayerDatas RightBottomCharacter;
        public PlayerDatas MainPlayer;
        public PlayerDatas LeftBottomCharacter;
        public CommonitySectionDatas MiddleSection;



        private string _timeRowColor = "Green";
        public string TimeRowColor
        {
            get { return _timeRowColor; }
            set
            {
                if(value != _timeRowColor)
                {
                    _timeRowColor = value;
                    OnPropertyChanged("TimeRowColor");
                }
            }
        }

        public int TimeForRound
        {
            get
            {
                return 10000;
            }
        }

        private int _remainTime = 10000;
        public int RemainTime
        {
            get
            {
                return _remainTime;
            }

            set
            {
                if(value != _remainTime)
                {
                    _remainTime = value;
                    OnPropertyChanged("RemainTime");
                }
            }
        }


        public string CallButtonContextUpdate
        {
            get
            {
                if (_model.getActualLicitBet() != 0)
                {
                    return "Call (" + _model.getActualLicitBet().ToString() + ")";
                }
                else
                {
                    return "Check";
                }
            }
        }

        public bool IsButtonActive
        {
            get
            {
                return _model.MainplayerTurn;
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


        public string BlindValues
        {
            get
            {
                return "€ " + _model.BlindValue.ToString() + " / " + (_model.BlindValue / 2).ToString();
            }
        }


        private void OnCardAllocation(object sender, PokerPlayerEventArgs e)
        {
            _characters[e.Player.StaticName].PropertyChange();
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

        }

        private Dictionary<string, PlayerDatas> _characters;

        public PokerViewModel(PokerModel model)
        {
            _model = model;
            _model.GeneratePlayers();
            _model.CardAllocation += OnCardAllocation;

            FoldButtonCommand = new DelegateCommand(p =>  _model.AsyncTestUnFoldMiddleCards() /*_model.MainPlayerAction(Model.Action.FOLD)*/); // p meanse mainplayer
            CallOrCheckButtonCommand = new DelegateCommand(t => CallOrCheckCommand(t));
            RaiseButtonCommand = new DelegateCommand(p => _model.MainPlayerAction(Model.Action.RAISE));


            _model.UnfoldCardEvent += OnUnFoldCardEvent;
            _model.PlayerActionEvent += OnPlayerActionEvent;
            _model.SignPlayerEvent += OnSignPlayerEvent;
            _model.RefreshRemainTime += OnRefreshRemainTime;
            _model.DealerChipPass += OnDealerChipPass;
            _model.MainPlayerTurnEvent += OnMainPlayerTurnEvent;

            _characters = new Dictionary<string, PlayerDatas>();
            
            RightTopCharacter = new PlayerDatas(_model.playerContainer[0]);
            _characters[_model.playerContainer[0].StaticName] = RightTopCharacter;
            RightBottomCharacter = new PlayerDatas(_model.playerContainer[1]);
            _characters[_model.playerContainer[1].StaticName] = RightBottomCharacter;
            MainPlayer = new PlayerDatas(_model.playerContainer[2]);
            _characters[_model.playerContainer[2].StaticName] = MainPlayer;
            LeftBottomCharacter = new PlayerDatas(_model.playerContainer[3]);
            _characters[_model.playerContainer[3].StaticName] = LeftBottomCharacter;
            LeftTopCharacter = new PlayerDatas(_model.playerContainer[4]);
            _characters[_model.playerContainer[4].StaticName] = LeftTopCharacter;
            MiddleTopCharacter = new PlayerDatas(_model.playerContainer[5]);
            _characters[_model.playerContainer[5].StaticName] = MiddleTopCharacter;


            MiddleSection = new CommonitySectionDatas(_model.MiddleFieldSection);
            }

        private void OnMainPlayerTurnEvent(object sender, EventArgs e)
        {
            OnPropertyChanged("IsButtonActive");
        }

        private void OnRefreshRemainTime(object sender, EventArgs e)
        {
            if(_model.RemaineTime < 10000 / 4)
            {
                TimeRowColor = "Red";
            }else if (_model.RemaineTime < 10000 / 2)
            {
                TimeRowColor = "Orange";
            }
            else
            {
                TimeRowColor = "Green";
            }

            RemainTime = _model.RemaineTime;
        }

        private void OnDealerChipPass(object sender, EventArgs e)
        {
            LeftTopCharacter.PropertyChange("DealerChipPictureVisibility");
            MiddleTopCharacter.PropertyChange("DealerChipPictureVisibility");
            RightTopCharacter.PropertyChange("DealerChipPictureVisibility");
            LeftBottomCharacter.PropertyChange("DealerChipPictureVisibility");
            MainPlayer.PropertyChange("DealerChipPictureVisibility");
            RightBottomCharacter.PropertyChange("DealerChipPictureVisibility");
        }


        private void OnSignPlayerEvent(object sender, PokerPlayerEventArgs e)
        {
            _characters[e.Player.StaticName].PropertyChange("ProfilePictureURL");
        }

        private void OnPlayerActionEvent(object sender, ActionEvenArgs e)
        {
            _characters[e.Player.StaticName].PropertyChange();
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
            LeftTopCharacter.PropertyChange();
            MiddleTopCharacter.PropertyChange();
            RightTopCharacter.PropertyChange();
            LeftBottomCharacter.PropertyChange();
            MainPlayer.PropertyChange();
            RightBottomCharacter.PropertyChange();

            MiddleSection.PropertyChange();
        }

        public void InitCharacterEventRaise()
        {
            OnInitCharacters(_model.playerContainer);
        }

    }
}
