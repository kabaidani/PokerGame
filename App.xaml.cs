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

        private Brush _deffaultBackground = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

        private List<Action<Player, int>> functionList;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }



        private void OnShowCardsEvent(object sender,PokerPlayerEventArgs e) // We could narrow down the eventArg reference
        {
            _model.TestUnFoldMiddleCards();
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

            _mainWindow.LeftTopCharacterLastActionTextBox.Text = p.LastAction.ToString();
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

            _mainWindow.MiddleTopCharacterLastActionTextBox.Text = p.LastAction.ToString();
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
            _mainWindow.RightTopCharacterLastActionTextBox.Text = p.LastAction.ToString();
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
            _mainWindow.RightBottomCharacterLastActionTextBox.Text = p.LastAction.ToString();
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

            _mainWindow.LeftBottomCharacterLastActionTextBox.Text = p.LastAction.ToString();
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

            _mainWindow.MainPlayerLastActionTextBox.Text = p.LastAction.ToString();
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
            _mainWindow.MiddleFirstCardPicture.Visibility = Visibility.Hidden;
            _mainWindow.MiddleSecondCardPicture.Visibility = Visibility.Hidden;
            _mainWindow.MiddleThirdCardPicture.Visibility = Visibility.Hidden;
            _mainWindow.MiddleFourthCardPicture.Visibility = Visibility.Hidden;
            _mainWindow.MiddleFifthCardPicture.Visibility = Visibility.Hidden;
            _mainWindow.AllBetPicture.Visibility = Visibility.Hidden;
            _mainWindow.AllBetValueTextBox.Visibility = Visibility.Hidden;
            //MainPlayer
            _mainWindow.MainPlayerGrid.Visibility = Visibility.Hidden;


            functionList = new List<Action<Player, int>>();

            //functionList.Add((p, i) => InitMiddleTopCharacter(p, i)); // 0
            //functionList.Add((p, i) => InitLeftTopCharacter(p, i)); // 1
            //functionList.Add((p, i) => InitRightTopCharacter(p, i)); // 2
            //functionList.Add((p, i) => InitRightBottomCharacter(p, i)); // 3
            //functionList.Add((p, i) => InitLeftBottomCharacter(p, i)); // 4
            //functionList.Add((p, i) => InitMainPlayer(p, i)); // 5

            functionList.Add((p, i) => InitLeftTopCharacter(p, i)); // 0
            functionList.Add((p, i) => InitMiddleTopCharacter(p, i)); // 1
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

            int delayTime = 200;
            GetCard(0, delayTime, e.Players, 0);
            await Task.Delay(delayTime);
            GetCard(1, delayTime, e.Players, 0);
            await Task.Delay(delayTime);
            GetCard(2, delayTime, e.Players, 0);
            await Task.Delay(delayTime);
            GetCard(3, delayTime, e.Players, 0);
            await Task.Delay(delayTime);
            GetCard(5, delayTime, e.Players, 0);
            await Task.Delay(delayTime);
            GetCard(4, delayTime, e.Players, 0);
            await Task.Delay(delayTime);
            GetCard(0, delayTime, e.Players);
            await Task.Delay(delayTime);
            GetCard(1, delayTime, e.Players);
            await Task.Delay(delayTime);
            GetCard(2, delayTime, e.Players);
            await Task.Delay(delayTime);
            GetCard(3, delayTime, e.Players);
            await Task.Delay(delayTime);
            GetCard(5, delayTime, e.Players);
            await Task.Delay(delayTime);
            GetCard(4, delayTime, e.Players);

        }

        private string ComminityCardUrlGenerator(Card card)
        {
            string rank = ((int)card.cardType.cardRank).ToString();
            string suit = card.cardType.cardSuit.ToString();
            return "../../Image/Deck/" + rank + suit + ".png";
        }

        private void OnUnFoldCardEvent(object sender, CommonityCardsEventArgs e)
        {
            //First
            if (!e.CommonityCards[0].isUpSideDown)
            {
                string url = ComminityCardUrlGenerator(e.CommonityCards[0]);
                _mainWindow.MiddleFirstCardPicture.Visibility = Visibility.Visible;
                _mainWindow.MiddleFirstCardPicture.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            } else
            {
                _mainWindow.MiddleFirstCardPicture.Visibility = Visibility.Hidden;
            }

            //Second
            if (!e.CommonityCards[1].isUpSideDown)
            {
                string url = ComminityCardUrlGenerator(e.CommonityCards[1]);
                _mainWindow.MiddleSecondCardPicture.Visibility = Visibility.Visible;
                _mainWindow.MiddleSecondCardPicture.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            }
            else
            {
                _mainWindow.MiddleSecondCardPicture.Visibility = Visibility.Hidden;
            }

            //Third
            if (!e.CommonityCards[2].isUpSideDown)
            {
                string url = ComminityCardUrlGenerator(e.CommonityCards[2]);
                _mainWindow.MiddleThirdCardPicture.Visibility = Visibility.Visible;
                _mainWindow.MiddleThirdCardPicture.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            }
            else
            {
                _mainWindow.MiddleThirdCardPicture.Visibility = Visibility.Hidden;
            }

            //Fourth
            if (!e.CommonityCards[3].isUpSideDown)
            {
                string url = ComminityCardUrlGenerator(e.CommonityCards[3]);
                _mainWindow.MiddleFourthCardPicture.Visibility = Visibility.Visible;
                _mainWindow.MiddleFourthCardPicture.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            }
            else
            {
                _mainWindow.MiddleFourthCardPicture.Visibility = Visibility.Hidden;
            }

            //Fifth
            if (!e.CommonityCards[4].isUpSideDown)
            {
                string url = ComminityCardUrlGenerator(e.CommonityCards[4]);
                _mainWindow.MiddleFifthCardPicture.Visibility = Visibility.Visible;
                _mainWindow.MiddleFifthCardPicture.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            }
            else
            {
                _mainWindow.MiddleFifthCardPicture.Visibility = Visibility.Hidden;
            }
        }

        private void OnPlayerActionEvent(object sender, ActionEvenArgs e)
        {
            if(e.Player.StaticName == "MainPlayer")
            {
                InitMainPlayer(e.Player, 1);
            }else if (e.Player.StaticName == "Character1")
            {
                InitLeftTopCharacter(e.Player,1);
            }
            else if (e.Player.StaticName == "Character2")
            {
                InitMiddleTopCharacter(e.Player, 1);
            }
            else if (e.Player.StaticName == "Character3")
            {
                InitRightTopCharacter(e.Player, 1);
            }
            else if (e.Player.StaticName == "Character4")
            {
                InitRightBottomCharacter(e.Player, 1);
            }
            else if (e.Player.StaticName == "Character5")
            {
                InitLeftBottomCharacter(e.Player, 1);
            }
        }

        private void SignLeftTopCharacter()
        {
            if (_mainWindow.LeftTopCharacterProfilePictureLabel.Background != Brushes.LightGreen)
            {
                _mainWindow.LeftTopCharacterProfilePictureLabel.Background = Brushes.LightGreen;
            }else
            {
                _mainWindow.LeftTopCharacterProfilePictureLabel.Background = _deffaultBackground;
            }
        }
        private void SignMiddleTopCharacter()
        {
            if (_mainWindow.MiddleTopCharacterProfilePictureLabel.Background != Brushes.LightGreen)
            {
                _mainWindow.MiddleTopCharacterProfilePictureLabel.Background = Brushes.LightGreen;
            }
            else
            {
                _mainWindow.MiddleTopCharacterProfilePictureLabel.Background = _deffaultBackground;
            }
        }
        private void SignRightTopCharacter()
        {
            if (_mainWindow.RightTopCharacterProfilePictureLabel.Background != Brushes.LightGreen)
            {
                _mainWindow.RightTopCharacterProfilePictureLabel.Background = Brushes.LightGreen;
            }
            else
            {
                _mainWindow.RightTopCharacterProfilePictureLabel.Background = _deffaultBackground;
            }
        }
        private void SignRightBottomCharacter()
        {
            if (_mainWindow.RightBottomCharacterProfilePictureLabel.Background != Brushes.LightGreen)
            {
                _mainWindow.RightBottomCharacterProfilePictureLabel.Background = Brushes.LightGreen;
            }
            else
            {
                _mainWindow.RightBottomCharacterProfilePictureLabel.Background = _deffaultBackground;
            }
        }
        private void SignLeftBottomCharacter()
        {
            if (_mainWindow.LeftBottomCharacterProfilePictureLabel.Background != Brushes.LightGreen)
            {
                _mainWindow.LeftBottomCharacterProfilePictureLabel.Background = Brushes.LightGreen;
            }
            else
            {
                _mainWindow.LeftBottomCharacterProfilePictureLabel.Background = _deffaultBackground;
            }
        }

        private void OnSignPlayerEvent(object sender, PokerPlayerEventArgs e)
        {
            if (e.Player.StaticName == "Character1")
            {
                SignLeftTopCharacter();
            }
            if (e.Player.StaticName == "Character2")
            {
                SignMiddleTopCharacter();
            }
            if (e.Player.StaticName == "Character3")
            {
                SignRightTopCharacter();
            }
            if (e.Player.StaticName == "Character4")
            {
                SignRightBottomCharacter();
            }
            if (e.Player.StaticName == "Character5")
            {
                SignLeftBottomCharacter();
            }
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new PokerModel(5);
            _viewModel = new PokerViewModel(_model);
            _mainWindow = new MainWindow();
            _viewModel.ShowCardsEvent += OnShowCardsEvent;
            //_viewModel.InitCharacters += OnInitCharacters;
            _viewModel.UnFoldCardEvent += OnUnFoldCardEvent;
            _viewModel.PlayerActionEvent += OnPlayerActionEvent;
            _viewModel.CardAllocationEvent += AsyncOnCardAllocationEvent;
            _viewModel.SignPlayerEvent += OnSignPlayerEvent;

            _mainWindow.DataContext = _viewModel;

            _mainWindow.MiddleTopCharacterGrid.DataContext = _viewModel.MiddleTopCharacter;
            _mainWindow.LeftTopCharacterGrid.DataContext = _viewModel.LeftTopCharacter;
            _mainWindow.RightTopCharacterGrid.DataContext = _viewModel.RightTopCharacter;
            _mainWindow.RightBottomCharacterGrid.DataContext = _viewModel.RightBottomCharacter;
            _mainWindow.MainPlayerGrid.DataContext = _viewModel.MainPlayer;
            _mainWindow.LeftBottomCharacterGrid.DataContext = _viewModel.LeftBottomCharacter;

            _viewModel.InitCharacterEventRaise();


            _mainWindow.Show();
        }
    }
}
