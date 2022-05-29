using System.Windows;
using PokerGame.Model;

namespace PokerGame.ViewModel
{
    public class CommonitySectionDatas : ViewModelBase
    {
        private MiddleField _middleSection;

        public CommonitySectionDatas(MiddleField middleField)
        {
            _middleSection = middleField;
        }

        public string FirstCardUrl
        {
            get
            {
                if (_middleSection.CommonityCards.Count < 1)
                {
                    return "../Image/cardBack.png";
                }
                else
                {
                    string rank = ((int)_middleSection.CommonityCards[0].cardType.cardRank).ToString();
                    string suit = _middleSection.CommonityCards[0].cardType.cardSuit.ToString();
                    return "../../Image/Deck/" + rank + suit + ".png";
                }
            }
        }

        public Visibility FirstCardVisibility
        {
            get
            {
                if (_middleSection.CommonityCards.Count < 1)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }


        public string SecondCardUrl
        {
            get
            {
                if (_middleSection.CommonityCards.Count < 2)
                {
                    return "../Image/cardBack.png";
                }
                else
                {
                    string rank = ((int)_middleSection.CommonityCards[1].cardType.cardRank).ToString();
                    string suit = _middleSection.CommonityCards[1].cardType.cardSuit.ToString();
                    return "../../Image/Deck/" + rank + suit + ".png";
                }
            }
        }

        public Visibility SecondCardVisibility
        {
            get
            {
                if (_middleSection.CommonityCards.Count < 2)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public string ThirdCardUrl
        {
            get
            {
                if (_middleSection.CommonityCards.Count < 3)
                {
                    return "../Image/cardBack.png";
                }
                else
                {
                    string rank = ((int)_middleSection.CommonityCards[2].cardType.cardRank).ToString();
                    string suit = _middleSection.CommonityCards[2].cardType.cardSuit.ToString();
                    return "../../Image/Deck/" + rank + suit + ".png";
                }
            }
        }

        public Visibility ThirdCardVisibility
        {
            get
            {
                if (_middleSection.CommonityCards.Count < 3)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public string FourthCardUrl
        {
            get
            {
                if (_middleSection.CommonityCards.Count < 4)
                {
                    return "../Image/cardBack.png";
                }
                else
                {
                    string rank = ((int)_middleSection.CommonityCards[3].cardType.cardRank).ToString();
                    string suit = _middleSection.CommonityCards[3].cardType.cardSuit.ToString();
                    return "../../Image/Deck/" + rank + suit + ".png";
                }
            }
        }

        public Visibility FourthCardVisibility
        {
            get
            {
                if (_middleSection.CommonityCards.Count < 4)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public string FifthCardUrl
        {
            get
            {
                if (_middleSection.CommonityCards.Count < 5)
                {
                    return "../Image/cardBack.png";
                }
                else
                {
                    string rank = ((int)_middleSection.CommonityCards[4].cardType.cardRank).ToString();
                    string suit = _middleSection.CommonityCards[4].cardType.cardSuit.ToString();
                    return "../../Image/Deck/" + rank + suit + ".png";
                }
            }
        }

        public Visibility FifthCardVisibility
        {
            get
            {
                if (_middleSection.CommonityCards.Count < 5)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public string CommonityBetPictureUrl
        {
            get
            {
                return "../Image/chips.png";
            }
        }

        public string CommonityBetValue
        {
            get
            {
                return "€ " + _middleSection.CommonityBet.ToString();
            }
        }

        public Visibility CommonityBetPictureAndValueVisibility
        {
            get
            {
                if (_middleSection.CommonityBet == 0)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public void PropertyChange()
        {
            OnPropertyChanged("FirstCardUrl");
            OnPropertyChanged("FirstCardVisibility");
            OnPropertyChanged("SecondCardUrl");
            OnPropertyChanged("SecondCardVisibility");
            OnPropertyChanged("ThirdCardUrl");
            OnPropertyChanged("ThirdCardVisibility");
            OnPropertyChanged("FourthCardUrl");
            OnPropertyChanged("FourthCardVisibility");
            OnPropertyChanged("FifthCardUrl");
            OnPropertyChanged("FifthCardVisibility");
            OnPropertyChanged("CommonityBetPictureUrl");
            OnPropertyChanged("CommonityBetValue");
            OnPropertyChanged("CommonityBetPictureAndValueVisibility");
        }

        public void CommonityBetpropertyChange()
        {
            OnPropertyChanged("CommonityBetPictureUrl");
            OnPropertyChanged("CommonityBetValue");
            OnPropertyChanged("CommonityBetPictureAndValueVisibility");
        }

    }
}
