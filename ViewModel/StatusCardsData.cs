using PokerGame.Model;
using System.Collections.Generic;

namespace PokerGame.ViewModel
{
    public class StatusCardsData : ViewModelBase
    {
        public List<Card> statusCards;
        private Player _mainPlayer;

        public StatusCardsData(Player player)
        {
            _mainPlayer = player;
        }

        private string GetUrl(int cardIdx)
        {
            if(_mainPlayer.LastAction == Action.FOLD)
            {
                return "../Image/cardBack.png";
            }
            if (statusCards == null || statusCards.Count < (cardIdx+1))
            {
                return "../Image/cardBack.png";
            }
            else
            {
                string rank = ((int)statusCards[cardIdx].cardType.cardRank).ToString();
                string suit = statusCards[cardIdx].cardType.cardSuit.ToString();
                return "../../Image/Deck/" + rank + suit + ".png";
            }
        }

        public void PropertyChange()
        {
            OnPropertyChanged("FirstCardUrl");
            OnPropertyChanged("SecondCardUrl");
            OnPropertyChanged("ThirdCardUrl");
            OnPropertyChanged("FourthCardUrl");
            OnPropertyChanged("FifthCardUrl");
        }

        public string FirstCardUrl { get { return GetUrl(0); } }
        public string SecondCardUrl { get { return GetUrl(1); } }
        public string ThirdCardUrl { get { return GetUrl(2); } }
        public string FourthCardUrl { get { return GetUrl(3); } }
        public string FifthCardUrl { get { return GetUrl(4); } }

    }
}
