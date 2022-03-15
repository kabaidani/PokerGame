using System.Windows;
using PokerGame.Model;

namespace PokerGame.ViewModel
{

    public class PlayerDatas : ViewModelBase
    {
        private Player _player;
        
        public PlayerDatas(Player player)
        {
            _player = player;
        }

        public Visibility GridVisibility
        {
            get
            {
                if (!_player.InGame)
                {
                    return Visibility.Hidden;
                }else
                {
                    return Visibility.Visible;
                }
            }
        }

        public string ProfilePictureURL
        {
            get
            {
                if (_player.Signed)
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
                if(_player.hand.leftHand.cardType.cardRank == CardRank.NOCARD)
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
                if (_player.hand.rightHand.cardType.cardRank == CardRank.NOCARD)
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
                if (_player.hand.leftHand.isUpSideDown)
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
                if (_player.hand.rightHand.isUpSideDown)
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
                if (_player.BetChips == 0)
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
                return _player.BetChips.ToString();
            }
        }

        public string CharacterNameText
        {
            get
            {
                return _player.Character.ToString();
            }
        }

        public string MoneyTextBox
        {
            get
            {
                return "€ " + _player.Money.ToString();
            }
        }

        public string LastActionTextBox
        {
            get
            {
                if (_player.LastAction == Action.NOACTION) return "";
                return _player.LastAction.ToString();
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
            }
            else
            {
                OnPropertyChanged(propertyName);
            }
        }
    }
}
