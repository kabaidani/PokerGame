using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame.Model
{
    public abstract class Player
    {
        //public string PlayerName { private set; get; }
        public string StaticName { protected set; get; }
        public CharacterTypes Character { protected set; get; }
        public int BetChips { protected set; get; }
        public int Money { protected set; get; }
        public Action LastAction { protected set; get; }
        public bool InGame { protected set; get; }
        public Hand hand; // the set should be more safer
        public int RaiseBet { set; get; }
        public bool Signed { set; get; }
        public Role Role;
        public PokerHandRanks PokerHandRanks { private set; get; } // need to be carefull with the useage of this

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

        public void gainPrize(int ammount)
        {
            Money += ammount;
        }

        public void TakeMandatoryBet(int blindValue)
        {
            if (Role.bigBlind)
            {
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

        public abstract void PlayerAction(ref int actualLicitBet, Action chosenAction = Action.NOACTION);

    }

    public class BotPlayer : Player
    {
        private static int _characterCounter = 1;
        public BotPlayer(CharacterTypes character, int money) : base("Character" + _characterCounter.ToString(), character, money)
        {
            _characterCounter++;
        }

        public override void PlayerAction(ref int actualLicitBet, Action chosenAction = Action.NOACTION)
        {
            Random rand = new Random();
            chosenAction = Action.CALL;//(Action)rand.Next(0, 4);
            LastAction = chosenAction;
            if (chosenAction == Action.FOLD)
            {
                this.hand.leftHand.cardType.cardRank = CardRank.NOCARD;
                this.hand.leftHand.cardType.cardSuit = CardSuit.NOCARD;

                this.hand.rightHand.cardType.cardRank = CardRank.NOCARD;
                this.hand.rightHand.cardType.cardSuit = CardSuit.NOCARD;
                
                this.LastAction = Action.FOLD;
                //Event for fold
            }
            else if (chosenAction == Action.CALL)
            {
                Money -= actualLicitBet;
                BetChips += actualLicitBet;
                //Event for call
            }
            else if (chosenAction == Action.CHECK)
            {
                //Event for check
            }
            else if (chosenAction == Action.RAISE)
            {
                // need to thro an error if RaiseBet < 2*actualLicitBet
                Money -= RaiseBet;
                actualLicitBet = RaiseBet;
                BetChips += actualLicitBet;
                //Event for raise
            }
            else if (chosenAction == Action.NOACTION) { }
        }

    }

    public class MainPlayer : Player
    {
        public string PlayerName { private set; get; }

        public MainPlayer(string playerName, CharacterTypes character, int money) : base("MainPlayer", character, money)
        {
            PlayerName = playerName;
        }

        public override void PlayerAction(ref int actualLicitBet, Action chosenAction = Action.NOACTION)
        {
            LastAction = chosenAction;
            if (chosenAction == Action.FOLD)
            {
                this.hand.leftHand.cardType.cardRank = CardRank.NOCARD;
                this.hand.leftHand.cardType.cardSuit = CardSuit.NOCARD;

                this.hand.rightHand.cardType.cardRank = CardRank.NOCARD;
                this.hand.rightHand.cardType.cardSuit = CardSuit.NOCARD;

                this.LastAction = Action.FOLD;
                this.InGame = false;
                //Event for fold
            } else if (chosenAction == Action.CALL)
            {
                Money -= actualLicitBet;
                BetChips += actualLicitBet;
                //Event for call
            } else if (chosenAction == Action.CHECK)
            {
                //Event for check
            } else if (chosenAction == Action.RAISE)
            {
                // need to thro an error if RaiseBet < 2*actualLicitBet
                Money -= RaiseBet;
                actualLicitBet = RaiseBet;
                BetChips += actualLicitBet;
                //Event for raise
            } else if (chosenAction == Action.NOACTION) { }
        }

    }
}
