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
        public event EventHandler<PossibleActionsEventArgs> SetActionOptionsEvent;
        public event EventHandler RefreshRemainTime;
        public event EventHandler DealerChipPass;
        public event EventHandler MainPlayerTurnEvent;
        public event EventHandler<CommonityCardsEventArgs> CheckCombinationEvent; //need to change the name of the template parameter
        public event EventHandler RefreshPlayers;
        public event EventHandler<PlayersEventArg> RefreshGivenPlayers;
        public event EventHandler RefreshCommonityBet;
        public event EventHandler LockingKeyStateChangeEvent;
        public event EventHandler<PlayersEventArg> UpdateGainedPrizeEvent;
        public event EventHandler GameOverEvent;
        public event EventHandler NewGameEvent;
        public event EventHandler CloseGameEvent;
        public event EventHandler BlindValuesEvent;


        public bool MainplayerTurn { get; private set; }
        public int StartingMoney { private get; set; } // Not sure if it is okey
        public int PlayersNum { private get; set; }
        public List<Player> playerContainer;
        public List<Player> playersOutOfTheGame;
        public MainPlayer mainPlayer;
        public MiddleField MiddleFieldSection { get; private set; } // need to be private but needs to handle the events
        public StatusCards StatusCards { get; private set; }
        public List<Tuple<Action, int>> previousActions;

        private enum Role { DEALER, SMALLBLIND, BIGBLIND}

        private int _blindValue = 100;
        public int BlindValue { get { return _blindValue; } }
        private int _lastRaiseValue;
        
        private int _actualLicitBet;
        private bool _end; // should give an intuitive name for this

        private Player _roundStarterPlayer;

        public Tuple<PokerHandRanks, List<Card>> _mainPlayerHandRank;

        public int RemaineTime { private set; get; }
        public int getActualLicitBet() { return _actualLicitBet; }
        private Deck _deck;
        private bool _lockingKey;
        private Random rand;
        private bool _gameOver;

        public PokerModel(int playersNumber, int startingMoney = 2000)
        {
            rand = new Random();
            if (playersNumber > 5 || playersNumber < 1) throw new ArgumentException();
            PlayersNum = playersNumber;
            StartingMoney = startingMoney;
            _deck = new Deck();
            StatusCards = new StatusCards();
            playersOutOfTheGame = new List<Player>();
            _lockingKey = false;
            previousActions = new List<Tuple<Action, int>>();
            _gameOver = false;
            OnLockingKeyStateChangeEvent();
        }

        private void OnRefreshCommonityBet()
        {
            if(RefreshCommonityBet != null)
            {
                RefreshCommonityBet(this, EventArgs.Empty);
            }
        }

        private void OnLockingKeyStateChangeEvent()
        {
            if(LockingKeyStateChangeEvent != null)
            {
                LockingKeyStateChangeEvent(this, EventArgs.Empty);
            }
        }

        private void OnUpdateGainedPrizeEvent(List<Player> players)
        {
            if(UpdateGainedPrizeEvent != null)
            {
                UpdateGainedPrizeEvent(this, new PlayersEventArg(players));
            }
        }

        private void OnGameOverEvent()
        {
            _gameOver = true;
            if(GameOverEvent != null)
            {
                GameOverEvent(this, EventArgs.Empty);
            }
        }

        public void OnCloseGameEvent()
        {
            if(CloseGameEvent != null)
            {
                CloseGameEvent(this, EventArgs.Empty);
            }
        }

        public void OnBlindValuesEvent()
        {
            if(BlindValuesEvent != null)
            {
                BlindValuesEvent(this, EventArgs.Empty);
            }
        }

        public void OnNewGameEvent()
        {
            if(NewGameEvent != null)
            {
                NewGameEvent(this, EventArgs.Empty);
            }
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


        public void GeneratePlayers(CharacterTypes mainPlayerCharacterType)
        {
            playerContainer = new List<Player>();

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
            //_roundStarterPlayer = playerContainer[5];

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
            _actualLicitBet = _blindValue;
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

        public void ReleaseLockingKey()
        {
            _lockingKey = false;
            OnLockingKeyStateChangeEvent();
        }

        public void LockLockingKey()
        {
            _lockingKey = true;
            OnLockingKeyStateChangeEvent();
        }

        public bool CheckLockingKeyState()
        {
            return _lockingKey;
        }

        public void MainPlayerAction(Action action, int raiseOrBetValue = 0)
        {
            _end = false;

            mainPlayer.PlayerAction(ref _actualLicitBet, ref _lastRaiseValue, raiseOrBetValue, _blindValue, ref previousActions, action);
        }



        private void CheckWinner()
        {
            List<Player> sortedwinners = new List<Player>();
            foreach (var player in playerContainer)
            {
                if(player.LastAction != Action.FOLD)
                {
                    sortedwinners.Add(player);
                }
            }
            sortedwinners.Clear();
            sortedwinners.Add(mainPlayer);

            sortedwinners.Sort((p1, p2) => p1.CompareTo(p2, MiddleFieldSection.CommonityCards));
            sortedwinners.Reverse();
            var realwinners = sortedwinners.Where(p => sortedwinners[0].CompareTo(p, MiddleFieldSection.CommonityCards) == 0).Select(p => p).ToList();
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
            List<Player> playerTempList = new List<Player>();
            foreach(var p in playerContainer)
            { 
                if (p.CheckIfInGame()) playerTempList.Add(p);
                else
                {
                    OnOutOfGamePlayerEvent(p);
                    playersOutOfTheGame.Add(p);
                }
            }
            playerContainer = playerTempList;
            if (playerContainer.Count == 1 || !mainPlayer.InGame)
            {
                OnGameOverEvent();
            }

        }

        public async void AsyncStartRound()
        {
            if (_gameOver == true) return;
            previousActions.Add(new Tuple<Action, int>(Action.BET, BlindValue));
            await Task.Delay(500);
            RemaineTime = 10000; //needs to handle
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
                    playerContainer[i].hand.leftHand.isUpSideDown = false; //Just temp
                if (playerContainer[i] == mainPlayer)
                {
                    OnCheckCombinationEvent();
                }
                OnCardAllocation(playerContainer[i]);
            }

            for (int i = 0; i < playerContainer.Count; i++)
            {
                await Task.Delay(delayTime);
                playerContainer[i].hand.rightHand = _deck.getCard();
                    playerContainer[i].hand.rightHand.isUpSideDown = false; //Just temp
                if (playerContainer[i] == mainPlayer)
                {
                    OnCheckCombinationEvent();
                }
                OnCardAllocation(playerContainer[i]);
            }
            AsyncRound(1);
        }

        private async Task WaitingTimeForPlayers(int thinkingTimeMultiplier, Player p)
        {
            for (int j = 0; j < thinkingTimeMultiplier && _end; j++)
            {
                await Task.Delay(20);
                RemaineTime -= 20;
                OnRefreshRemainTime();
            }
            if(p.StaticName == "MainPlayer" && _end)
            {
                MainPlayerAction(Action.FOLD);
            }
        }

        private bool CheckIfEveryOneFolded()
        {
            int counter = 0;
            foreach(var p in playerContainer)
            {
                if (p.LastAction != Action.FOLD) counter++;
            }
            return counter == 1;
        }

        private bool CheckIfEveryOneHasNoLeftMoney()
        {
            int counter = 0;
            foreach (var p in playerContainer)
            {
                if (p.Money > 0) counter++;
            }
            return counter == 1;
        }

        private async Task RoundNumberFive()
        {
            await Task.Delay(1000);
            MiddleFieldSection.CollectBets(playerContainer);
            foreach (var player in playerContainer)
            {
                if(player.LastAction != Action.FOLD) player.UnfoldHand();
            }
            CheckWinner();
            MiddleFieldSection.CollectBets(playerContainer);
            OnRoundOverForPlayersEvent(playerContainer);

            OnUpdateGainedPrizeEvent(playerContainer);
            LockLockingKey();
            while (_lockingKey) await Task.Delay(200);

            ChangePlayersOrder(true);
            EndOfTheRoundUpdates();
            await Task.Delay(2500);
            OnUpdateMiddleSectionEvent();
            foreach (var player in playerContainer)
            {
                player.ClearLastAction();
                player.RoundEnd();
            } 
            OnRefreshPlayers();
            _deck.Refresh();

            _blindValue = _blindValue + 100;
            OnBlindValuesEvent();

            AsyncStartRound();
            return;
        }

        private void PutPlayersInQueueInOrder(ref Queue<Player> nextPlayerQueue, Player firstPlayer)
        {
            nextPlayerQueue.Clear();
            int i = playerContainer.IndexOf(firstPlayer);
            if (i < 0) throw new PokerGameException("The FirstPlayer can not be found among the other players");
            int idx = 1;
            while(idx < playerContainer.Count)
            {
                int nextPlayerIdx = (i + idx) % playerContainer.Count;
                nextPlayerQueue.Enqueue(playerContainer[nextPlayerIdx]);
                idx++;
            }
        }

        private async Task PlayersTurn(bool firstTurn)
        {
            if (firstTurn)
            {
                _roundStarterPlayer = playerContainer[GetBigBlind()];
            }

            Queue<Player> nextPlayersQueue = new Queue<Player>();
            if(_roundStarterPlayer == null)
            {
                PutPlayersInQueueInOrder(ref nextPlayersQueue, playerContainer[GetDealer()]);
                nextPlayersQueue.Enqueue(playerContainer[GetDealer()]);
            } else
            {
                PutPlayersInQueueInOrder(ref nextPlayersQueue, _roundStarterPlayer);
            }
            if (firstTurn) nextPlayersQueue.Enqueue(_roundStarterPlayer);

            while (nextPlayersQueue.Count != 0)
            {

                Player p = nextPlayersQueue.Dequeue();
                if (!p.InRound || p.Money == 0)
                {
                    continue;
                }


                if (p.StaticName == "MainPlayer") OnMainPlayerTurn(true);
                p.Signed = true;
                OnSignPlayerEvent(p);

                _end = true;
                int thinkingTimeMultiplier = 0;
                if (p.StaticName != "MainPlayer")
                {
                    BotPlayer actPlayer = p as BotPlayer;

                    actPlayer.ActualPlayerAction(ref _actualLicitBet, MiddleFieldSection.CommonityCards, playerContainer, _blindValue, ref _lastRaiseValue, ref previousActions);
                    thinkingTimeMultiplier = rand.Next(100, 150);
                    thinkingTimeMultiplier = 50;
                }
                else
                {
                    MainPlayer actPlayer = p as MainPlayer;
                    actPlayer.SetPossibleActions(ref _actualLicitBet, MiddleFieldSection.CommonityCards, playerContainer, _blindValue, ref _lastRaiseValue, ref previousActions);
                    thinkingTimeMultiplier = 500; //Max waiting time ~ 10 seconds
                }

                thinkingTimeMultiplier += 50;
                await WaitingTimeForPlayers(thinkingTimeMultiplier, p);

                if (p.LastAction == Action.RAISE || p.LastAction == Action.BET)
                {
                    _roundStarterPlayer = p;
                    PutPlayersInQueueInOrder(ref nextPlayersQueue, p);
                } 

                OnPlayerActionEvent(p);
                if (p.StaticName == "MainPlayer") OnMainPlayerTurn(false);
                p.Signed = false;
                await Task.Delay(150);
                OnSignPlayerEvent(p);

                RemaineTime = 10000;
                OnRefreshRemainTime();
            }
            _actualLicitBet = 0;
            _roundStarterPlayer = null;
        }

        public async void AsyncRound(int numberOfRound) //Refactor this function
        {
            if (numberOfRound == 5)
            {
                await RoundNumberFive();
                return;
            }

            await PlayersTurn(numberOfRound == 1);


            await Task.Delay(1500);
            MiddleFieldSection.CollectBets(playerContainer);
            OnRefreshCommonityBet();
            OnRefreshPlayers();
            await Task.Delay(1500);


            bool everyOneFolderButOnePlayer = CheckIfEveryOneFolded() || CheckIfEveryOneHasNoLeftMoney();
            if (everyOneFolderButOnePlayer)
            {
                for (int i = MiddleFieldSection.CommonityCards.Count; i < 5; i++)
                {
                    MiddleFieldSection.UnfoldNextCard();
                    await Task.Delay(200);
                    OnUpdateMiddleSectionEvent();
                }
                OnCheckCombinationEvent();
            }

            if (numberOfRound == 1 && !everyOneFolderButOnePlayer)
            {
                for (int i = 0; i < 3; i++)
                {
                    MiddleFieldSection.UnfoldNextCard();
                    await Task.Delay(200);
                    OnUpdateMiddleSectionEvent();
                }
                OnCheckCombinationEvent();
            } 
            else if (numberOfRound <= 4 && !everyOneFolderButOnePlayer)
            {
                MiddleFieldSection.UnfoldNextCard();
                OnUpdateMiddleSectionEvent();
                OnCheckCombinationEvent(); //Event that needs to look after it
            }


            foreach (var player in playerContainer)
            {
                if(player.LastAction != Action.FOLD) player.ClearLastAction();
                player.AlreadyRaisedInThisRound = false;
            }

            previousActions.Clear();
            await Task.Delay(400);
            if (everyOneFolderButOnePlayer)
            {
                AsyncRound(5);
            } else
            {
                AsyncRound(++numberOfRound);
            }
        }

        public void GameOn()
        {
            AsyncStartRound();
        }

    }
}
