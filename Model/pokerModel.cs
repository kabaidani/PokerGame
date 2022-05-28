using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading;
using System.Linq;

namespace PokerGame.Model
{
    public class CharacterSelecterModel
    {
        public int GridSize { get; private set; }
        public CharacterSelecterModel()
        {
            GridSize = 4;
        }
    }

    public class PokerModel
    {
        public event EventHandler<PokerPlayerEventArgs> CardAllocation; //Good
        public event EventHandler UpdateMiddleSectionEvent; //Good
        public event EventHandler<PokerPlayerEventArgs> PlayerActionEvent; //Good
        public event EventHandler<PokerPlayerEventArgs> SignPlayerEvent; // Good
        public event EventHandler<PlayersEventArg> RoundOverForPlayersEvent; //Good
        public event EventHandler<PokerPlayerEventArgs> OutOfGamePlayerEvent;
        //public event EventHandler<PlayersEventArg> AveragePlayersUpdateEvent; //Deprecated
        public event EventHandler RefreshRemainTime;
        public event EventHandler DealerChipPass;
        public event EventHandler MainPlayerTurnEvent;
        public event EventHandler<CommonityCardsEventArgs> CheckCombinationEvent; //need to change the name of the template parameter
        public event EventHandler RefreshPlayers;
        public event EventHandler<PlayersEventArg> RefreshGivenPlayers;


        public bool MainplayerTurn { get; private set; }
        public int StartingMoney { private get; set; } // Not sure if it is okey
        public int PlayersNum { private get; set; }
        public List<Player> playerContainer;
        public List<Player> playersOutOfTheGame;
        public MainPlayer mainPlayer;
        public MiddleField MiddleFieldSection { get; private set; } // need to be private but needs to handle the events
        public StatusCards StatusCards { get; private set; }

        private enum Role { DEALER, SMALLBLIND, BIGBLIND}

        //private int _dealer;
        //private int _smallBlind;
        //private int _bigBlind;

        private int _blindValue = 100;
        public int BlindValue { get { return _blindValue; } }

        private int _actualLicitBet;
        private bool _end; // should give an intuitive name for this

        public Tuple<PokerHandRanks, List<Card>> _mainPlayerHandRank;

        public int RemaineTime { private set; get; }
        public int getActualLicitBet() { return _actualLicitBet; }
        private Deck _deck;

        public PokerModel(int playersNumber, int startingMoney = 2000)
        {
            if (playersNumber > 5 || playersNumber < 1) throw new ArgumentException();
            PlayersNum = playersNumber;
            StartingMoney = startingMoney;
            _deck = new Deck();
            StatusCards = new StatusCards();
            playersOutOfTheGame = new List<Player>();
        }


        private void OnRefreshGivenPlayers(List<Player> refreshedPlayers)
        {
            if(RefreshGivenPlayers != null)
            {
                RefreshGivenPlayers(this, new PlayersEventArg(refreshedPlayers));
            }
        }

        private void OnOutOfGamePlayerEvent(Player p)
        {
            if(OutOfGamePlayerEvent != null)
            {
                OutOfGamePlayerEvent(this, new PokerPlayerEventArgs(p));
            }
        }

        private void OnRefreshPlayers()
        {
            if(RefreshPlayers != null)
            {
                RefreshPlayers(this, EventArgs.Empty);
            }
        }

        private void OnMainPlayerTurn(bool mainPlayerTurn)
        {
            MainplayerTurn = mainPlayerTurn;
            if(MainPlayerTurnEvent != null)
            {
                MainPlayerTurnEvent(this, EventArgs.Empty);
            }
        }

        private void OnCheckCombinationEvent()
        {
            if(CheckCombinationEvent != null)
            {
                List<Card> cards = new List<Card>();
                cards.Add(mainPlayer.hand.leftHand);
                cards.Add(mainPlayer.hand.rightHand);
                cards.AddRange(MiddleFieldSection.CommonityCards);
                List<Card> res = new List<Card>();
                StatusCards.checkPokerCombination(cards, ref res);

                CheckCombinationEvent(this, new CommonityCardsEventArgs(res));
            }
        }

        private void OnDealerChipRound()
        {
            if(DealerChipPass != null)
            {
                DealerChipPass(this, EventArgs.Empty);
            }
        }

        private void OnRefreshRemainTime()
        {
            if(RefreshRemainTime != null)
            {
                RefreshRemainTime(this, EventArgs.Empty);
            }
        }

        private void OnRoundOverForPlayersEvent(List<Player> players)
        {
            if(RoundOverForPlayersEvent != null)
            {
                RoundOverForPlayersEvent(this, new PlayersEventArg(players));
            }
        }

        private void OnSignPlayerEvent(Player player)
        {
            if(SignPlayerEvent != null)
            {
                SignPlayerEvent(this, new PokerPlayerEventArgs(player));
            }
        }

