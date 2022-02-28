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
            _model.AsyncStartRound();
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
            _mainWindow.MiddleTopCharacterGrid.Visibility = Visibility.Visible;
            _mainWindow.MiddleTopCharacterBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.MiddleTopCharacterBetTextBox.Visibility = Visibility.Hidden;
            _mainWindow.MiddleTopCharacterLeftHand.Visibility = Visibility.Hidden;
            _mainWindow.MiddleTopCharacterRightHand.Visibility = Visibility.Hidden;

            _mainWindow.MiddleTopCharacterMoneyTextBox.Text = p.Money.ToString();
            _mainWindow.MiddleTopCharacterMoneyTextBox.Visibility = Visibility.Visible;

            _mainWindow.MiddleTopCharacterNameTextBox.Text = p.Character.ToString();
            _mainWindow.MiddleTopCharacterNameTextBox.Visibility = Visibility.Visible;

            string urlSource = "../../Image/" + p.Character.ToString() + ".png";
            _mainWindow.MiddleTopCharacterProfilePicture.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));


            _mainWindow.MiddleTopCharacterLastActionTextBox.Text = "";
        }

        private void InitRightTopCharacter(Player p)
        {
            _mainWindow.RightTopCharacterGrid.Visibility = Visibility.Visible;
            _mainWindow.RightTopCharacterBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.RightTopCharacterBetTextBox.Visibility = Visibility.Hidden;
            _mainWindow.RightTopCharacterLeftHand.Visibility = Visibility.Hidden;
            _mainWindow.RightTopCharacterRightHand.Visibility = Visibility.Hidden;

            _mainWindow.RightTopCharacterMoneyTextBox.Text = p.Money.ToString();
            _mainWindow.RightTopCharacterMoneyTextBox.Visibility = Visibility.Visible;

            _mainWindow.RightTopCharacterNameTextBox.Text = p.Character.ToString();
            _mainWindow.RightTopCharacterNameTextBox.Visibility = Visibility.Visible;

            string urlSource = "../../Image/" + p.Character.ToString() + ".png";
            _mainWindow.RightTopCharacterProfilePicture.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));


            _mainWindow.RightTopCharacterLastActionTextBox.Text = "";
        }

        private void InitRightBottomCharacter(Player p)
        {
            _mainWindow.RightBottomCharacterGrid.Visibility = Visibility.Visible;
            _mainWindow.RightBottomCharacterBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.RightBottomCharacterBetTextBox.Visibility = Visibility.Hidden;
            _mainWindow.RightBottomCharacterLeftHand.Visibility = Visibility.Hidden;
            _mainWindow.RightBottomCharacterRightHand.Visibility = Visibility.Hidden;

            _mainWindow.RightBottomCharacterMoneyTextBox.Text = p.Money.ToString();
            _mainWindow.RightBottomCharacterMoneyTextBox.Visibility = Visibility.Visible;

            _mainWindow.RightBottomCharacterNameTextBox.Text = p.Character.ToString();
            _mainWindow.RightBottomCharacterNameTextBox.Visibility = Visibility.Visible;

            string urlSource = "../../Image/" + p.Character.ToString() + ".png";
            _mainWindow.RightBottomCharacterProfilePicture.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));


            _mainWindow.RightBottomCharacterLastActionTextBox.Text = "";
        }

        private void InitLeftBottomCharacter(Player p)
        {
            _mainWindow.LeftBottomCharacterGrid.Visibility = Visibility.Visible;
            _mainWindow.LeftBottomCharacterBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.LeftBottomCharacterBetTextBox.Visibility = Visibility.Hidden;
            _mainWindow.LeftBottomCharacterLeftHand.Visibility = Visibility.Hidden;
            _mainWindow.LeftBottomCharacterRightHand.Visibility = Visibility.Hidden;


            _mainWindow.LeftBottomCharacterMoneyTextBox.Text = p.Money.ToString();
            _mainWindow.LeftBottomCharacterMoneyTextBox.Visibility = Visibility.Visible;

            _mainWindow.LeftBottomCharacterNameTextBox.Text = p.Character.ToString();
            _mainWindow.LeftBottomCharacterNameTextBox.Visibility = Visibility.Visible;

            string urlSource = "../../Image/" + p.Character.ToString() + ".png";
            _mainWindow.LeftBottomCharacterProfilePicture.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));


            _mainWindow.LeftBottomCharacterLastActionTextBox.Text = "";
        }

        private void InitMainPlayer(Player p)
        {
            _mainWindow.MainPlayerBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.MainPlayerBetTextBox.Visibility = Visibility.Hidden;
            _mainWindow.MainPlayerLeftHand.Visibility = Visibility.Hidden;
            _mainWindow.MainPlayerRightHand.Visibility = Visibility.Hidden;

            _mainWindow.MainPlayerMoneyTextBox.Text = p.Money.ToString();
            _mainWindow.MainPlayerMoneyTextBox.Visibility = Visibility.Visible;

            _mainWindow.MainPlayerNameTextBox.Text = p.Character.ToString();
            _mainWindow.MainPlayerNameTextBox.Visibility = Visibility.Visible;

            string urlSource = "../../Image/" + p.Character.ToString() + ".png";
            _mainWindow.MainPlayerProfilePicture.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));


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

            List<Action<Player>> functionList = new List<Action<Player>>();

            functionList.Add(p => InitMiddleTopCharacter(p));
            functionList.Add(p => InitLeftTopCharacter(p));
            functionList.Add(p => InitRightTopCharacter(p));
            functionList.Add(p => InitRightBottomCharacter(p));
            functionList.Add(p => InitLeftBottomCharacter(p));
            functionList.Add(p => InitMainPlayer(p));

            for (int i = 0; i<e.Players.Count-1; i++)
            {
                functionList[i].Invoke(e.Players[i]);
            }
            functionList[functionList.Count-1].Invoke(e.Players[ e.Players.Count-1 ]); // Init main player

        }

        private void OnCardAllocationEvent(object sender, EventArgs e)
        {
            _mainWindow.LeftTopCharacterLeftHand.Visibility = Visibility.Visible;
            _mainWindow.LeftTopCharacterRightHand.Visibility = Visibility.Visible;

            _mainWindow.LeftBottomCharacterLeftHand.Visibility = Visibility.Visible;
            _mainWindow.LeftBottomCharacterRightHand.Visibility = Visibility.Visible;

            _mainWindow.RightTopCharacterLeftHand.Visibility = Visibility.Visible;
            _mainWindow.RightTopCharacterRightHand.Visibility = Visibility.Visible;

            _mainWindow.RightBottomCharacterLeftHand.Visibility = Visibility.Visible;
            _mainWindow.RightBottomCharacterRightHand.Visibility = Visibility.Visible;


            _mainWindow.MainPlayerLeftHand.Visibility = Visibility.Visible;
            _mainWindow.MainPlayerLeftHand.Visibility = Visibility.Visible;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new PokerModel(5);
            _viewModel = new PokerViewModel(_model);
            _mainWindow = new MainWindow();
            _viewModel.ShowCardsEvent += OnShowCardsEvent;
            _viewModel.InitCharacters += OnInitCharacters;
            _viewModel.CardAllocationEvent += OnCardAllocationEvent;
            _mainWindow.DataContext = _viewModel;
            _viewModel.InitCharacterEventRaise();


            _mainWindow.Show();
        }
    }
}
