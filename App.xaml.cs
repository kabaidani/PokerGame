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
            //string urlSource = "";
            //if (e.Player.Hand.Item1.isUpSideDown)
            //{
            //    urlSource = "../../Image/cardBack.jpg";
            //} else
            //{
            //    string rank = ((int)e.Player.Hand.Item1.cardType.cardRank).ToString();
            //    string tmp = e.Player.Hand.Item2.cardType.cardSuit.ToString();
            //    urlSource = "../../Image/Deck/" + rank + tmp + ".png";
            //}
            //_mainWindow.TestImageName.Visibility = Visibility.Visible;
            //_mainWindow.TestImageName.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));
        }

        private void InitLeftTopCharacter(Player p)
        {
            _mainWindow.LeftTopCharacterGrid.Visibility = Visibility.Visible;
            _mainWindow.LeftTopCharacterBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterBetTextBox.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterLeftHand.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterRightHand.Visibility = Visibility.Hidden;

            _mainWindow.LeftTopCharacterMoneyTextBox.Text = p.Money.ToString();
            _mainWindow.LeftTopCharacterMoneyTextBox.Visibility = Visibility.Visible;

            _mainWindow.LeftTopCharacterNameTextBox.Text = p.Character.ToString();
            _mainWindow.LeftTopCharacterNameTextBox.Visibility = Visibility.Visible;

            string urlSource = "../../Image/" + p.Character.ToString() + ".png";
            _mainWindow.LeftTopCharacterProfilePicture.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));

            _mainWindow.LeftTopCharacterLastActionTextBox.Text = "";
        }

        private void InitMiddleTopCharacter(Player p)
        {
            _mainWindow.LeftTopCharacterGrid.Visibility = Visibility.Visible;
            _mainWindow.LeftTopCharacterBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterBetTextBox.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterLeftHand.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterRightHand.Visibility = Visibility.Hidden;

            _mainWindow.LeftTopCharacterMoneyTextBox.Text = p.Money.ToString();
            _mainWindow.LeftTopCharacterMoneyTextBox.Visibility = Visibility.Visible;

            _mainWindow.LeftTopCharacterNameTextBox.Text = p.Character.ToString();
            _mainWindow.LeftTopCharacterNameTextBox.Visibility = Visibility.Visible;

            _mainWindow.LeftTopCharacterLastActionTextBox.Text = "";
        }

        private void InitRightTopCharacter(Player p)
        {
            _mainWindow.LeftTopCharacterGrid.Visibility = Visibility.Visible;
            _mainWindow.LeftTopCharacterBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterBetTextBox.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterLeftHand.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterRightHand.Visibility = Visibility.Hidden;

            _mainWindow.LeftTopCharacterMoneyTextBox.Text = p.Money.ToString();
            _mainWindow.LeftTopCharacterMoneyTextBox.Visibility = Visibility.Visible;

            _mainWindow.LeftTopCharacterNameTextBox.Text = p.Character.ToString();
            _mainWindow.LeftTopCharacterNameTextBox.Visibility = Visibility.Visible;

            _mainWindow.LeftTopCharacterLastActionTextBox.Text = "";
        }

        private void InitRightBottomCharacter(Player p)
        {
            _mainWindow.LeftTopCharacterGrid.Visibility = Visibility.Visible;
            _mainWindow.LeftTopCharacterBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterBetTextBox.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterLeftHand.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterRightHand.Visibility = Visibility.Hidden;

            _mainWindow.LeftTopCharacterMoneyTextBox.Text = p.Money.ToString();
            _mainWindow.LeftTopCharacterMoneyTextBox.Visibility = Visibility.Visible;

            _mainWindow.LeftTopCharacterNameTextBox.Text = p.Character.ToString();
            _mainWindow.LeftTopCharacterNameTextBox.Visibility = Visibility.Visible;

            _mainWindow.LeftTopCharacterLastActionTextBox.Text = "";
        }

        private void InitLeftBottomCharacter(Player p)
        {
            _mainWindow.LeftTopCharacterGrid.Visibility = Visibility.Visible;
            _mainWindow.LeftTopCharacterBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterBetTextBox.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterLeftHand.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterRightHand.Visibility = Visibility.Hidden;

            _mainWindow.LeftTopCharacterMoneyTextBox.Text = p.Money.ToString();
            _mainWindow.LeftTopCharacterMoneyTextBox.Visibility = Visibility.Visible;

            _mainWindow.LeftTopCharacterNameTextBox.Text = p.Character.ToString();
            _mainWindow.LeftTopCharacterNameTextBox.Visibility = Visibility.Visible;

            _mainWindow.LeftTopCharacterLastActionTextBox.Text = "";
        }

        private void InitMainPlayer(Player p)
        {
            _mainWindow.LeftTopCharacterGrid.Visibility = Visibility.Visible;
            _mainWindow.LeftTopCharacterBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterBetTextBox.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterLeftHand.Visibility = Visibility.Hidden;
            _mainWindow.LeftTopCharacterRightHand.Visibility = Visibility.Hidden;

            _mainWindow.LeftTopCharacterMoneyTextBox.Text = p.Money.ToString();
            _mainWindow.LeftTopCharacterMoneyTextBox.Visibility = Visibility.Visible;

            _mainWindow.LeftTopCharacterNameTextBox.Text = p.Character.ToString();
            _mainWindow.LeftTopCharacterNameTextBox.Visibility = Visibility.Visible;

            _mainWindow.LeftTopCharacterLastActionTextBox.Text = "";
        }

        private void OnInitCharacters(object sender, PlayersEventArg e)
        {
            //Charcater1
            _mainWindow.LeftTopCharacterGrid.Visibility = Visibility.Hidden;
            //Character2
            _mainWindow.MiddleTopCharacterGrid.Visibility = Visibility.Hidden;
            //Charcater3
            _mainWindow.RightTopCharacterGrid.Visibility = Visibility.Hidden;
            //Character4
            _mainWindow.RightBottomCharacterGrid.Visibility = Visibility.Hidden;
            //Character5
            _mainWindow.LeftBottomCharacterGrid.Visibility = Visibility.Hidden;
            //MiddleSection
            _mainWindow.MiddleSectionGrid.Visibility = Visibility.Hidden;

            //MainPlayer
            _mainWindow.MainPlayerLeftHand.Visibility = Visibility.Hidden;
            _mainWindow.MainPlayerRightHand.Visibility = Visibility.Hidden;
            _mainWindow.MainPlayerBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.MainPlayerBetTextBox.Visibility = Visibility.Hidden;

            InitLeftTopCharacter(e.Players[0]);

        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new PokerModel();
            _viewModel = new PokerViewModel(_model);
            _mainWindow = new MainWindow();
            _viewModel.ShowCardsEvent += OnShowCardsEvent;
            _viewModel.InitCharacters += OnInitCharacters;
            _mainWindow.DataContext = _viewModel;
            _viewModel.InitCharacterEventRaise();


            _mainWindow.Show();
        }
    }
}