        private void OnPlayerActionEvent(Player player)
        {
            if( PlayerActionEvent != null)
            {
                PlayerActionEvent(this, new PokerPlayerEventArgs(player));
            }
        }

        private void OnUpdateMiddleSectionEvent() //We don't need to pass th list of the cards
        {
            if(UpdateMiddleSectionEvent != null)
            {
                UpdateMiddleSectionEvent(this, EventArgs.Empty);
            }
        }

        private void OnCardAllocation(Player player)
        {
            if (CardAllocation != null)
            {
                CardAllocation(this, new PokerPlayerEventArgs(player));
            }
        }

        //private void NextRoles()
        //{
        //    playerContainer[_dealer].Role.dealer = true;
        //    playerContainer[_bigBlind].Role.bigBlind = true;
        //    playerContainer[_smallBlind].Role.smallBlind = true;

        //    OnDealerChipRound();
        //}


        public void GeneratePlayers(CharacterTypes mainPlayerCharacterType)
        {
            playerContainer = new List<Player>();
            Random rand = new Random();

            mainPlayer = new MainPlayer("Daniel", mainPlayerCharacterType, StartingMoney);

            for(int i = 0; i < 5; i++)
            {
                int tempCharacterType = rand.Next(0, 5);
                CharacterTypes character = (CharacterTypes)tempCharacterType;
                playerContainer.Add(new BotPlayer(character, StartingMoney));
                if(i == 1)
                {
                    playerContainer.Add(mainPlayer);
                }
            }
            playerContainer[3].Role.dealer = true;
            playerContainer[4].Role.smallBlind = true;
            playerContainer[5].Role.bigBlind = true;

            MiddleFieldSection = new MiddleField(_deck, playerContainer);
        }

        private int GetDealer()
        {
            for(int i = 0; i<playerContainer.Count; i++)
            {
                if (playerContainer[i].Role.dealer) return i;
            }
            throw new PokerGameException("Dealer can not be found");
        }

        private int GetSmallBlind()
        {
            for (int i = 0; i < playerContainer.Count; i++)
            {
                if (playerContainer[i].Role.smallBlind) return i;
            }
            throw new PokerGameException("Small blind can not be found");
        }

        private int GetBigBlind()
        {
            for (int i = 0; i < playerContainer.Count; i++)
            {
                if (playerContainer[i].Role.bigBlind) return i;
            }
            throw new PokerGameException("Big blind can not be found");
        }


        private void ChangePlayersOrder(bool lastRound)
        {
            List<Player> result = new List<Player>();
            playerContainer[GetDealer()].Role.dealer = false;
            playerContainer[GetSmallBlind()].Role.smallBlind = false;
            playerContainer[GetBigBlind()].Role.bigBlind = false;
            if(lastRound) CheckPlayersInGame();
            for (int i = 0; i< playerContainer.Count-1; i++)
            {
                result.Add(playerContainer[i + 1]);
            }
            result.Add(playerContainer[0]);
            result[3 % playerContainer.Count].Role.dealer = true;
            result[4 % playerContainer.Count].Role.smallBlind = true;
            result[5 % playerContainer.Count].Role.bigBlind = true;
            playerContainer = result;
            OnDealerChipRound();
        }

        private void TakeMandatoryBets()
        {
            playerContainer[GetSmallBlind()].TakeMandatoryBet(_blindValue);
            playerContainer[GetBigBlind()].TakeMandatoryBet(_blindValue);
            OnPlayerActionEvent(playerContainer[GetSmallBlind()]);
            OnPlayerActionEvent(playerContainer[GetBigBlind()]);
        }

        private void EndOfTheRoundUpdates()
        {
            foreach(var player in playerContainer)
            {
                player.RoundEndFoldCards();
            }
            MiddleFieldSection.ClearTheSection();
            OnRefreshPlayers();
            OnUpdateMiddleSectionEvent();
        }


        public async void AsyncTestUnFoldMiddleCards()
        {
            _end = false;
            if (MiddleFieldSection.CommonityCards.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    MiddleFieldSection.UnfoldNextCard();
                    await Task.Delay(200);
                    OnUpdateMiddleSectionEvent();
                }
            }
            else if (MiddleFieldSection.CommonityCards.Count < 5)
            {
                MiddleFieldSection.UnfoldNextCard();
                OnUpdateMiddleSectionEvent();
            }
            OnCheckCombinationEvent();
        }


        public void MainPlayerAction(Action action)
        {
            _end = false;
            mainPlayer.PlayerAction(ref _actualLicitBet, action);
            OnPlayerActionEvent(mainPlayer);
        }



