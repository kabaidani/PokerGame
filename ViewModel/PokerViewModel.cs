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
        public string RaiseButtonContent
        {
            get { return _raiseButtonContent; }
            set
            {
                if (value != _raiseButtonContent)
                {
                    _raiseButtonContent = value;
                    OnPropertyChanged("RaiseButtonContent"); //Not sure that we need that
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
                return _model.mainPlayer.RaiseBet.ToString();
            }
        }

        public int RaiseBetValue
        {
            get
            {
                return _model.mainPlayer.RaiseBet;
            }

            set
            {
                if(value != _model.mainPlayer.RaiseBet)
                {
                    _model.mainPlayer.RaiseBet = value;
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

        private void OnRefreshPlayers(object sender, EventArgs e)
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

            FoldButtonCommand = new DelegateCommand(p => _model.AsyncTestUnFoldMiddleCards() /*_model.MainPlayerAction(Model.Action.FOLD)*/); // p meanse mainplayer
            CallOrCheckButtonCommand = new DelegateCommand(t => CallOrCheckCommand(t));
            RaiseButtonCommand = new DelegateCommand(p => _model.MainPlayerAction(Model.Action.RAISE, _model.mainPlayer.RaiseBet)); //TODO rename the function so that Bet and Raise appear in it
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
            StatusCards = new StatusCardsDatas();

        }

        private void OnMainPlayerTurnEvent(object sender, EventArgs e)
        {
            OnPropertyChanged("IsButtonActive");
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
                _characters[player.StaticName].RoundOverUpdate();
            }
        }

        private void OnSetActionOptionsEvent(object sender, PossibleActionsEventArgs e)
        {
            if (e.PossibleActions.Contains(PokerGame.Model.Action.FOLD)) FoldButtonContent = "FOLD";

            if (e.PossibleActions.Contains(PokerGame.Model.Action.RAISE)) RaiseButtonContent = "RAISE";
            if (e.PossibleActions.Contains(PokerGame.Model.Action.BET)) RaiseButtonContent = "BET";

            if (e.PossibleActions.Contains(PokerGame.Model.Action.CHECK))
            {
                if (_model.getActualLicitBet() != 0) throw new PokerGameException("The CHECK action not allowed here");
                OnPropertyChanged("CallButtonContextUpdate");
            }
            if (e.PossibleActions.Contains(PokerGame.Model.Action.CALL))
            {

                if (_model.getActualLicitBet() == 0) throw new PokerGameException("The BET action not allowed here");
                OnPropertyChanged("CallButtonContextUpdate");
            }
            OnPropertyChanged("MinRaiseBetValue");

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
                if (_model.CheckLockingKeyState()) return System.Windows.Visibility.Collapsed;
                return System.Windows.Visibility.Visible;
            }
        }

        public System.Windows.Visibility LockingKeyReleaser
        {
            get
            {
                if (_model.CheckLockingKeyState()) return System.Windows.Visibility.Visible;
                return System.Windows.Visibility.Collapsed;
            }
        }

        public void OnLockingKeyStateChangeEvent(object sender, EventArgs e)
        {
            OnPropertyChanged("ActionButtonsVisible");
            OnPropertyChanged("LockingKeyReleaser");
        }

        public void InitCharacterEventRaise()
        {
            OnInitCharacters(_model.playerContainer);
        }


    }
}
