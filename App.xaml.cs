using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using PokerGame.view; // Need to correct it capital letter
using PokerGame.Model;
using PokerGame.ViewModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PokerGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _mainWindow;
        private PokerModel _model;
        private PokerViewModel _viewModel;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void OnShowCardsEvent(object sender, PokerPlayerEventArgs e) // We could narrow down the eventArg reference
        {
            string urlSource = "";
            if (e.Player.Hand.Item1.isUpSideDown)
            {
                urlSource = "../../Image/cardBack.jpg";
            } else
            {
                string rank = ((int)e.Player.Hand.Item1.cardType.cardRank).ToString();
                string tmp = e.Player.Hand.Item2.cardType.cardSuit.ToString();
                urlSource = "../../Image/Deck/" + rank + tmp + ".png";
            }
            _mainWindow.TestImageName.Visibility = Visibility.Visible;
            _mainWindow.TestImageName.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new PokerModel();
            _viewModel = new PokerViewModel(_model);
            _mainWindow = new MainWindow();
            _viewModel.ShowCardsEvent += OnShowCardsEvent;
            _mainWindow.DataContext = _viewModel;


            _mainWindow.Show();
        }
    }
}
