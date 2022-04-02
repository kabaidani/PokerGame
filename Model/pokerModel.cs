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
    public enum PokerHandRanks { ROYALFLUSH, STRAIGHTFLUSH, FOUROFAKIND, FULLHOUSE, FLUSH, STRAIGHT, THREEOFAKIND, TWOPAIR, HIGHCARD }
    public class PokerModel
    {
        public event EventHandler<PokerPlayerEventArgs> CardAllocation;
        public event EventHandler<CommonityCardsEventArgs> UnfoldCardEvent;
        public event EventHandler<ActionEvenArgs> PlayerActionEvent;
        public event EventHandler<PokerPlayerEventArgs> SignPlayerEvent;
        public event EventHandler RefreshRemainTime;
        public event EventHandler DealerChipPass;
        public event EventHandler MainPlayerTurnEvent;

        public bool MainplayerTurn { get; private set; }
        public int StartingMoney { private get; set; } // Not sure if it is okey
        public int PlayersNum { private get; set; }
        public List<Player> playerContainer; 
        public MainPlayer p;

        private enum Role { DEALER, SMALLBLIND, BIGBLIND}

        private int _dealer;
        private int _smallBlind;
        private int _bigBlind;

        private int _blindValue = 100;
        public int BlindValue { get { return _blindValue; } }

        private int _actualLicitBet;
        private bool _end; // should give an intuitive name for this

        public Tuple<PokerHandRanks, List<Card>> _mainPlayerHandRank;

        private bool CardsEqual(Card lhs, Card rhs, int checkLevel)
        {
            if(checkLevel == 0)
            {
                if (lhs.cardType.cardSuit == rhs.cardType.cardSuit &&
                   lhs.cardType.cardRank == rhs.cardType.cardRank) return true;
                return false;
            }
            if (checkLevel == 1)
            {
                if (lhs.cardType.cardSuit == rhs.cardType.cardSuit) return true;
                return false;
            }
            if (checkLevel == 2)
            {
                if (lhs.cardType.cardRank == rhs.cardType.cardRank) return true;
                return false;
            }
            return false;

        }

        public Card WeakestCard(List<Card> cards)
        {
            CardRank cardRank = CardRank.ACE;
            int idx = 0;
            for(int i = 0; i<cards.Count; i++)
            {
                if(cards[i].cardType.cardRank < cardRank)
                {
                    cardRank = cards[i].cardType.cardRank;
                    idx = i;
                }
            }
            return cards[idx];
        }

        private Tuple<bool, Card> CardsContain(List<Card> cards, Card card, int checkLevel)
        {
            foreach(var c in cards)
            {
                if(CardsEqual(c, card, checkLevel))
                {
                    return new Tuple<bool, Card>(true, c);
                }
            }
            return new Tuple<bool, Card>(false, new Card());
        }

        private Tuple<int, List<Card>> CardRankOccureNumber(List<Card> hand, CardRank rank)
        {
            int result = 0;
            List<Card> resultList = new List<Card>();
            foreach(var card in hand)
            {
                if (card.cardType.cardRank == rank)
                {
                    resultList.Add(card);
                    result++;
                }
            }

            return new Tuple<int, List<Card>>(result, resultList);
        }


        private int CardNumberInList(List<Card> cards, Card card)
        {
            int result = 0;
            foreach(var c in cards)
            {
                if (CardsEqual(c, card, 2)) result++;
            }
            return result;
        }

        public bool CheckRoyalFlush(List<Card> hand, ref List<Card> result) //export this
        {
            for(int i = 0; i<hand.Count-4; i++)
            {
                result.Clear();
                var suit = hand[i].cardType.cardSuit;
                var containedRes = CardsContain(hand, new Card(new CardType(suit, CardRank.TEN), false), 0);
                if (containedRes.Item1) result.Add(containedRes.Item2);
                else continue;
                containedRes = CardsContain(hand, new Card(new CardType(suit, CardRank.JACK), false), 0);
                if (containedRes.Item1) result.Add(containedRes.Item2);
                else continue;
                containedRes = CardsContain(hand, new Card(new CardType(suit, CardRank.QUEEN), false), 0);
                if (containedRes.Item1) result.Add(containedRes.Item2);
                else continue;
                containedRes = CardsContain(hand, new Card(new CardType(suit, CardRank.KING), false), 0);
                if (containedRes.Item1) result.Add(containedRes.Item2);
                else continue;
                containedRes = CardsContain(hand, new Card(new CardType(suit, CardRank.ACE), false), 0);
                if (containedRes.Item1) result.Add(containedRes.Item2);
                else continue;

                return true;
            }
            return false;
        }

        public bool CheckStraightFlush(List<Card> hand, ref List<Card> result) //export this
        {
            foreach(var card in hand)
            {
                result.Clear();
                int idx = 1;
                bool res = true;
                while(idx < 5 && res)
                {
                    var containedRes = CardsContain(hand, new Card(new CardType(card.cardType.cardSuit, card.cardType.cardRank + idx), false), 0);
                    if (containedRes.Item1)
                    {
                        result.Add(card);
                        idx++;
                    }
                    else
                    {
                        res = false;
                    }
                }
                if (res) return true;
            }
            return false;
        }

        public bool CheckFourOfAKind(List<Card> hand, ref List<Card> result)
        {
            return CheckParNumberKind(hand, 4, ref result);
        }

        public bool CheckFlush(List<Card> hand, ref List<Card> result)
        {
            for(int i = 0; i<hand.Count; i++)
            {
                result.Clear();
                int number = 1;
                result.Add(hand[i]);
                for (int j = i+1; j<hand.Count; j++)
                {
                    if (CardsEqual(hand[i], hand[j], 1))
                    {
                        number++;
                        result.Add(hand[j]);
                    }
                }
                if (number >= 5) return true;
            }
            return false;
        }

        public bool CheckStraight(List<Card> hand, ref List<Card> result)
        {
            foreach(var refCard in hand)
            {
                bool res = true;
                for(int j = 1; j<5 && res; j++)
                {
                    res &= CardsContain(hand, new Card(new CardType(CardSuit.NOCARD, refCard.cardType.cardRank + j), false), 2).Item1;
                }
                if (res) return true;
            }
            return false;
        }

        public bool CheckThreeOfAKind(List<Card> hand, ref List<Card> result)
        {
            return CheckParNumberKind(hand, 3, ref result);
        }

        public bool CheckTwoPair(List<Card> hand, ref List<Card> result)
        {
            for(int i = 0; i<hand.Count; i++)
            {
                var card = hand[i];
                var containedRes = CardRankOccureNumber(hand, card.cardType.cardRank);
                if (containedRes.Item1 >= 2)
                {
                    for(int j = i+1; j<hand.Count; j++)
                    {
                        if (hand[j].cardType.cardRank == card.cardType.cardRank) continue;
                        var containedRes2 = CardRankOccureNumber(hand, hand[j].cardType.cardRank);
                        if (containedRes2.Item1 >= 2)
                        {
                            result = containedRes.Item2.Concat(containedRes2.Item2).ToList();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckPair(List<Card> hand, ref List<Card> result)
        {
            return CheckParNumberKind(hand, 2, ref result);
        }

        public bool CheckHighCard(List<Card> hand, ref List<Card> result)
        {
            result.Clear();
            var tempList = hand.OrderBy(p => p.cardType.cardRank).ToList();
            int idx = 0;
            CardRank rank = CardRank.NOCARD;
            for(int i = 0; i<hand.Count; i++)
            {
                if(rank < hand[i].cardType.cardRank)
                {
                    rank = hand[i].cardType.cardRank;
                    idx = i;
                }
            }
            result.Add(hand[idx]);
            return true;
        }

        private bool CheckParNumberKind(List<Card> hand, int p, ref List<Card> result)
        {
            result.Clear();
            foreach (var card in hand)
            {
                var containedRes = CardRankOccureNumber(hand, card.cardType.cardRank);
                if (containedRes.Item1 >= p)
                {
                    result = containedRes.Item2;
                    return true;
                }
            }
            return false;
        }

        public int RemaineTime { private set; get; }
        public int getActualLicitBet() { return _actualLicitBet; }

        private void OnMainPlayerTurn(bool mainPlayerTurn)
        {
            MainplayerTurn = mainPlayerTurn;
            if(MainPlayerTurnEvent != null)
            {
                MainPlayerTurnEvent(this, EventArgs.Empty);
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
            playerContainer[_dealer].Role.dealer = true;
            playerContainer[_bigBlind].Role.bigBlind = true;
            playerContainer[_smallBlind].Role.smallBlind = true;

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
            _end = false;
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
                if (playerContainer[i].StaticName == "MainPlayer") OnMainPlayerTurn(true);
                playerContainer[i].Signed = true;
                OnSignPlayerEvent(playerContainer[i]);

                _end = true;
                for (int j = 0; j < 200 && _end; j++)
                {
                    await Task.Delay(30);
                    RemaineTime -= 10;
                    OnRefreshRemainTime();
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