        private void CheckWinner()
        {
            List<Player> sortedwinners = new List<Player>();
            foreach (var player in playerContainer) sortedwinners.Add(player);
            sortedwinners.Sort((p1, p2) => p1.CompareTo(p2, MiddleFieldSection.CommonityCards));
            sortedwinners.Reverse();
            var realwinners = sortedwinners.Where(p => sortedwinners[0].CompareTo(p, MiddleFieldSection.CommonityCards) == 0).Select(p => p).ToList();
            //int winningPrice = MiddleFieldSection.CommonityBet / winners.Count;
            while ( MiddleFieldSection.PrizeDistribution(realwinners))
            {
                foreach(var player in realwinners)
                {
                    sortedwinners.Remove(player);
                }
                sortedwinners.Sort((p1, p2) => p1.CompareTo(p2, MiddleFieldSection.CommonityCards));
                sortedwinners.Reverse();
                realwinners = sortedwinners.Where(p => sortedwinners[0].CompareTo(p, MiddleFieldSection.CommonityCards) == 0).Select(p => p).ToList();

            }
        }

        private void CheckPlayersInGame()
        {
            foreach(var p in playerContainer) p.CheckIfInGame();

            for(int i = 0; i<playerContainer.Count; i++)
            {
                if (!playerContainer[i].InGame)
                {
                    var outOfGame = playerContainer[i];
                    playerContainer.RemoveAt(i);
                    OnOutOfGamePlayerEvent(outOfGame);
                    playersOutOfTheGame.Add(outOfGame);
                    i--;
                }
            }
        }

        public async void AsyncStartRound()
        {
            await Task.Delay(500);
            RemaineTime = 10000; //needs to handle
            _actualLicitBet = 200;
            int delayTime = 150;
            foreach(var p in playerContainer)
            {
                if (p.Signed)
                {
                    p.Signed = false;
                    OnSignPlayerEvent(p);
                }
            }

            TakeMandatoryBets();
            for (int i = 0; i < playerContainer.Count; i++)
            {
                await Task.Delay(delayTime);
                playerContainer[i].hand.leftHand = _deck.getCard();
                if (playerContainer[i] == mainPlayer)
                {
                    playerContainer[i].hand.leftHand.isUpSideDown = false;
                    OnCheckCombinationEvent();
                }
                OnCardAllocation(playerContainer[i]);
            }

            for (int i = 0; i < playerContainer.Count; i++)
            {
                await Task.Delay(delayTime);
                playerContainer[i].hand.rightHand = _deck.getCard();
                if (playerContainer[i] == mainPlayer)
                {
                    playerContainer[i].hand.rightHand.isUpSideDown = false;
                    OnCheckCombinationEvent();
                }
                OnCardAllocation(playerContainer[i]);
            }
            AsyncRound(1);
        }


        public async void AsyncRound(int numberOfRound) //Refactor this function
        {
            if (numberOfRound == 5)
            {
                await Task.Delay(500);
                MiddleFieldSection.CollectBets(playerContainer);
                foreach (var player in playerContainer) player.UnfoldHand();
                CheckWinner();
                MiddleFieldSection.CollectBets(playerContainer);
                OnRoundOverForPlayersEvent(playerContainer);
                //if (playerContainer.Count > 1) AsyncStartRound();

                await Task.Delay(1000);
                ChangePlayersOrder(true);
                EndOfTheRoundUpdates();
                await Task.Delay(1000);
                OnUpdateMiddleSectionEvent();
                OnRefreshPlayers();
                _deck.Refresh();
                AsyncStartRound();
                return;
            }
            for (int i = 0; i<playerContainer.Count; i++)
            {
                if (playerContainer[i].StaticName == "MainPlayer") OnMainPlayerTurn(true);
                playerContainer[i].Signed = true;
                OnSignPlayerEvent(playerContainer[i]);

                _end = true;
                for (int j = 0; j < 50 && _end; j++)
                {
                    await Task.Delay(2);
                    RemaineTime -= 10;
                    //OnRefreshRemainTime();
                }
                //await Task.Delay(10000);
                playerContainer[i].PlayerAction(ref _actualLicitBet);
                OnPlayerActionEvent(playerContainer[i]);

                if (playerContainer[i].StaticName == "MainPlayer") OnMainPlayerTurn(false);
                playerContainer[i].Signed = false;
                await Task.Delay(150);
                OnSignPlayerEvent(playerContainer[i]);
                
                RemaineTime = 10000;
                OnRefreshRemainTime();

            }

            await Task.Delay(500);
            MiddleFieldSection.CollectBets(playerContainer);
            await Task.Delay(500);

            if (numberOfRound == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    MiddleFieldSection.UnfoldNextCard();
                    await Task.Delay(200);
                    OnUpdateMiddleSectionEvent();
                }
                OnCheckCombinationEvent();
            } 
            else if (numberOfRound < 4)
            {
                MiddleFieldSection.UnfoldNextCard();
                OnUpdateMiddleSectionEvent();
                OnCheckCombinationEvent(); //Event that needs to look after it
            }

            ChangePlayersOrder(false);

            await Task.Delay(400);
            if (numberOfRound != 4) TakeMandatoryBets();
            AsyncRound(++numberOfRound);
        }

        public void GameOn()
        {
            AsyncStartRound();
        }

    }
}
