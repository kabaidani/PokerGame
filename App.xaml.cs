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

        private List<Action<Player, int>> functionList;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }



        private void OnShowCardsEvent(object sender, PokerPlayerEventArgs e) // We could narrow down the eventArg reference
        {
            _model.StartRound();
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

        private string LeftHandCardUrlGenerator(Player p)
        {
            if (p.hand.leftHand.isUpSideDown)
            {
                return "../../Image/cardBack.png";
            }
            else
            {
                string rank = ((int)p.hand.leftHand.cardType.cardRank).ToString();
                string suit = p.hand.leftHand.cardType.cardSuit.ToString();
                return "../../Image/Deck/" + rank + suit + ".png";
            }
        }
        
        private string RightHandCardUrlGenerator(Player p)
        {
            if (p.hand.rightHand.isUpSideDown)
            {
                return "../../Image/cardBack.png";
            }
            else
            {
                string rank = ((int)p.hand.rightHand.cardType.cardRank).ToString();
                string suit = p.hand.rightHand.cardType.cardSuit.ToString();
                return "../../Image/Deck/" + rank + suit + ".png";
            }
        }


        private void InitLeftTopCharacter(Player p, int whichHand) //need to chane whichHand into boolean
        {
            if (p.InGame)
            {
                _mainWindow.LeftTopCharacterGrid.Visibility = Visibility.Visible;

            }
            else
            {
                _mainWindow.LeftTopCharacterGrid.Visibility = Visibility.Hidden;
                return;
            }

            if (p.BetChips != 0)
            {
                _mainWindow.LeftTopCharacterBetPicture.Visibility = Visibility.Visible;
                _mainWindow.LeftTopCharacterBetTextBox.Text = p.BetChips.ToString() + " €";
            }else
            {
                _mainWindow.LeftTopCharacterBetPicture.Visibility = Visibility.Hidden;
                _mainWindow.LeftTopCharacterBetTextBox.Text = "";
            }

            if(p.hand.leftHand.cardType.cardRank == CardRank.NOCARD) // Left Hand
            {
                _mainWindow.LeftTopCharacterLeftHand.Visibility = Visibility.Hidden;
            }else
            {
                _mainWindow.LeftTopCharacterLeftHand.Visibility = Visibility.Visible;
                string leftHandUrl = LeftHandCardUrlGenerator(p);
                _mainWindow.LeftTopCharacterLeftHand.Source = new BitmapImage(new Uri(leftHandUrl, UriKind.Relative));
            }

            if (p.hand.rightHand.cardType.cardRank == CardRank.NOCARD || whichHand == 0) // Right Hand
            {
                _mainWindow.LeftTopCharacterRightHand.Visibility = Visibility.Hidden;
            }
            else
            {
                _mainWindow.LeftTopCharacterRightHand.Visibility = Visibility.Visible;
                string rightHandUrl = RightHandCardUrlGenerator(p);
                _mainWindow.LeftTopCharacterRightHand.Source = new BitmapImage(new Uri(rightHandUrl, UriKind.Relative));
            }

            _mainWindow.LeftTopCharacterMoneyTextBox.Text = p.Money.ToString() + " €";


            _mainWindow.LeftTopCharacterNameTextBox.Text = p.Character.ToString();

            string urlSource = "../../Image/" + p.Character.ToString() + ".png";
            _mainWindow.LeftTopCharacterProfilePicture.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));

            _mainWindow.LeftTopCharacterLastActionTextBox.Text = "";
        }

        private void InitMiddleTopCharacter(Player p, int whichHand)
        {
            if (p.InGame)
            {
                _mainWindow.MiddleTopCharacterGrid.Visibility = Visibility.Visible;

            }
            else
            {
                _mainWindow.MiddleTopCharacterGrid.Visibility = Visibility.Hidden;
                return;
            }

            if (p.BetChips != 0)
            {
                _mainWindow.MiddleTopCharacterBetPicture.Visibility = Visibility.Visible;
                _mainWindow.MiddleTopCharacterBetTextBox.Text = p.BetChips.ToString() + " €";
            }
            else
            {
                _mainWindow.MiddleTopCharacterBetPicture.Visibility = Visibility.Hidden;
                _mainWindow.MiddleTopCharacterBetTextBox.Text = "";
            }

            if (p.hand.leftHand.cardType.cardRank == CardRank.NOCARD) // Left Hand
            {
                _mainWindow.MiddleTopCharacterLeftHand.Visibility = Visibility.Hidden;
            }
            else
            {
                _mainWindow.MiddleTopCharacterLeftHand.Visibility = Visibility.Visible;
                string leftHandUrl = LeftHandCardUrlGenerator(p);
                _mainWindow.MiddleTopCharacterLeftHand.Source = new BitmapImage(new Uri(leftHandUrl, UriKind.Relative));
            }

            if (p.hand.rightHand.cardType.cardRank == CardRank.NOCARD || whichHand == 0) // Right Hand
            {
                _mainWindow.MiddleTopCharacterRightHand.Visibility = Visibility.Hidden;
            }
            else
            {
                _mainWindow.MiddleTopCharacterRightHand.Visibility = Visibility.Visible;
                string rightHandUrl = RightHandCardUrlGenerator(p);
                _mainWindow.MiddleTopCharacterRightHand.Source = new BitmapImage(new Uri(rightHandUrl, UriKind.Relative));
            }

            _mainWindow.MiddleTopCharacterMoneyTextBox.Text = p.Money.ToString() + " €";


            _mainWindow.MiddleTopCharacterNameTextBox.Text = p.Character.ToString();

            string urlSource = "../../Image/" + p.Character.ToString() + ".png";
            _mainWindow.MiddleTopCharacterProfilePicture.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));

            _mainWindow.MiddleTopCharacterLastActionTextBox.Text = "";
        }

        private void InitRightTopCharacter(Player p, int whichHand)
        {
            if (p.InGame)
            {
                _mainWindow.RightTopCharacterGrid.Visibility = Visibility.Visible;

            }
            else
            {
                _mainWindow.RightTopCharacterGrid.Visibility = Visibility.Hidden;
                return;
            }

            if (p.BetChips != 0)
            {
                _mainWindow.RightTopCharacterBetPicture.Visibility = Visibility.Visible;
                _mainWindow.RightTopCharacterBetTextBox.Text = p.BetChips.ToString() + " €";
            }
            else
            {
                _mainWindow.RightTopCharacterBetPicture.Visibility = Visibility.Hidden;
                _mainWindow.RightTopCharacterBetTextBox.Text = "";
            }

            if (p.hand.leftHand.cardType.cardRank == CardRank.NOCARD) // Left Hand
            {
                _mainWindow.RightTopCharacterLeftHand.Visibility = Visibility.Hidden;
            }
            else
            {
                _mainWindow.RightTopCharacterLeftHand.Visibility = Visibility.Visible;
                string leftHandUrl = LeftHandCardUrlGenerator(p);
                _mainWindow.RightTopCharacterLeftHand.Source = new BitmapImage(new Uri(leftHandUrl, UriKind.Relative));
            }

            if (p.hand.rightHand.cardType.cardRank == CardRank.NOCARD || whichHand == 0) // Right Hand
            {
                _mainWindow.RightTopCharacterRightHand.Visibility = Visibility.Hidden;
            }
            else
            {
                _mainWindow.RightTopCharacterRightHand.Visibility = Visibility.Visible;
                string rightHandUrl = RightHandCardUrlGenerator(p);
                _mainWindow.RightTopCharacterRightHand.Source = new BitmapImage(new Uri(rightHandUrl, UriKind.Relative));
            }

            _mainWindow.RightTopCharacterMoneyTextBox.Text = p.Money.ToString() + " €";


            _mainWindow.RightTopCharacterNameTextBox.Text = p.Character.ToString();

            string urlSource = "../../Image/" + p.Character.ToString() + ".png";
            _mainWindow.RightTopCharacterProfilePicture.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));

            _mainWindow.RightTopCharacterLastActionTextBox.Text = "";
        }

        private void InitRightBottomCharacter(Player p, int whichHand)
        {
            if (p.InGame)
            {
                _mainWindow.RightBottomCharacterGrid.Visibility = Visibility.Visible;

            }
            else
            {
                _mainWindow.RightBottomCharacterGrid.Visibility = Visibility.Hidden;
                return;
            }

            if (p.BetChips != 0)
            {
                _mainWindow.RightBottomCharacterBetPicture.Visibility = Visibility.Visible;
                _mainWindow.RightBottomCharacterBetTextBox.Text = p.BetChips.ToString() + " €";
            }
            else
            {
                _mainWindow.RightBottomCharacterBetPicture.Visibility = Visibility.Hidden;
                _mainWindow.RightBottomCharacterBetTextBox.Text = "";
            }

            if (p.hand.leftHand.cardType.cardRank == CardRank.NOCARD) // Left Hand
            {
                _mainWindow.RightBottomCharacterLeftHand.Visibility = Visibility.Hidden;
            }
            else
            {
                _mainWindow.RightBottomCharacterLeftHand.Visibility = Visibility.Visible;
                string leftHandUrl = LeftHandCardUrlGenerator(p);
                _mainWindow.RightBottomCharacterLeftHand.Source = new BitmapImage(new Uri(leftHandUrl, UriKind.Relative));
            }

            if (p.hand.rightHand.cardType.cardRank == CardRank.NOCARD || whichHand == 0) // Right Hand
            {
                _mainWindow.RightBottomCharacterRightHand.Visibility = Visibility.Hidden;
            }
            else
            {
                _mainWindow.RightBottomCharacterRightHand.Visibility = Visibility.Visible;
                string rightHandUrl = RightHandCardUrlGenerator(p);
                _mainWindow.RightBottomCharacterRightHand.Source = new BitmapImage(new Uri(rightHandUrl, UriKind.Relative));
            }

            _mainWindow.RightBottomCharacterMoneyTextBox.Text = p.Money.ToString() + " €";


            _mainWindow.RightBottomCharacterNameTextBox.Text = p.Character.ToString();

            string urlSource = "../../Image/" + p.Character.ToString() + ".png";
            _mainWindow.RightBottomCharacterProfilePicture.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));

            _mainWindow.RightBottomCharacterLastActionTextBox.Text = "";
        }

        private void InitLeftBottomCharacter(Player p, int whichHand)
        {
            if (p.InGame)
            {
                _mainWindow.LeftBottomCharacterGrid.Visibility = Visibility.Visible;

            }
            else
            {
                _mainWindow.LeftBottomCharacterGrid.Visibility = Visibility.Hidden;
                return;
            }

            if (p.BetChips != 0)
            {
                _mainWindow.LeftBottomCharacterBetPicture.Visibility = Visibility.Visible;
                _mainWindow.LeftBottomCharacterBetTextBox.Text = p.BetChips.ToString() + " €";
            }
            else
            {
                _mainWindow.LeftBottomCharacterBetPicture.Visibility = Visibility.Hidden;
                _mainWindow.LeftBottomCharacterBetTextBox.Text = "";
            }

            if (p.hand.leftHand.cardType.cardRank == CardRank.NOCARD) // Left Hand
            {
                _mainWindow.LeftBottomCharacterLeftHand.Visibility = Visibility.Hidden;
            }
            else
            {
                _mainWindow.LeftBottomCharacterLeftHand.Visibility = Visibility.Visible;
                string leftHandUrl = LeftHandCardUrlGenerator(p);
                _mainWindow.LeftBottomCharacterLeftHand.Source = new BitmapImage(new Uri(leftHandUrl, UriKind.Relative));
            }

            if (p.hand.rightHand.cardType.cardRank == CardRank.NOCARD || whichHand == 0) // Right Hand
            {
                _mainWindow.LeftBottomCharacterRightHand.Visibility = Visibility.Hidden;
            }
            else
            {
                _mainWindow.LeftBottomCharacterRightHand.Visibility = Visibility.Visible;
                string rightHandUrl = RightHandCardUrlGenerator(p);
                _mainWindow.LeftBottomCharacterRightHand.Source = new BitmapImage(new Uri(rightHandUrl, UriKind.Relative));
            }

            _mainWindow.LeftBottomCharacterMoneyTextBox.Text = p.Money.ToString() + " €";


            _mainWindow.LeftBottomCharacterNameTextBox.Text = p.Character.ToString();

            string urlSource = "../../Image/" + p.Character.ToString() + ".png";
            _mainWindow.LeftBottomCharacterProfilePicture.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));

            _mainWindow.LeftBottomCharacterLastActionTextBox.Text = "";
        }

        private void InitMainPlayer(Player p, int whichHand)
        {
            if (p.InGame)
            {
                _mainWindow.MainPlayerGrid.Visibility = Visibility.Visible;

            }
            else
            {
                _mainWindow.MainPlayerGrid.Visibility = Visibility.Hidden;
                return;
            }

            if (p.BetChips != 0)
            {
                _mainWindow.MainPlayerBetPicture.Visibility = Visibility.Visible;
                _mainWindow.MainPlayerBetTextBox.Text = p.BetChips.ToString() + " €";
            }
            else
            {
                _mainWindow.MainPlayerBetPicture.Visibility = Visibility.Hidden;
                _mainWindow.MainPlayerBetTextBox.Text = "";
            }

            if (p.hand.leftHand.cardType.cardRank == CardRank.NOCARD) // Left Hand
            {
                _mainWindow.MainPlayerLeftHand.Visibility = Visibility.Hidden;
            }
            else
            {
                _mainWindow.MainPlayerLeftHand.Visibility = Visibility.Visible;
                string leftHandUrl = LeftHandCardUrlGenerator(p);
                _mainWindow.MainPlayerLeftHand.Source = new BitmapImage(new Uri(leftHandUrl, UriKind.Relative));
            }

            if (p.hand.rightHand.cardType.cardRank == CardRank.NOCARD || whichHand == 0) // Right Hand
            {
                _mainWindow.MainPlayerRightHand.Visibility = Visibility.Hidden;
            }
            else
            {
                _mainWindow.MainPlayerRightHand.Visibility = Visibility.Visible;
                string rightHandUrl = RightHandCardUrlGenerator(p);
                _mainWindow.MainPlayerRightHand.Source = new BitmapImage(new Uri(rightHandUrl, UriKind.Relative));
            }

            _mainWindow.MainPlayerMoneyTextBox.Text = p.Money.ToString() + " €";


            _mainWindow.MainPlayerNameTextBox.Text = p.Character.ToString();

            string urlSource = "../../Image/" + p.Character.ToString() + ".png";
            _mainWindow.MainPlayerProfilePicture.Source = new BitmapImage(new Uri(urlSource, UriKind.Relative));

            _mainWindow.MainPlayerLastActionTextBox.Text = "";
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
            _mainWindow.MainPlayerGrid.Visibility = Visibility.Hidden;


            functionList = new List<Action<Player, int>>();

            functionList.Add((p, i) => InitMiddleTopCharacter(p, i)); // 0
            functionList.Add((p, i) => InitLeftTopCharacter(p, i)); // 1
            functionList.Add((p, i) => InitRightTopCharacter(p, i)); // 2
            functionList.Add((p, i) => InitRightBottomCharacter(p, i)); // 3
            functionList.Add((p, i) => InitLeftBottomCharacter(p, i)); // 4
            functionList.Add((p, i) => InitMainPlayer(p, i)); // 5

            for (int i = 0; i<e.Players.Count-1; i++)
            {
                functionList[i].Invoke(e.Players[i], 1);
            }
            functionList[functionList.Count-1].Invoke(e.Players[ e.Players.Count-1 ], 1); // Init main player

        }

        private void GetCard(int playerNumber, int delayTime, List<Player> e, int time = 1)
        {
            if (e.Count < playerNumber) return;
            functionList[playerNumber].Invoke(e[playerNumber], time);
        }

        private async void AsyncOnCardAllocationEvent(object sender, PlayersEventArg e)
        {

            int delayTime = 350;
            GetCard(1, delayTime, e.Players, 0);
            await Task.Delay(delayTime);
            GetCard(0, delayTime, e.Players, 0);
            await Task.Delay(delayTime);
            GetCard(2, delayTime, e.Players, 0);
            await Task.Delay(delayTime);
            GetCard(3, delayTime, e.Players, 0);
            await Task.Delay(delayTime);
            GetCard(5, delayTime, e.Players, 0);
            await Task.Delay(delayTime);
            GetCard(4, delayTime, e.Players, 0);
            await Task.Delay(delayTime);
            GetCard(1, delayTime, e.Players);
            await Task.Delay(delayTime);
            GetCard(0, delayTime, e.Players);
            await Task.Delay(delayTime);
            GetCard(2, delayTime, e.Players);
            await Task.Delay(delayTime);
            GetCard(3, delayTime, e.Players);
            await Task.Delay(delayTime);
            GetCard(5, delayTime, e.Players);
            await Task.Delay(delayTime);
            GetCard(4, delayTime, e.Players);

        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new PokerModel(5);
            _viewModel = new PokerViewModel(_model);
            _mainWindow = new MainWindow();
            _viewModel.ShowCardsEvent += OnShowCardsEvent;
            _viewModel.InitCharacters += OnInitCharacters;
            _viewModel.CardAllocationEvent += AsyncOnCardAllocationEvent;
            _mainWindow.DataContext = _viewModel;
            _viewModel.InitCharacterEventRaise();


            _mainWindow.Show();
        }
    }
}
