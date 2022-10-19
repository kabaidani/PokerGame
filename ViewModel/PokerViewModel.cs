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
        public DelegateCommand RaiseOrBetButtonCommand { get; set; }
        public DelegateCommand ReleaseModelLockingKey { get; set; }

        public event EventHandler<PlayersEventArg> InitCharacters;

        public PlayerDatas LeftTopCharacter;
        public PlayerDatas MiddleTopCharacter;
        public PlayerDatas RightTopCharacter;
        public PlayerDatas RightBottomCharacter;
        public PlayerDatas MainPlayer;
        public PlayerDatas LeftBottomCharacter;
        public CommonitySectionDatas MiddleSection;
        public StatusCardsDatas StatusCards;



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

        private string _foldButtonContent = "FOLD";
        public string FoldButtonContent
        {
            get { return _foldButtonContent;}
            set
            {
                if ( value != _foldButtonContent )
                {
                    _foldButtonContent = value;
                    OnPropertyChanged("FoldButtonContent"); //Not sure that we need that
                }
            }
        }

        private string _raiseButtonContent = "RAISE";
        public string RaiseOrBetButtonContent
        {
            get { return _raiseButtonContent; }
            set
            {
                if (value != _raiseButtonContent)
                {
                    _raiseButtonContent = value;
                    OnPropertyChanged("RaiseOrBetButtonContent"); //Not sure that we need that
                }
            }
        }

        private int getCallButtonContextInteger()
        {
            string callButtonContext = CallButtonContextUpdate;
            if (callButtonContext == "Check") return -1;
            var arr = callButtonContext.Split('('); //"CALL 200)"
            arr = arr[1].Split(')'); // "200"
            int number = int.Parse(arr[0]);
            return number;
        }

        private string _callOrCheckButtonContext = "CALL";
        public string CallButtonContextUpdate
        {
            get
            {
                if (_callOrCheckButtonContext == "CALL")
                {
                    if(_model.mainPlayer.LastAction == Model.Action.SMALLBLIND)
                    {
                        int smallBlind = _model.BlindValue / 2;
                        return "Call (" + smallBlind.ToString() + ")";
                    }
                    int callStrValue = _model.getActualLicitBet() - _model.mainPlayer.BetChips;
                    if(callStrValue > _model.mainPlayer.Money) return "Call (" + _model.mainPlayer.Money + ")";
                    return "Call (" + callStrValue.ToString() + ")";

                }
                else
                {
                    return "Check";
                }
            }
            set
            {
                if (value != _callOrCheckButtonContext)
                {
                    _callOrCheckButtonContext = value;
                    OnPropertyChanged("CallButtonContextUpdate");
                }
            }
        }

        private bool _isRaiseButtonActive;
        public bool IsRaiseButtonActive
        {
            get { return _isRaiseButtonActive; }
            set
            {
                if(value != _isRaiseButtonActive)
                {
                    _isRaiseButtonActive = value;
                    OnPropertyChanged("IsRaiseButtonActive");
                }
            }
        }

        public bool IsButtonActive
        {
            get
            {
                IsRaiseButtonActive = _model.MainplayerTurn;
                return _model.MainplayerTurn;
            }
        }

        public string RaiseBetTextValue // need to set a setter for input value
        {
            get
            {
                if (RaiseBetValue < MinRaiseBetValue)
                {
                    if (_raiseButtonContent == "RAISE")
                    {
                        RaiseBetValue = MinRaiseBetValue;
                    }
                    else //Case of BET
                    {
                        RaiseBetValue = _model.BlindValue; //TODO Make a function in the model for this purpose
                    }
                }
                if (_raiseOrBetValue > _model.mainPlayer.Money)
                {
                    int i = RaiseBetValue;
                    _raiseOrBetValue = _model.mainPlayer.Money;
                    RaiseBetValue = _model.mainPlayer.Money;

                    int callValue = getCallButtonContextInteger();
                    if (_raiseOrBetValue == callValue || _raiseOrBetValue < callValue)
                    {
                        IsRaiseButtonActive = false;
                    }
                }
                return RaiseBetValue.ToString();
            }
        }

        private int _raiseOrBetValue;
        public int RaiseBetValue
        {
            get
            {
                return _raiseOrBetValue;
            }

            set
            {
                if (value != _raiseOrBetValue)
                {
                    _raiseOrBetValue = value;
                    OnPropertyChanged("RaiseBetValue");
                    OnPropertyChanged("RaiseBetTextValue");
                    OnPropertyChanged("MinRaiseBetValue");
                    OnPropertyChanged("MaxRaiseBetValue");
                    OnPropertyChanged("CallButtonContextUpdate");
                }
            }
        }

        private int _minRaiseOrBetValue;
        public int MinRaiseBetValue
        {
            get
            {
                return _minRaiseOrBetValue;
            }
            set
            {
                if(value != _minRaiseOrBetValue)
                {
                    _minRaiseOrBetValue = value;
                    OnPropertyChanged("RaiseBetTextValue");
                    OnPropertyChanged("MinRaiseBetValue");
                }
            }
        }

        public int MaxRaiseBetValue
        {
            get
            {
                return _model.mainPlayer.Money;
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

        private void OnRefreshPlayers(object sender, EventArgs e) //Is it used???
        {
            foreach (var character in _characters)
            {
                character.Value.PropertyChange();
            }
        }

        private void OnRefreshGivenPlayers(object sender, PlayersEventArg e)
        {
            foreach (var p in e.Players) _characters[p.StaticName].PropertyChange();
        }


        private Dictionary<string, PlayerDatas> _characters;

        public PokerViewModel(PokerModel model, CharacterTypes mainPlayerCharacter)
        {
            _model = model;
            _model.GeneratePlayers(mainPlayerCharacter);
            _model.CardAllocation += OnCardAllocation;

            FoldButtonCommand = new DelegateCommand(p => _model.MainPlayerAction(Model.Action.FOLD)); // p meanse mainplayer
            CallOrCheckButtonCommand = new DelegateCommand(t => CallOrCheckCommand(t));
            RaiseOrBetButtonCommand = new DelegateCommand(p => RaiseOrBetCommand(p));
            ReleaseModelLockingKey = new DelegateCommand(p => _model.ReleaseLockingKey());


            _model.UpdateMiddleSectionEvent += OnUpdateMiddleSectionEvent;
            _model.PlayerActionEvent += OnPlayerActionEvent;
            _model.SignPlayerEvent += OnSignPlayerEvent;
            _model.RoundOverForPlayersEvent += OnRoundOverForPlayersEvent;
            _model.RefreshRemainTime += OnRefreshRemainTime;
            _model.DealerChipPass += OnDealerChipPass;
            _model.MainPlayerTurnEvent += OnMainPlayerTurnEvent;
            _model.CheckCombinationEvent += OnCombinationEvent;
            _model.RefreshPlayers += OnRefreshPlayers;
            _model.RefreshGivenPlayers += OnRefreshGivenPlayers;
            _model.RefreshCommonityBet += OnRefreshCommonityBet;
            _model.LockingKeyStateChangeEvent += OnLockingKeyStateChangeEvent;
            _model.UpdateGainedPrizeEvent += OnUpdateGainedPrizeEvent;
            _model.mainPlayer.SetActionOptionsEvent += OnSetActionOptionsEvent;
            _model.GameOverEvent += OnGameOverEvent;
            _model.BlindValuesEvent += OnOnBlindValuesEvent;

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
            StatusCards = new StatusCardsDatas(_model.mainPlayer);

        }

        private void OnMainPlayerTurnEvent(object sender, EventArgs e)
        {
            OnPropertyChanged("IsButtonActive");
            OnPropertyChanged("ActionButtonsVisible");
            OnPropertyChanged("IsLockingVisible");
        }

        private void OnCombinationEvent(object sender, CommonityCardsEventArgs e)
        {
            StatusCards.statusCards = e.CommonityCards;
            StatusCards.PropertyChange();
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

        private void OnRoundOverForPlayersEvent(object sender, PlayersEventArg e)
        {
            foreach (var player in e.Players)
            {
                if(player.LastAction != Model.Action.FOLD) _characters[player.StaticName].RoundOverUpdate();
            }
        }

        private void OnSetActionOptionsEvent(object sender, PossibleActionsEventArgs e)
        {
            if (e.PossibleActions.Contains(PokerGame.Model.Action.FOLD)) FoldButtonContent = "FOLD";

            if (e.PossibleActions.Contains(PokerGame.Model.Action.RAISE))
            {
                IsRaiseButtonActive = true;
                RaiseOrBetButtonContent = "RAISE";
            } else if (e.PossibleActions.Contains(PokerGame.Model.Action.BET))
            {
                IsRaiseButtonActive = true;
                RaiseBetValue = _model.BlindValue;
                RaiseOrBetButtonContent = "BET";
            }else
            {
                IsRaiseButtonActive = false;
            }

            OnPropertyChanged("MaxRaiseBetValue");


            if (e.PossibleActions.Contains(PokerGame.Model.Action.CHECK)) CallButtonContextUpdate = "CHECK";
            if (e.PossibleActions.Contains(PokerGame.Model.Action.CALL)) CallButtonContextUpdate = "CALL";


            OnPropertyChanged("CallButtonContextUpdate");
            MinRaiseBetValue = _model.mainPlayer.MinRaiseBet;

        }

        private void OnGameOverEvent(Object sender, EventArgs e)
        {
            //MAybe it should be somewhere else in a sperated ViewModel
        }

        private void OnOnBlindValuesEvent(Object sender, EventArgs e)
        {
            OnPropertyChanged("BlindValues");
        }

        private void OnPlayerActionEvent(object sender, PokerPlayerEventArgs e)
        {
            _characters[e.Player.StaticName].PropertyChange();
        }

        private void OnUpdateGainedPrizeEvent(object sender, PlayersEventArg e)
        {
            foreach(var player in e.Players)
            {
                _characters[player.StaticName].ShowGainedPrize();
            }
        }


        private void CallOrCheckCommand(object o)
        {
            string buttonContext = Convert.ToString(o);
            if(buttonContext == "Check")
            {
                _model.MainPlayerAction(Model.Action.CHECK); //Correct in the model
            }else
            {
                _model.MainPlayerAction(Model.Action.CALL); //Correct in the model
            }
        }

        private void RaiseOrBetCommand(object o)
        {
            string buttonContext = Convert.ToString(o);
            if (buttonContext == "RAISE")
            {
                _model.MainPlayerAction(Model.Action.RAISE, RaiseBetValue); //Correct in the model
            }
            else
            {
                _model.MainPlayerAction(Model.Action.BET, RaiseBetValue); //Correct in the model
            }
        }

        public void OnUpdateMiddleSectionEvent(Object sender, EventArgs e)
        {
            MiddleSection.PropertyChange();
        }

        public void OnRefreshCommonityBet(object sender, EventArgs e)
        {
            MiddleSection.CommonityBetpropertyChange();
        }


        public System.Windows.Visibility ActionButtonsVisible
        {
            get
            {
                if (IsButtonActive) return System.Windows.Visibility.Visible;
                return System.Windows.Visibility.Collapsed;
            }
        }

        //public System.Windows.Visibility LockingKeyReleaser
        //{
        //    get
        //    {
        //        if (_model.CheckLockingKeyState()) return System.Windows.Visibility.Visible;
        //        return System.Windows.Visibility.Collapsed;
        //    }
        //}

        public System.Windows.Visibility IsLockingVisible
        {
            get
            {
                if (!IsButtonActive) return System.Windows.Visibility.Visible;
                return System.Windows.Visibility.Collapsed;
            }
        }

        public bool IsLockingKeyEnabled
        {
            get
            {
                return _model.CheckLockingKeyState();
            }
        }

        public void OnLockingKeyStateChangeEvent(object sender, EventArgs e)
        {
            OnPropertyChanged("ActionButtonsVisible");
            OnPropertyChanged("LockingKeyReleaser");
            OnPropertyChanged("IsLockingVisible");
            OnPropertyChanged("IsLockingKeyEnabled");
        }

        public void InitCharacterEventRaise()
        {
            OnInitCharacters(_model.playerContainer);
        }


    }
}
