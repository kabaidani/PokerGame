using System;
using System.Collections.Generic;
using System.Text;
using PokerGame.Model;

namespace PokerGame.ViewModel
{
    public class GameOverViewModel : ViewModelBase
    {
        public string CharacterUrl { get; set; }
        public string SympathyContent { get; set; }
        public string ResultContent { get; set; }
        public DelegateCommand NewGameCommand { get; set; }
        public DelegateCommand CloseTheGameCommand { get; set; }


        public GameOverViewModel(bool mainPlayerWon, CharacterTypes character, PokerModel model)
        {
            if (mainPlayerWon)
            {
                SympathyContent = "Congratulations";
                ResultContent = "WON";
            } else
            {
                SympathyContent = "We are sorry";
                ResultContent = "LOST";
            }

            CharacterUrl = "../../Image/" + character.ToString() + ".png";

            NewGameCommand = new DelegateCommand(p => model.OnRequestNewGame());
            CloseTheGameCommand = new DelegateCommand(p => model.OnCloseGameEvent());
        }
    }
}
