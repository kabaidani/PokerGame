using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading;

namespace PokerGame.Model
{

    public class PokerModel
    {
        public event EventHandler<PokerPlayerEventArgs> CardAllocation;
        public event EventHandler<CommonityCardsEventArgs> UnfoldCardEvent;
        public event EventHandler<ActionEvenArgs> PlayerActionEvent;
        public event EventHandler<PokerPlayerEventArgs> SignPlayerEvent;
        public event EventHandler RefreshRemainTime;
        public event EventHandler DealerChipPass;

        public int StartingMoney { private get; set; } // Not sure if it is okey
        public int PlayersNum { private get; set; }
        public List<Player> playerContainer; 
        public MainPlayer p;

        private enum Role { DEALER, SMALLBLIND, BIGBLIND}

        private int _dealer;
        private int _smallBlind;
        private int _bigBlind;

        private int _blindValue = 100;

        private int _actualLicitBet;
        private bool _end; // should give an intuitive name for this


        public int RemaineTime { private set; get; }
        public int getActualLicitBet() { return _actualLicitBet; }

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
        }


        private void NextPlayer(Role role, ref int idx)
        {
            for(int i = 1; i<playerContainer.Count; i++)
            {
                idx = (idx + i) % playerContainer.Count;
                if (playerContainer[idx].InGame)
                {
                    if(role == Role.DEALER)
                    {
                        playerContainer[idx].Role.dealer = true;
                    }else if (role == Role.BIGBLIND)
                    {
                        playerContainer[idx].Role.bigBlind = true;
                    }else if (role == Role.SMALLBLIND)
                    {
                        playerContainer[idx].Role.smallBlind = true;
                    }
                    return;
                }
            }
        }

        private void NextRoles()
        {
            playerContainer[_dealer].Role.dealer = false;
            playerContainer[_bigBlind].Role.bigBlind = false;
            playerContainer[_smallBlind].Role.smallBlind = false;

            NextPlayer(Role.DEALER, ref _dealer);
            NextPlayer(Role.BIGBLIND, ref _bigBlind);
            NextPlayer(Role.SMALLBLIND, ref _smallBlind);
            OnDealerChipRound();
        }

        public MiddleField MiddleFieldSection { get; private set; } // need to be private but needs to handle the events


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
                if(i == 3)
                {
                    playerContainer.Add(p);
                }
            }
            _dealer = 0;
            playerContainer[0].Role.dealer = true;
            _smallBlind = 1;
            playerContainer[1].Role.smallBlind = true;
            _bigBlind = 2;
            playerContainer[2].Role.bigBlind = true;
        }

        private List<Player> ChangePlayersOrder(List<Player> players)
        {
            List<Player> result = new List<Player>();
            for(int i = 0; i<players.Count; i++)
            {
                int idx = (_dealer + i) % players.Count;
                result.Add(players[idx]);
            }
            _dealer = 0;
            _smallBlind = 1;
            _bigBlind = 2;
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
                OnCardAllocation(playerContainer[i]);
            }

            for (int i = 0; i < playerContainer.Count; i++)
            {
                Random rand = new Random(); // look for if it is worth to create every time
                await Task.Delay(delayTime);
                playerContainer[i].hand.rightHand = MiddleField.CardGenerator(rand);
                OnCardAllocation(playerContainer[i]);
            }
            AsyncRound(1);
        }

        public async void AsyncTestUnFoldMiddleCards()
        {
            _end = false;
            if(MiddleFieldSection.CommonityCards.Count == 0)
            {
                for(int i = 0; i<3; i++)
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
        }
        

        public void MainPlayerAction(Action action)
        {
            p.PlayerAction(ref _actualLicitBet, action);
            OnPlayerActionEvent(p);
        }

        public async void AsyncRound(int numberOfRound)
        {
            if (numberOfRound == 5)
            {
                await Task.Delay(500);
                MiddleFieldSection.CollectBets(playerContainer);
                OnUnfoldCardEvent(MiddleFieldSection.CommonityCards);
                return;
            }
            for (int i = 0; i<playerContainer.Count; i++)
            {
                if (numberOfRound == 1 && (playerContainer[i].Role.bigBlind || playerContainer[i].Role.smallBlind || playerContainer[i].Role.dealer)) continue;
                playerContainer[i].Signed = true;
                OnSignPlayerEvent(playerContainer[i]);

                _end = true;
                for (int j = 0; j < 100 && _end; j++)
                {
                    await Task.Delay(10);
                    RemaineTime -= 10;
                    OnRefreshRemainTime();
                }
                //await Task.Delay(10000);
                playerContainer[i].PlayerAction(ref _actualLicitBet);
                OnPlayerActionEvent(playerContainer[i]);


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
            } 
            else if (numberOfRound < 4)
            {
                MiddleFieldSection.UnfoldNextCard();
                OnUnfoldCardEvent(MiddleFieldSection.CommonityCards);
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
