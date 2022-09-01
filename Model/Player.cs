using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PokerGame.Model
{
    public abstract class Player
    {
        public event EventHandler<PossibleActionsEventArgs> SetActionOptionsEvent;
        //public string PlayerName { private set; get; }
        public string StaticName { protected set; get; }
        public CharacterTypes Character { protected set; get; }
        public int BetChips { protected set; get; }
        public int Money { protected set; get; }
        public Action LastAction { protected set; get; }
        public bool InGame { set; get; } //should set the set property protected
        public bool InRound { set; get; }
        public Hand hand; // the set should be more safer
        public int RaiseBet { set; get; }
        public bool Signed { set; get; }
        public Role Role;
        private int _gainedPrize;
        public int StartLicitBet { set; get; } //Indicates the licit bet value at the start of each round
        public PokerHandRanks PokerHandRanks { protected set; get; } // need to be carefull with the useage of this
        public bool AlreadyRaisedInThisRound { set; get; }

        protected void OnSetActionOptionsEvent(List<Action> actions)
        {
            if (SetActionOptionsEvent != null)
            {
                SetActionOptionsEvent(this, new PossibleActionsEventArgs(actions));
            }
        }

        public Player(string staticName, CharacterTypes character, int money)
        {
            Role = new Role();
            Signed = false;
            RaiseBet = 0;
            //PlayerName = playerName;
            StaticName = staticName;
            Character = character;
            BetChips = 0;
            Money = money;
            LastAction = Action.NOACTION;
            InGame = true;
            InRound = true;
            _gainedPrize = 0;
            AlreadyRaisedInThisRound = false;
        }

        public int GetGainedPrize()
        {
            var returnVal = _gainedPrize;
            _gainedPrize = 0;
            return returnVal;
        }

        public void ClearLastAction()
        {
            LastAction = Action.NOACTION;
        }

        public int CollectBet()
        {
            if (!this.InGame) return 0;

            int ammount = BetChips;
            BetChips = 0;
            return ammount;
        }

        public void UnfoldHand()
        {
            this.hand.rightHand.isUpSideDown = false;
            this.hand.leftHand.isUpSideDown = false;
        }

        public void RoundEnd()
        {
            InRound = true;
        }

        public void gainPrize(int ammount)
        {
            _gainedPrize = ammount;
            Money += ammount;
        }

        public void TakeMandatoryBet(int blindValue)
        {
            if (Role.bigBlind)
            {
                LastAction = Action.BIGBLIND;
                RaiseBet = blindValue;
                if(Money > blindValue)
                {
                    Money -= blindValue;
                    BetChips = blindValue;
                } else
                {
                    BetChips = Money;
                    Money = 0;
                }
            }else if (Role.smallBlind)
            {
                LastAction = Action.SMALLBLIND;
                int smallBlindValue = blindValue / 2;
                if (Money > smallBlindValue)
                {
                    Money -= smallBlindValue;
                    BetChips = smallBlindValue;
                }
                else
                {
                    BetChips = Money;
                    Money = 0;
                }
            }
        }

        public void RoundEndFoldCards()
        {
            hand.leftHand = new Card();
            hand.rightHand = new Card();
        }

        public List<Card> combinationCards; // This is not in the good place

        public PokerHandRanks CheckCombination(List<Card> commonityCards)
        {
            List<Card> cards = new List<Card>();
            cards.Add(this.hand.leftHand);
            cards.Add(this.hand.rightHand);
            cards.AddRange(commonityCards);
            combinationCards = new List<Card>();
            PokerHandRanks = StatusCards.checkPokerCombination(cards, ref combinationCards);
            return PokerHandRanks;
        }

        public int CompareTo(Player p, List<Card> commonityCards)
        {
            var ownCombination = this.CheckCombination(commonityCards);
            var pCombination = p.CheckCombination(commonityCards);
            if (ownCombination > pCombination) return 1;
            if (ownCombination < pCombination) return -1;
            if (ownCombination == pCombination)
            {
                if (ownCombination == PokerHandRanks.FULLHOUSE)
                {
                    int res = (StatusCards.FullHouseRankThreeOfKind(this.combinationCards) - StatusCards.FullHouseRankThreeOfKind(p.combinationCards));
                    if(res == 0)
                    {
                        res = (StatusCards.FullHouseRankPair(this.combinationCards) - StatusCards.FullHouseRankPair(p.combinationCards));
                        return res;
                    }else
                    {
                        return res;
                    }
                } else if(ownCombination == PokerHandRanks.STRAIGHTFLUSH)
                {
                    var sortedOwnPlayerCards = this.combinationCards.OrderBy(p => p.cardType.cardRank).ToList();
                    var sortedPPlayerCards = p.combinationCards.OrderBy(p => p.cardType.cardRank).ToList();
                    for(int i = sortedOwnPlayerCards.Count - 1; i>=0; i--)
                    {
                        int res = sortedOwnPlayerCards[i].cardType.cardRank - sortedPPlayerCards[i].cardType.cardRank;
                        if (res != 0) return res;
                    }
                    return 0;
                } else if (ownCombination == PokerHandRanks.FOUROFAKIND)
                {
                    CardRank ownRank = this.combinationCards[0].cardType.cardRank;
                    CardRank pRank = p.combinationCards[0].cardType.cardRank;
                    if(ownRank == pRank)
                    {
                        List<Card> ownSortedHandCards = new List<Card>();
                        List<Card> pSortedHandCards = new List<Card>();
                        if (this.hand.leftHand.cardType.cardRank >= this.hand.rightHand.cardType.cardRank)
                        {
                            ownSortedHandCards.Add(this.hand.leftHand);
                            ownSortedHandCards.Add(this.hand.rightHand);
                        }
                        else
                        {
                            ownSortedHandCards.Add(this.hand.rightHand);
                            ownSortedHandCards.Add(this.hand.leftHand);
                        }
                        if (p.hand.leftHand.cardType.cardRank >= p.hand.rightHand.cardType.cardRank)
                        {
                            pSortedHandCards.Add(p.hand.leftHand);
                            pSortedHandCards.Add(p.hand.rightHand);
                        }
                        else
                        {
                            pSortedHandCards.Add(p.hand.rightHand);
                            pSortedHandCards.Add(p.hand.leftHand);
                        }
                        if (ownSortedHandCards[0].cardType.cardRank == pSortedHandCards[0].cardType.cardRank)
                        {
                            return ownSortedHandCards[1].cardType.cardRank - pSortedHandCards[1].cardType.cardRank;
                        }
                        else
                        {
                            return ownSortedHandCards[0].cardType.cardRank - pSortedHandCards[0].cardType.cardRank;
                        }
                    } else
                    {
                        return ownRank - pRank;
                    }
                } else if (ownCombination == PokerHandRanks.FLUSH)
                {
                    var sortedOwnPlayerCards = this.combinationCards.OrderBy(p => p.cardType.cardRank).ToList();
                    var sortedPPlayerCards = p.combinationCards.OrderBy(p => p.cardType.cardRank).ToList();
                    for (int i = sortedOwnPlayerCards.Count - 1; i >= 0; i--)
                    {
                        int res = sortedOwnPlayerCards[i].cardType.cardRank - sortedPPlayerCards[i].cardType.cardRank;
                        if (res != 0) return res;
                    }
                    return 0;
                } else if (ownCombination == PokerHandRanks.STRAIGHT)
                {
                    var sortedOwnPlayerCards = this.combinationCards.OrderBy(p => p.cardType.cardRank).ToList();
                    var sortedPPlayerCards = p.combinationCards.OrderBy(p => p.cardType.cardRank).ToList();
                    return sortedOwnPlayerCards[sortedOwnPlayerCards.Count - 1].cardType.cardRank - sortedPPlayerCards[sortedPPlayerCards.Count - 1].cardType.cardRank;
                } else if (ownCombination == PokerHandRanks.THREEOFAKIND)
                {
                    CardRank ownRank = this.combinationCards[0].cardType.cardRank;
                    CardRank pRank = p.combinationCards[0].cardType.cardRank;
                    if (ownRank == pRank)
                    {
                        List<Card> ownSortedHandCards = new List<Card>();
                        List<Card> pSortedHandCards = new List<Card>();
                        if(this.hand.leftHand.cardType.cardRank >= this.hand.rightHand.cardType.cardRank)
                        {
                            ownSortedHandCards.Add(this.hand.leftHand);
                            ownSortedHandCards.Add(this.hand.rightHand);
                        }else
                        {
                            ownSortedHandCards.Add(this.hand.rightHand);
                            ownSortedHandCards.Add(this.hand.leftHand);
                        }
                        if (p.hand.leftHand.cardType.cardRank >= p.hand.rightHand.cardType.cardRank)
                        {
                            pSortedHandCards.Add(p.hand.leftHand);
                            pSortedHandCards.Add(p.hand.rightHand);
                        }
                        else
                        {
                            pSortedHandCards.Add(p.hand.rightHand);
                            pSortedHandCards.Add(p.hand.leftHand);
                        }
                        if(ownSortedHandCards[0].cardType.cardRank == pSortedHandCards[0].cardType.cardRank)
                        {
                            return ownSortedHandCards[1].cardType.cardRank - pSortedHandCards[1].cardType.cardRank;
                        }
                        else
                        {
                            return ownSortedHandCards[0].cardType.cardRank - pSortedHandCards[0].cardType.cardRank;
                        }

                    } else
                    {
                        return ownRank - pRank;
                    }
                } else if (ownCombination == PokerHandRanks.TWOPAIR)
                {
                    CardRank ownHigherCardRank = StatusCards.TwoPairHigher(this.combinationCards);
                    CardRank pHigherCardRank = StatusCards.TwoPairHigher(p.combinationCards);
                    if(ownHigherCardRank == pHigherCardRank)
                    {
                        CardRank ownLowerCardRank = StatusCards.TwoPairLower(this.combinationCards);
                        CardRank pLowerCardRank = StatusCards.TwoPairLower(p.combinationCards);
                        if(ownLowerCardRank == pLowerCardRank)
                        {
                            List<Card> ownSortedHandCards = new List<Card>();
                            List<Card> pSortedHandCards = new List<Card>();
                            if (this.hand.leftHand.cardType.cardRank >= this.hand.rightHand.cardType.cardRank)
                            {
                                ownSortedHandCards.Add(this.hand.leftHand);
                                ownSortedHandCards.Add(this.hand.rightHand);
                            }
                            else
                            {
                                ownSortedHandCards.Add(this.hand.rightHand);
                                ownSortedHandCards.Add(this.hand.leftHand);
                            }
                            if (p.hand.leftHand.cardType.cardRank >= p.hand.rightHand.cardType.cardRank)
                            {
                                pSortedHandCards.Add(p.hand.leftHand);
                                pSortedHandCards.Add(p.hand.rightHand);
                            }
                            else
                            {
                                pSortedHandCards.Add(p.hand.rightHand);
                                pSortedHandCards.Add(p.hand.leftHand);
                            }
                            if (ownSortedHandCards[0].cardType.cardRank == pSortedHandCards[0].cardType.cardRank)
                            {
                                return ownSortedHandCards[1].cardType.cardRank - pSortedHandCards[1].cardType.cardRank;
                            }
                            else
                            {
                                return ownSortedHandCards[0].cardType.cardRank - pSortedHandCards[0].cardType.cardRank;
                            }
                        }
                        else
                        {
                            return ownLowerCardRank - pLowerCardRank;
                        }
                    }
                    else
                    {
                        return ownHigherCardRank - pHigherCardRank;
                    }
                } else if (ownCombination == PokerHandRanks.PAIR)
                {
                    CardRank ownCardRank = this.combinationCards[0].cardType.cardRank;
                    CardRank pCardRank = p.combinationCards[0].cardType.cardRank;
                    if(ownCardRank == pCardRank)
                    {
                        List<Card> ownSortedHandCards = new List<Card>();
                        List<Card> pSortedHandCards = new List<Card>();
                        if (this.hand.leftHand.cardType.cardRank >= this.hand.rightHand.cardType.cardRank)
                        {
                            ownSortedHandCards.Add(this.hand.leftHand);
                            ownSortedHandCards.Add(this.hand.rightHand);
                        }
                        else
                        {
                            ownSortedHandCards.Add(this.hand.rightHand);
                            ownSortedHandCards.Add(this.hand.leftHand);
                        }
                        if (p.hand.leftHand.cardType.cardRank >= p.hand.rightHand.cardType.cardRank)
                        {
                            pSortedHandCards.Add(p.hand.leftHand);
                            pSortedHandCards.Add(p.hand.rightHand);
                        }
                        else
                        {
                            pSortedHandCards.Add(p.hand.rightHand);
                            pSortedHandCards.Add(p.hand.leftHand);
                        }
                        if (ownSortedHandCards[0].cardType.cardRank == pSortedHandCards[0].cardType.cardRank)
                        {
                            return ownSortedHandCards[1].cardType.cardRank - pSortedHandCards[1].cardType.cardRank;
                        }
                        else
                        {
                            return ownSortedHandCards[0].cardType.cardRank - pSortedHandCards[0].cardType.cardRank;
                        }
                    } else
                    {
                        return ownCardRank - pCardRank;
                    }
                } else if(ownCombination == PokerHandRanks.HIGHCARD)
                {
                    List<Card> ownSortedCards = new List<Card> { this.hand.leftHand, this.hand.rightHand };
                    List<Card> pSortedCards = new List<Card> { p.hand.leftHand, p.hand.rightHand };
                    ownSortedCards.AddRange(commonityCards);
                    pSortedCards.AddRange(commonityCards);

                    ownSortedCards = ownSortedCards.OrderBy(p => p.cardType.cardRank).ToList();
                    pSortedCards = pSortedCards.OrderBy(p => p.cardType.cardRank).ToList();

                    int counter = 0;
                    while(counter < 5)
                    {
                        int res = ownSortedCards[ownSortedCards.Count - counter - 1].cardType.cardRank - pSortedCards[pSortedCards.Count - counter - 1].cardType.cardRank;
                        if (res != 0) return res;
                        counter++;
                    }
                    return 0;
                }
            }
            throw new PokerGameException("not able to find to suitable combination");
        }
        
        public bool CheckIfInGame()
        {
            if (Money <= 0) InGame = false;
            return InGame;
        }

        public void PlayerAction(ref int actualLicitBet, ref int lastRaiseValue, int followingRaiseOrBetValue,
            int bigBlind, ref List<Tuple<Action, int>> previousActions, Action chosenAction = Action.NOACTION)
        {
            if (chosenAction == Action.FOLD)
            {
                this.hand.leftHand.cardType.cardRank = CardRank.NOCARD;
                this.hand.leftHand.cardType.cardSuit = CardSuit.NOCARD;

                this.hand.rightHand.cardType.cardRank = CardRank.NOCARD;
                this.hand.rightHand.cardType.cardSuit = CardSuit.NOCARD;
                InRound = false;
                previousActions.Add(new Tuple<Action, int>(Action.FOLD, 0));
            }
            else if (chosenAction == Action.CALL)
            {

                int requiredBet = actualLicitBet;
                int givenValue = requiredBet - this.BetChips;
                if (Money >= givenValue)
                {
                    Money -= givenValue;
                    BetChips += givenValue;
                }
                else
                {
                    BetChips += Money;
                    Money = 0;
                }

            }
            else if (chosenAction == Action.CHECK)
            {
                previousActions.Add(new Tuple<Action, int>(Action.CHECK, 0));
            }
            else if (chosenAction == Action.BET)
            {
                //if (followingRaiseOrBetValue < bigBlind) throw new PokerGameException("Not legal bet value");
                if (followingRaiseOrBetValue > Money)
                {
                    RaiseBet = Money;
                    Money = 0;
                    lastRaiseValue = RaiseBet;
                    actualLicitBet = RaiseBet + BetChips;
                    BetChips += RaiseBet;
                }
                else
                {
                    RaiseBet = followingRaiseOrBetValue;
                    Money -= RaiseBet;
                    lastRaiseValue = RaiseBet;
                    actualLicitBet = RaiseBet + BetChips;
                    BetChips += RaiseBet;
                }
                previousActions.Add(new Tuple<Action, int>(Action.BET, RaiseBet));
                AlreadyRaisedInThisRound = true;
            }
            else if (chosenAction == Action.RAISE)
            {
                if (followingRaiseOrBetValue < 2 * lastRaiseValue)
                {
                    string str = "It could be problematic";
                }
                if (Money < followingRaiseOrBetValue)
                {
                    int mustDeleteMe = 10;
                    //throw new PokerGameException("The Raisebet is higher than the actual credit"); //This is the all in 
                }
                RaiseBet = followingRaiseOrBetValue;
                Money -= RaiseBet;
                lastRaiseValue = RaiseBet - actualLicitBet;
                actualLicitBet = RaiseBet + BetChips;
                BetChips += RaiseBet;
                previousActions.Add(new Tuple<Action, int>(Action.RAISE, lastRaiseValue));
                AlreadyRaisedInThisRound = true;
                //Event for raise
            }
            else if (chosenAction == Action.NOACTION) { }
            LastAction = chosenAction;

        }

        private List<Action> PossibleActionsForNoneBlind(List<Player> players, int bigBlindValue, int actualLicitBet, ref int minRaiseOrBetValue, 
            int lastRaiseValue, ref List<Tuple<Action, int>> previousActions)
        {
            List<Action> actions = new List<Action>();
            actions.Add(Action.FOLD);

            bool checkOrBet = true;
            foreach (var action in previousActions)
            {
                if (action.Item1 == Action.BET || action.Item1 == Action.RAISE || action.Item1 == Action.CALL)
                {
                    lastRaiseValue = action.Item2;
                    checkOrBet = false;
                }
            }
            if (checkOrBet)
            {
                minRaiseOrBetValue = bigBlindValue;
                if(minRaiseOrBetValue <= Money || this.StaticName == "MainPlayer")
                {
                    actions.Add(Action.BET);
                }
                actions.Add(Action.CHECK);
            }
            else //Call or Raise
            {
                minRaiseOrBetValue = lastRaiseValue + actualLicitBet;
                if(minRaiseOrBetValue <= this.Money && AlreadyRaisedInThisRound == false)
                {
                    actions.Add(Action.RAISE);
                }
                actions.Add(Action.CALL);
            }

            return actions;

        }

        private List<Action> PossibleActionsForBigBlind(List<Player> players, int bigBlindValue, int actualLicitBet, ref int minRaiseOrBetValue, int lastRaiseValue)
        {
            List<Action> actions = new List<Action>();
            actions.Add(Action.FOLD);

            if (actualLicitBet - BetChips > 0)
            {
                actions.Add(Action.CALL);
                actions.Add(Action.RAISE);
                minRaiseOrBetValue = lastRaiseValue * 2; // TODO check if lastRaiseValue is good
            } else if (actualLicitBet - BetChips == 0)
            {
                actions.Add(Action.CHECK);
                actions.Add(Action.RAISE);
                minRaiseOrBetValue = bigBlindValue;
            } else
            {
                throw new PokerGameException("Not valid use case when the licit bet is less than the betted value");
            }

            return actions;
        }

        private List<Action> PossibleActionsForSmallBlind(List<Player> players, int bigBlindValue, int actualLicitBet, ref int minRaiseOrBetValue, int lastRaiseValue)
        {
            List<Action> actions = new List<Action>();
            
            actions.Add(Action.FOLD);
            actions.Add(Action.CALL);
            actions.Add(Action.RAISE);

            if (actualLicitBet == bigBlindValue)
            {
                minRaiseOrBetValue = bigBlindValue * 2;
            }
            else
            {
                minRaiseOrBetValue = lastRaiseValue * 2; //TODO consider to give the bigBlindValue value to lastRaiseValue after the binds
            }

            return actions;
        }

        protected List<Action> PossibleActions(List<Player> players, int bigBlindValue, int actualLicitBet, ref int minRaiseOrBetValue, 
            int lastRaiseValue, ref List<Tuple<Action, int>> previousActions)
        {
            if (LastAction == Action.BIGBLIND)
            {
                return PossibleActionsForBigBlind(players, bigBlindValue, actualLicitBet, ref minRaiseOrBetValue, lastRaiseValue); //Correct it
            }
            if (LastAction == Action.SMALLBLIND)
            {
                return PossibleActionsForSmallBlind(players, bigBlindValue, actualLicitBet, ref minRaiseOrBetValue, lastRaiseValue); //Correct it
            }
            return PossibleActionsForNoneBlind(players, bigBlindValue, actualLicitBet, ref minRaiseOrBetValue, lastRaiseValue, ref previousActions);
        }

        protected void SelectValuesForGivenActionPlus(bool holding, bool raising,
            double raiseOrBetValue, List<Action> possiblyActions, ref Action followingAction, ref int followingRaiseValue)
        {
            //TODO Pay attention to not use random chances
            if (holding) //Hold the crad
            {
                if (possiblyActions.Contains(Action.CHECK)) followingAction = Action.CHECK;
                else if (possiblyActions.Contains(Action.CALL)) followingAction = Action.CALL;
                else
                {
                    throw new PokerGameException("Possibly actions must contain CHECK or CALL");
                }
            }
            if (raising && possiblyActions.Count > 2) //Raise
            {
                if (possiblyActions.Contains(Action.RAISE)) followingAction = Action.RAISE;
                else if (possiblyActions.Contains(Action.BET)) followingAction = Action.BET;
                else
                {
                    throw new PokerGameException("Possibly actions must contain CHECK or CALL");
                }
                followingRaiseValue = (int)raiseOrBetValue; //Completely fucked here both of two value are parameteres
            }
        }



    }

    public class BotPlayer : Player
    {
        private static int _characterCounter = 1;
        public BotPlayer(CharacterTypes character, int money) : base("Character" + _characterCounter.ToString(), character, money)
        {
            _characterCounter++;
        }

        public void ActualPlayerAction(ref int actualLicitBet, List<Card> commonityCards, 
            List<Player> players, int bigBlindValue, ref int lastRaiseValue, ref List<Tuple<Action, int>> previousActions)
        {
            int minRaiseOrBetValue = 0;
            var actions = PossibleActions(players, bigBlindValue, actualLicitBet, ref minRaiseOrBetValue, lastRaiseValue, ref previousActions);
            List<Card> cards = new List<Card>();
            cards.Add(this.hand.leftHand);
            cards.Add(this.hand.rightHand);
            cards.AddRange(commonityCards);
            combinationCards = new List<Card>();
            PokerHandRanks = StatusCards.checkPokerCombination(cards, ref combinationCards);
            int followingRaiseValue = 0;
            Action followingAction = Action.FOLD;


            if (actions.Contains(Action.CHECK)) followingAction = Action.CHECK;
            if (commonityCards.Count == 0) //The first set when there is no cards in commonity section
            {

                if( PokerHandRanks == PokerHandRanks.HIGHCARD )
                {
                    SelectValuesForGivenActionPlus(true, false, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue); //No Raise in case of PAIR
                } else
                {
                    SelectValuesForGivenActionPlus(true, true, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue); //No Raise in case of PAIR
                }
                
                PlayerAction(ref actualLicitBet, ref lastRaiseValue, followingRaiseValue, bigBlindValue, ref previousActions, followingAction);
                return;
            }

            if (commonityCards.Count == 3)
            {
                //Should examine the probability
                if (StatusCards.CheckProbablityOfRoyalFlush(cards) == 1 || StatusCards.CheckProbabilityOfStraightFlush(cards) == 1)
                {
                    SelectValuesForGivenActionPlus(true, false, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue);
                }
                else if (StatusCards.CheckProbabilityOfFourOfKind(cards) == 1 || StatusCards.CheckProbabilityOfFullHouse(cards) == 1
                        || StatusCards.CheckProbabilityOfFlush(cards) == 1 || StatusCards.CheckProbabilityOfStraight(cards) == 1)
                {
                    SelectValuesForGivenActionPlus(true, false, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue); //We should raise here the bet
                }
                else if (StatusCards.CheckProbabilityOfThreeOfKind(cards) == 1 || StatusCards.CheckProbabilityOfTwoPair(cards) == 1)
                {
                    SelectValuesForGivenActionPlus(true, false, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue);
                }
            }
            else if ( commonityCards.Count == 4 )
            {
                if (StatusCards.CheckProbabilityOfFourOfKind(cards) == 1 || StatusCards.CheckProbabilityOfFullHouse(cards) == 1
                         || StatusCards.CheckProbabilityOfFlush(cards) == 1 || StatusCards.CheckProbabilityOfStraight(cards) == 1)
                {
                    SelectValuesForGivenActionPlus(true, false, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue);
                }
                else if (StatusCards.CheckProbablityOfRoyalFlush(cards) == 1 || StatusCards.CheckProbabilityOfStraightFlush(cards) == 1)
                {
                    SelectValuesForGivenActionPlus(false, false, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue);
                }
                else if (StatusCards.CheckProbabilityOfThreeOfKind(cards) == 1 || StatusCards.CheckProbabilityOfTwoPair(cards) == 1)
                {
                    SelectValuesForGivenActionPlus(true, false, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue);
                }
            }

            if (PokerHandRanks == PokerHandRanks.FOUROFAKIND || PokerHandRanks == PokerHandRanks.STRAIGHTFLUSH || PokerHandRanks == PokerHandRanks.ROYALFLUSH)
            {
                SelectValuesForGivenActionPlus(true, true, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue);
            }
            else if (PokerHandRanks == PokerHandRanks.FULLHOUSE)
            {
                SelectValuesForGivenActionPlus(true, true, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue);
            }
            else if (PokerHandRanks == PokerHandRanks.FLUSH)
            {
                SelectValuesForGivenActionPlus(true, true, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue);
            }
            else if (PokerHandRanks == PokerHandRanks.STRAIGHT)
            {
                SelectValuesForGivenActionPlus(true, true, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue);

            }
            else if (PokerHandRanks == PokerHandRanks.THREEOFAKIND)
            {
                SelectValuesForGivenActionPlus(true, true, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue);
            }
            else if (PokerHandRanks == PokerHandRanks.TWOPAIR)
            {
                SelectValuesForGivenActionPlus(true, true, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue);
            }
            else if (PokerHandRanks == PokerHandRanks.PAIR)
            {
                SelectValuesForGivenActionPlus(true, true, minRaiseOrBetValue, actions, ref followingAction, ref followingRaiseValue); //It shouldn't raise value
            }


            PlayerAction(ref actualLicitBet, ref lastRaiseValue, followingRaiseValue, bigBlindValue, ref previousActions, followingAction);
        }

    }

    public class MainPlayer : Player
    {
        public string PlayerName { private set; get; }
        public int MinRaiseBet { private set; get; }

        public MainPlayer(string playerName, CharacterTypes character, int money) : base("MainPlayer", character, money)
        {
            PlayerName = playerName;
        }

        public void SetPossibleActions(ref int actualLicitBet, List<Card> commonityCards,
            List<Player> players, int bigBlindValue, ref int lastRaiseValue, ref List<Tuple<Action, int>> previousActions)
        {
            int minRaiseOrBetValue = 0;
            var actions = PossibleActions(players, bigBlindValue, actualLicitBet, ref minRaiseOrBetValue, lastRaiseValue, ref previousActions);
            List<Card> cards = new List<Card>();
            MinRaiseBet = minRaiseOrBetValue;
            OnSetActionOptionsEvent(actions);
        }

    }
}
