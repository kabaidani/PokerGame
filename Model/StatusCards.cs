﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame.Model
{
    public enum PokerHandRanks { HIGHCARD, PAIR, TWOPAIR, THREEOFAKIND, STRAIGHT, FLUSH, FULLHOUSE, FOUROFAKIND, STRAIGHTFLUSH, ROYALFLUSH } //Should segregate into a new class
    public class StatusCards
    {
        private static Tuple<bool, Card> CardsContain(List<Card> cards, Card card, int checkLevel)
        {
            foreach (var c in cards)
            {
                if (CardsEqual(c, card, checkLevel))
                {
                    return new Tuple<bool, Card>(true, c);
                }
            }
            return new Tuple<bool, Card>(false, new Card());
        }

        private static bool CardsEqual(Card lhs, Card rhs, int checkLevel)
        {
            if (checkLevel == 0)
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

        private static Tuple<int, List<Card>> CardRankOccureNumber(List<Card> hand, CardRank rank)
        {
            int result = 0;
            List<Card> resultList = new List<Card>();
            foreach (var card in hand)
            {
                if (card.cardType.cardRank == rank)
                {
                    resultList.Add(card);
                    result++;
                }
            }

            return new Tuple<int, List<Card>>(result, resultList);
        }

        private static int CardNumberInList(List<Card> cards, Card card)
        {
            int result = 0;
            foreach (var c in cards)
            {
                if (c.cardType.cardRank == card.cardType.cardRank) result++;
            }
            return result;
        }

        private static bool CheckParNumberKind(List<Card> hand, int p, ref List<Card> result)
        {
            result.Clear();
            foreach (var card in hand)
            {
                var containedRes = CardRankOccureNumber(hand, card.cardType.cardRank);
                if (containedRes.Item1 >= p)
                {
                    if(result.Count == 0 || result[0].cardType.cardRank < containedRes.Item2[0].cardType.cardRank)
                    {
                        result = containedRes.Item2;
                    }
                }
            }
            return result.Count != 0;
        }

        public static bool CheckRoyalFlush(List<Card> hand, ref List<Card> result)
        {
            for (int i = 0; i < hand.Count - 4; i++)
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

        public static bool CheckStraightFlush(List<Card> hand, ref List<Card> result) //export this
        {
            var sortedHand = hand.OrderBy(p => p.cardType.cardRank).ToList();
            for(int i = sortedHand.Count - 1; i>=0; i--)
            {
                var card = sortedHand[i];
                result.Clear();
                int idx = 1;
                bool res = true;
                result.Add(card);
                while (idx < 5 && res)
                {
                    var containedRes = CardsContain(hand, new Card(new CardType(card.cardType.cardSuit, card.cardType.cardRank - idx), false), 0);
                    if (containedRes.Item1)
                    {
                        result.Add(containedRes.Item2);
                        idx++;
                    }
                    else
                    {
                        res = false;
                    }
                }
                result.Reverse();
                if (res) return true;
            }
            return false;
        }

        public static bool CheckFullHouse(List<Card> hand, ref List<Card> result)
        {
            result.Clear();
            var tempList = new List<Card>();
            if(StatusCards.CheckThreeOfAKind(hand, ref tempList))
            {
                CardRank forbiddenedCardRank = tempList[0].cardType.cardRank;
                foreach(var card in hand)
                {
                    if (card.cardType.cardRank == forbiddenedCardRank) continue;
                    var containedRes = CardRankOccureNumber(hand, card.cardType.cardRank);
                    if(containedRes.Item1 >= 2)
                    {
                        for (int i = 0; i < 2; i++) tempList.Add(containedRes.Item2[i]);
                        result = tempList;
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CheckFlush(List<Card> hand, ref List<Card> result)
        {
            hand = hand.OrderBy(p => p.cardType.cardRank).ToList();
            hand.Reverse();
            for (int i = 0; i < hand.Count; i++)
            {
                result.Clear();
                int number = 1;
                result.Add(hand[i]);
                for (int j = i + 1; j < hand.Count && result.Count < 5; j++)
                {
                    if (CardsEqual(hand[i], hand[j], 1))
                    {
                        number++;
                        result.Add(hand[j]);
                    }
                }
                if (number >= 5)
                {
                    result.Reverse();
                    return true;
                }
            }
            return false;
        }

        public static bool CheckFourOfAKind(List<Card> hand, ref List<Card> result)
        {
            return CheckParNumberKind(hand, 4, ref result);
        }

        public static bool CheckThreeOfAKind(List<Card> hand, ref List<Card> result)
        {
            return CheckParNumberKind(hand, 3, ref result);
        }

        public static bool CheckTwoPair(List<Card> hand, ref List<Card> result)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                var card = hand[i];
                var containedRes = CardRankOccureNumber(hand, card.cardType.cardRank);
                if (containedRes.Item1 >= 2)
                {
                    for (int j = i + 1; j < hand.Count; j++)
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

        public static bool CheckPair(List<Card> hand, ref List<Card> result)
        {
            return CheckParNumberKind(hand, 2, ref result);
        }


        public static bool CheckStraight(List<Card> hand, ref List<Card> result)
        {
            hand = hand.OrderBy(p => p.cardType.cardRank).ToList();
            for(int i = hand.Count - 1; i>=0; i--)
            {
                var refCard = hand[i];
                bool res = true;
                result.Clear();
                result.Add(refCard);
                for (int j = 1; j < 5 && res; j++) // it should be a while loop
                {
                    var containedRes = CardsContain(hand, new Card(new CardType(CardSuit.NOCARD, refCard.cardType.cardRank - j), false), 2);
                    res = containedRes.Item1;
                    if (containedRes.Item1)
                    {
                        result.Add(containedRes.Item2);
                    }
                }
                if (res)
                {
                    result.Reverse();
                    return true;
                }
            }
            return false;
        }

        public static bool CheckHighCard(List<Card> hand, ref List<Card> result)
        {
            result.Clear();
            var tempList = hand.OrderBy(p => p.cardType.cardRank).ToList();
            int idx = 0;
            CardRank rank = CardRank.NOCARD;
            for (int i = 0; i < hand.Count; i++)
            {
                if (rank < hand[i].cardType.cardRank)
                {
                    rank = hand[i].cardType.cardRank;
                    idx = i;
                }
            }
            result.Add(hand[idx]);
            return true;
        }

        public static PokerHandRanks checkPokerCombination(List<Card> cards, ref List<Card> result)
        {
            result.Clear();
            if (StatusCards.CheckRoyalFlush(cards, ref result))
            {
                return PokerHandRanks.ROYALFLUSH;
            } else if (StatusCards.CheckStraightFlush(cards, ref result))
            {
                return PokerHandRanks.STRAIGHTFLUSH;
            } else if (StatusCards.CheckFourOfAKind(cards, ref result))
            {
                return PokerHandRanks.FOUROFAKIND;
            }else if (StatusCards.CheckFullHouse(cards, ref result))
            {
                return PokerHandRanks.FULLHOUSE;
            }
            else if (StatusCards.CheckFlush(cards, ref result))
            {
                return PokerHandRanks.FLUSH;
            }
            else if (StatusCards.CheckStraight(cards, ref result))
            {
                return PokerHandRanks.STRAIGHT;
            }else if (StatusCards.CheckThreeOfAKind(cards, ref result))
            {
                return PokerHandRanks.THREEOFAKIND;
            }else if (StatusCards.CheckTwoPair(cards, ref result))
            {
                return PokerHandRanks.TWOPAIR;
            }else if (StatusCards.CheckPair(cards, ref result))
            {
                return PokerHandRanks.PAIR;
            }
            else
            {
                StatusCards.CheckHighCard(cards, ref result);
                return PokerHandRanks.HIGHCARD;
            }
        }

        public static CardRank FullHouseRankThreeOfKind(List<Card> list)
        {
            for(int i = 0; i<list.Count; i++)
            {
                if(CardNumberInList(list, list[i]) == 3)
                {
                    return list[i].cardType.cardRank;
                }
            }
            throw new PokerGameException("The given cards are not full house, can not find three of kind");
        }

        public static CardRank FullHouseRankPair(List<Card> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (CardNumberInList(list, list[i]) == 2)
                {
                    return list[i].cardType.cardRank;
                }
            }
            throw new PokerGameException("The given cards are not full house, can not find a pair");
        }

        public static CardRank TwoPairHigher(List<Card> list)
        {
            CardRank result = list[0].cardType.cardRank;
            for(int i = 1; i<list.Count; i++)
            {
                if(list[i].cardType.cardRank > result)
                {
                    result = list[i].cardType.cardRank;
                }
            }
            return result;
        }
        public static CardRank TwoPairLower(List<Card> list)
        {
            CardRank result = list[0].cardType.cardRank;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].cardType.cardRank < result)
                {
                    result = list[i].cardType.cardRank;
                }
            }
            return result;
        }

    }

    public class PokerGameException : Exception
    {
        public PokerGameException(string message) : base(message) { }
    }
}
