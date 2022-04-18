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
    public class PokerModel
    {
        public event EventHandler<PokerPlayerEventArgs> CardAllocation;
        public event EventHandler<CommonityCardsEventArgs> UnfoldCardEvent;
        public event EventHandler<ActionEvenArgs> PlayerActionEvent;
        public event EventHandler<PokerPlayerEventArgs> SignPlayerEvent;
        public event EventHandler<PlayersEventArg> RoundOverForPlayersEvent;
        public event EventHandler RefreshRemainTime;
        public event EventHandler DealerChipPass;
        public event EventHandler MainPlayerTurnEvent;
        public event EventHandler<CommonityCardsEventArgs> CheckCombinationEvent; //need to change the name of the template parameter
        public event EventHandler RefreshPlayers;


        public bool MainplayerTurn { get; private set; }
        public int StartingMoney { private get; set; } // Not sure if it is okey
        public int PlayersNum { private get; set; }
        public List<Player> playerContainer; 
        public MainPlayer p;
        public MiddleField MiddleFieldSection { get; private set; } // need to be private but needs to handle the events
        public StatusCards StatusCards { get; private set; }

        private enum Role { DEALER, SMALLBLIND, BIGBLIND}

        private int _dealer;
        private int _smallBlind;
        private int _bigBlind;

        private int _blindValue = 100;
        public int BlindValue { get { return _blindValue; } }

        private int _actualLicitBet;
        private bool _end; // should give an intuitive name for this

        public Tuple<PokerHandRanks, List<Card>> _mainPlayerHandRank;

        public int RemaineTime { private set; get; }
        public int getActualLicitBet() { return _actualLicitBet; }

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
                cards.Add(p.hand.leftHand);
                cards.Add(p.hand.rightHand);
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
                PlayerActionEvent(this, new ActionEvenArgs(player));
            }
        }

        private void OnUnfoldCardEvent(List<Card> commonityCards)
        {
            if(UnfoldCardEvent != null)
            {
                UnfoldCardEvent(this, new CommonityCardsEventArgs(commonityCards));
            }
        }

        private void OnCardAllocation(Player player)
        {
            if (CardAllocation != null)
            {
                CardAllocation(this, new PokerPlayerEventArgs(player));
            }
        }


        public PokerModel(int playersNumber, int startingMoney = 2000)
        {
            if (playersNumber > 5 || playersNumber < 1) throw new ArgumentException();
            PlayersNum = playersNumber;
            StartingMoney = startingMoney;
            MiddleFieldSection = new MiddleField();
            StatusCards = new StatusCards();
        }

        private void NextRoles()
        {
            playerContainer[_dealer].Role.dealer = true;
            playerContainer[_bigBlind].Role.bigBlind = true;
            playerContainer[_smallBlind].Role.smallBlind = true;

            OnDealerChipRound();
        }


        public void GeneratePlayers()
        {
            playerContainer = new List<Player>();
            Random rand = new Random();

            p = new MainPlayer("Daniel", CharacterTypes.BOB, StartingMoney);

            for(int i = 0; i < 5; i++)
            {
                int tempCharacterType = rand.Next(0, 5);
                CharacterTypes character = (CharacterTypes)tempCharacterType;
                playerContainer.Add(new BotPlayer(character, StartingMoney));
                if(i == 1)
                {
                    playerContainer.Add(p);
                }
            }
            _dealer = 3;
            playerContainer[3].Role.dealer = true;
            _smallBlind = 4;
            playerContainer[4].Role.smallBlind = true;
            _bigBlind = 5;
            playerContainer[5].Role.bigBlind = true;
        }

        private List<Player> ChangePlayersOrder(List<Player> players)
        {
            List<Player> result = new List<Player>();
            playerContainer[_dealer].Role.dealer = false;
            playerContainer[_smallBlind].Role.smallBlind = false;
            playerContainer[_bigBlind].Role.bigBlind = false;
            for (int i = 0; i<players.Count-1; i++)
            {
                result.Add(players[i + 1]);
            }
            result.Add(players[0]);
            _dealer = 3;
            _smallBlind = 4;
            _bigBlind = 5;
            return result;
        }

        private void TakeMandatoryBets()
        {
            playerContainer[_smallBlind].TakeMandatoryBet(_blindValue);
            playerContainer[_bigBlind].TakeMandatoryBet(_blindValue);
            OnPlayerActionEvent(playerContainer[_smallBlind]);
            OnPlayerActionEvent(playerContainer[_bigBlind]);
        }

        public async void AsyncStartRound()
        {
            RemaineTime = 10000;
            _actualLicitBet = 200;
            int delayTime = 150;

            TakeMandatoryBets();
            for (int i = 0; i<playerContainer.Count; i++)
            {
                Random rand = new Random(); // look for if it is worth to create every time
                await Task.Delay(delayTime);
                playerContainer[i].hand.leftHand = MiddleField.CardGenerator(rand);
                if (playerContainer[i] == p)
                {
                    playerContainer[i].hand.leftHand.isUpSideDown = false;
                    OnCheckCombinationEvent();
                }
                OnCardAllocation(playerContainer[i]);
            }

            for (int i = 0; i < playerContainer.Count; i++)
            {
                Random rand = new Random(); // look for if it is worth to create every time
                await Task.Delay(delayTime);
                playerContainer[i].hand.rightHand = MiddleField.CardGenerator(rand);
                if (playerContainer[i] == p)
                {
                    playerContainer[i].hand.rightHand.isUpSideDown = false;
                    OnCheckCombinationEvent();
                }
                OnCardAllocation(playerContainer[i]);
            }
            AsyncRound(1);
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
                    OnUnfoldCardEvent(MiddleFieldSection.CommonityCards);
                }
            }
            else if (MiddleFieldSection.CommonityCards.Count < 5)
            {
                MiddleFieldSection.UnfoldNextCard();
                OnUnfoldCardEvent(MiddleFieldSection.CommonityCards);
            }
            OnCheckCombinationEvent();
        }


        public void MainPlayerAction(Action action)
        {
            _end = false;
            p.PlayerAction(ref _actualLicitBet, action);
            OnPlayerActionEvent(p);
        }



        private void CheckWinner()
        {
            List<Player> winners = new List<Player>();
            foreach (var player in playerContainer) winners.Add(player);
            winners.Sort((p1, p2) => p1.CompareTo(p2, MiddleFieldSection.CommonityCards));
            winners.Reverse();
            winners = winners.Where(p => winners[0].CompareTo(p, MiddleFieldSection.CommonityCards) == 0).Select(p => p).ToList();
            int winningPrice = MiddleFieldSection.CommonityBet / winners.Count;
            foreach(var player in winners)
            {
                player.gainPrize(winningPrice);
                player.Signed = true;
            }
        }

        public async void AsyncRound(int numberOfRound)
        {
            if (numberOfRound == 5)
            {
                await Task.Delay(500);
                MiddleFieldSection.CollectBets(playerContainer);
                foreach (var p in playerContainer) p.UnfoldHand();
                CheckWinner();
                OnRoundOverForPlayersEvent(playerContainer);
                MiddleFieldSection.CollectBets(playerContainer);
                return;
            }
            for (int i = 0; i<playerContainer.Count; i++)
            {
                if (playerContainer[i].StaticName == "MainPlayer") OnMainPlayerTurn(true);
                playerContainer[i].Signed = true;
                OnSignPlayerEvent(playerContainer[i]);

                _end = true;
                for (int j = 0; j < 200 && _end; j++)
                {
                    await Task.Delay(5);
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
                    OnUnfoldCardEvent(MiddleFieldSection.CommonityCards);
                }
                OnCheckCombinationEvent();
            } 
            else if (numberOfRound < 4)
            {
                MiddleFieldSection.UnfoldNextCard();
                OnUnfoldCardEvent(MiddleFieldSection.CommonityCards);
                OnCheckCombinationEvent();
            }

            playerContainer = ChangePlayersOrder(playerContainer);
            NextRoles();
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
