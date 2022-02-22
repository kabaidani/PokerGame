using System;
using System.Collections.Generic;
using System.Text;
using PokerGame.Model;


namespace PokerGame.ViewModel
{

    public class PokerViewModel : ViewModelBase
    {
        private PokerModel _model;
        public DelegateCommand TestButtonCommand { get; set; }

        public event EventHandler<PokerPlayerEventArgs> ShowCardsEvent;

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
            _model.Task();
            TestButtonCommand = new DelegateCommand(p => TestCommand(_model.p));
        }

        private void TestCommand(Player player)
        {
            OnShowCardsEvent(player);
        }
    }
}
