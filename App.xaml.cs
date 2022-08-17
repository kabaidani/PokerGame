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
        private CharcterSelecter _charcterSelecterWindow;
        private GameOverWindow _gameOverWindow;


        private CharacterSelecterModel _characterSelecterModel;
        private CharacterSelecterViewModel _characterSelecterViewModel;
        private GameOverViewModel _gameOverViewModel;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }


        private void App_Startup(object sender, StartupEventArgs e)
        {
            _characterSelecterModel = new CharacterSelecterModel();
            _characterSelecterViewModel = new CharacterSelecterViewModel(_characterSelecterModel);
            _charcterSelecterWindow = new CharcterSelecter();

            _charcterSelecterWindow.DataContext = _characterSelecterViewModel;
            _characterSelecterViewModel.StartGameEvent += OnStartPokerGame;
            _charcterSelecterWindow.Show();


        }

        private void OnStartPokerGame(object sender, EventArgs e)
        {

            int startingMoney = 0;
            try
            {
                startingMoney = Int32.Parse(_characterSelecterViewModel.StartingMoney);
                _model = new PokerModel(5, startingMoney);
            }
            catch (FormatException)
            {
                _model = new PokerModel(5);
            }

            _viewModel = new PokerViewModel(_model, _characterSelecterViewModel.SelectedCharacter);
            _mainWindow = new MainWindow();

            _model.NewGameEvent += OnNewGameEvent;
            _model.CloseGameEvent += OnCloseGameEvent;

            _mainWindow.MiddleTopCharacterGrid.DataContext = _viewModel.MiddleTopCharacter;
            _mainWindow.LeftTopCharacterGrid.DataContext = _viewModel.LeftTopCharacter;
            _mainWindow.RightTopCharacterGrid.DataContext = _viewModel.RightTopCharacter;
            _mainWindow.RightBottomCharacterGrid.DataContext = _viewModel.RightBottomCharacter;
            _mainWindow.MainPlayerGrid.DataContext = _viewModel.MainPlayer;
            _mainWindow.LeftBottomCharacterGrid.DataContext = _viewModel.LeftBottomCharacter;
            _mainWindow.MiddleSectionGrid.DataContext = _viewModel.MiddleSection;
            _mainWindow.StatusCards.DataContext = _viewModel.StatusCards;
            _mainWindow.StatusBar.DataContext = _viewModel;
            _mainWindow.DataContext = _viewModel;



            _charcterSelecterWindow.Close();
            //_mainWindow.Show();
            _viewModel.InitCharacterEventRaise();
            //_model.GameOn();


            _gameOverViewModel = new GameOverViewModel(true, CharacterTypes.BOB, _model);
            _gameOverWindow = new GameOverWindow();
            _gameOverWindow.DataContext = _gameOverViewModel;
            _gameOverWindow.Show();
        }

        private void OnCloseGameEvent(Object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OnNewGameEvent(Object sender, EventArgs e)
        {
            _gameOverWindow.Close();
            _charcterSelecterWindow = new CharcterSelecter();
            _charcterSelecterWindow.DataContext = _characterSelecterViewModel;
            _characterSelecterViewModel.StartGameEvent += OnStartPokerGame;
            _charcterSelecterWindow.Show();
        }
    }
}
