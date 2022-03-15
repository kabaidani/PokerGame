using System;
using System.Collections.Generic;
using System.Text;
using PokerGame.Model;
using System.Collections;

namespace PokerGame.ViewModel
{
    public static class Characters // Shoould be solved by mapped values eg.: {("Character1", "LeftTopCharacter")}
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





        private void OnCardAllocation(object sender, PokerPlayerEventArgs e)
        {
            if(e.Player.StaticName == "Character1")
            {
                LeftTopCharacter.PropertyChange();
            } else if (e.Player.StaticName == "Character2")
            {
                MiddleTopCharacter.PropertyChange();
            } else if (e.Player.StaticName == "Character3")
            {
                RightTopCharacter.PropertyChange();
            } else if (e.Player.StaticName == "Character4")
            {
                RightBottomCharacter.PropertyChange();
            } else if (e.Player.StaticName == "Character5")
            {
                LeftBottomCharacter.PropertyChange();
            } else if (e.Player.StaticName == "MainPlayer")
            {
                MainPlayer.PropertyChange();
            }
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

            LeftTopCharacter = new PlayerDatas(_model.playerContainer[0]);
            MiddleTopCharacter = new PlayerDatas(_model.playerContainer[1]);
            RightTopCharacter = new PlayerDatas(_model.playerContainer[2]);
            RightBottomCharacter = new PlayerDatas(_model.playerContainer[3]);
            LeftBottomCharacter = new PlayerDatas(_model.playerContainer[5]);
            MainPlayer = new PlayerDatas(_model.playerContainer[4]);
            MiddleSection = new CommonitySectionDatas(_model.MiddleFieldSection);
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

        private void OnSignPlayerEvent(object sender, PokerPlayerEventArgs e)
        {
            if (e.Player.StaticName == "Character1")
            {
                LeftTopCharacter.PropertyChange("ProfilePictureURL");
            }
            else if (e.Player.StaticName == "Character2")
            {
                MiddleTopCharacter.PropertyChange("ProfilePictureURL");
            }
            else if (e.Player.StaticName == "Character3")
            {
                RightTopCharacter.PropertyChange("ProfilePictureURL");
            }
            else if (e.Player.StaticName == "Character4")
            {
                RightBottomCharacter.PropertyChange("ProfilePictureURL");
            }
            else if (e.Player.StaticName == "Character5")
            {
                LeftBottomCharacter.PropertyChange("ProfilePictureURL");
            }
            else if (e.Player.StaticName == "MainPlayer")
            {
                MainPlayer.PropertyChange("ProfilePictureURL");
            }
        }

        private void OnPlayerActionEvent(object sender, ActionEvenArgs e)
        {
            if (e.Player.StaticName == "Character1")
            {
                LeftTopCharacter.PropertyChange();
            }
            else if (e.Player.StaticName == "Character2")
            {
                MiddleTopCharacter.PropertyChange();
            }
            else if (e.Player.StaticName == "Character3")
            {
                RightTopCharacter.PropertyChange();
            }
            else if (e.Player.StaticName == "Character4")
            {
                RightBottomCharacter.PropertyChange();
            }
            else if (e.Player.StaticName == "Character5")
            {
                LeftBottomCharacter.PropertyChange();
            }
            else if (e.Player.StaticName == "MainPlayer")
            {
                MainPlayer.PropertyChange();
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

        //private void TestCommand(Player player)
        //{
        //    //_model.TestUnFoldMiddleCards();
        //    OnShowCardsEvent(player);
        //}
    }
}
