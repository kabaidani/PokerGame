using System.Windows;
using PokerGame.Model;

namespace PokerGame.ViewModel
{

    public class PlayerData : ViewModelBase
    {
        private Player _player;
        private bool _roundOverState;
        private bool _showGainedPrize;
        
        public PlayerData(Player player)
        {
            _player = player;
            _roundOverState = false;
        }


        public Visibility GridVisibility
        {
            get
            {
                return Visibility.Visible; // Maybe it will always be Visible
            }
        }

        public string ProfilePictureURL
        {
            get
            {
                if (!_player.InGame)
                {
                    return "../../Image/NOONE.png";
                }
                else if (_player.Signed)
                {
                    return "../../Image/SignedCharacters/" + _player.Character.ToString() + ".png";
                }
                else
                {
                    return "../../Image/" + _player.Character.ToString() + ".png";
                }
            }
        }

        public Visibility LeftHandVisibility
        {
            get
            {
                if(!_player.InGame || _player.hand.leftHand.cardType.cardRank == CardRank.NOCARD)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public Visibility RightHandVisibility
        {
            get
            {
                if (!_player.InGame || _player.hand.rightHand.cardType.cardRank == CardRank.NOCARD)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public string LeftHandCardUrl
        {
            get
            {
                if (!_player.InGame || _player.hand.leftHand.isUpSideDown)
                {
                    return "../Image/cardBack.png";
                } else
                {
                    string rank = ((int)_player.hand.leftHand.cardType.cardRank).ToString();
                    string suit = _player.hand.leftHand.cardType.cardSuit.ToString();
                    return "../../Image/Deck/" + rank + suit + ".png";
                }
            }
        }

        public string RightHandCardUrl
        {
            get
            {
                if (!_player.InGame || _player.hand.rightHand.isUpSideDown)
                {
                    return "../Image/cardBack.png";
                }
                else
                {
                    string rank = ((int)_player.hand.rightHand.cardType.cardRank).ToString();
                    string suit = _player.hand.rightHand.cardType.cardSuit.ToString();
                    return "../../Image/Deck/" + rank + suit + ".png";
                }
            }
        }

        public Visibility BetPictureAndValueVisibility
        {
            get
            {
                if (!_player.InGame || _player.BetChips == 0)
                {
                    return Visibility.Hidden;
                }else
                {
                    return Visibility.Visible;
                }
            }
        }

        public string BetPictureUrl
        {
            get
            {
                return "../Image/chips.png";
            }
        }

        public string BetValue
        {
            get
            {
                if (!_player.InGame) return "";

                return _player.BetChips.ToString();
            }
        }

        public string CharacterNameText
        {
            get
            {
                if (!_player.InGame) return "";
                return _player.Character.ToString();
            }
        }

        public string MoneyTextBox
        {
            get
            {
                if (!_player.InGame) return "";
                if (_showGainedPrize)
                {
                    _showGainedPrize = false;
                    return "+ €" + _player.GetGainedPrize().ToString();
                }
                return "€ " + _player.Money.ToString();
            }
        }

        public string LastActionTextBox
        {
            get
            {
                if (_roundOverState)
                {
                    _roundOverState = false;
                    return _player.PokerHandRanks.ToString();
                }
                if (!_player.InGame || _player.LastAction == Action.NOACTION) return "";
                return _player.LastAction.ToString();
            }
        }

        public string DealerChipPicture
        {
            get
            {
                return "../../Image/dealerChip.png";
            }
        }

        public Visibility DealerChipPictureVisibility
        {
            get
            {
                if (_player.Role.dealer)
                {
                    return Visibility.Visible;
                }else
                {
                    return Visibility.Hidden;
                }
            }
        }


        public void PropertyChange(string propertyName = "")
        {
            if(propertyName == "")
            {
                OnPropertyChanged("LeftHandVisibility");
                OnPropertyChanged("RightHandVisibility");
                OnPropertyChanged("LeftHandCardUrl");
                OnPropertyChanged("RightHandCardUrl");
                OnPropertyChanged("BetPictureAndValueVisibility");
                OnPropertyChanged("BetPictureUrl");
                OnPropertyChanged("BetValue");
                OnPropertyChanged("CharacterNameText");
                OnPropertyChanged("MoneyTextBox");
                OnPropertyChanged("LastActionTextBox");
                OnPropertyChanged("GridVisibility");
                OnPropertyChanged("DealerChipPicture");
                OnPropertyChanged("DealerChipPictureVisibility");
                OnPropertyChanged("ProfilePictureURL");
            }
            else
            {
                OnPropertyChanged(propertyName);
            }
        }

        public void RoundOverUpdate()
        {
            _roundOverState = true;
            PropertyChange("ProfilePictureURL");
            OnPropertyChanged("");
        }

        public void ShowGainedPrize()
        {
            _showGainedPrize = true;
            OnPropertyChanged("MoneyTextBox");
        }
    }
}
