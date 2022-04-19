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
using System.Windows.Controls;

// Further TODOs :
// Some pokerhand combinations to long to take place in the UI in LastAction label
// In the end round the dealer button goes to the next player, it could be problematic if the player out of the game
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


        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new PokerModel(5);
            _viewModel = new PokerViewModel(_model);
            _mainWindow = new MainWindow();


            _mainWindow.DataContext = _viewModel;

            _mainWindow.MiddleTopCharacterGrid.DataContext = _viewModel.MiddleTopCharacter;
            _mainWindow.LeftTopCharacterGrid.DataContext = _viewModel.LeftTopCharacter;
            _mainWindow.RightTopCharacterGrid.DataContext = _viewModel.RightTopCharacter;
            _mainWindow.RightBottomCharacterGrid.DataContext = _viewModel.RightBottomCharacter;
            _mainWindow.MainPlayerGrid.DataContext = _viewModel.MainPlayer;
            _mainWindow.LeftBottomCharacterGrid.DataContext = _viewModel.LeftBottomCharacter;
            _mainWindow.MiddleSectionGrid.DataContext = _viewModel.MiddleSection;
            _mainWindow.StatusCards.DataContext = _viewModel.StatusCards;

            _viewModel.InitCharacterEventRaise();


            _mainWindow.Show();

            _model.GameOn();
        }
    }
}
