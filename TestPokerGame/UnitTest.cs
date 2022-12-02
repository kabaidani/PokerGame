using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGame;
using PokerGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerModelUnitTests
{
    [TestClass]
    public class TestPokerCombinations
    {
        private PokerModel _model = new PokerModel(5);
        [TestMethod]
        public void RoyalFlushTestMethod()
        {
            List<Card> cards = new List<Card>();
            List<Card> tmp = new List<Card>();
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.EIGHT), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.KING), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.QUEEN), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            Assert.IsTrue(StatusCards.CheckRoyalFlush(cards, ref tmp));
            int idx = 0;
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.TEN));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.JACK));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.QUEEN));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.KING));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.ACE));
        }
        [TestMethod]
        public void FourOfAKindTestMethod()
        {
            List<Card> cards = new List<Card>();
            List<Card> tmp = new List<Card>();
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.JACK), false));
            cards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.QUEEN), false));
            cards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            Assert.IsTrue(StatusCards.CheckFourOfAKind(cards, ref tmp));

            Assert.AreEqual(tmp.Count, 4);
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false)));

            List<Card> cards2 = new List<Card>();
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.QUEEN), false));
            cards2.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false));
            Assert.IsFalse(StatusCards.CheckFourOfAKind(cards2, ref tmp));
        }

        [TestMethod]
        public void FullHouseTestMethod()
        {
            List<Card> cards = new List<Card>();
            List<Card> tmp = new List<Card>();
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.QUEEN), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.JACK), false));
            cards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.QUEEN), false));
            cards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            Assert.IsTrue(StatusCards.CheckFullHouse(cards, ref tmp));

            Assert.IsTrue(tmp.Count >= 5);
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.DIAMOND, CardRank.QUEEN), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.SPADE, CardRank.QUEEN), false)));


            List<Card> cards2 = new List<Card>();
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.QUEEN), false));
            cards2.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false));
            Assert.IsFalse(StatusCards.CheckFullHouse(cards2, ref tmp));
        }

        [TestMethod]
        public void FlushTestMethod()
        {
            List<Card> cards = new List<Card>();
            List<Card> tmp = new List<Card>();
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.QUEEN), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            Assert.IsTrue(StatusCards.CheckFlush(cards, ref tmp));

            Assert.IsTrue(tmp.Count >= 5);
            int idx = 0;
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.TWO));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.THREE));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.FIVE));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.JACK));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.QUEEN));

            List<Card> cards2 = new List<Card>();
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.QUEEN), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            Assert.IsFalse(StatusCards.CheckFlush(cards2, ref tmp));
        }

        [TestMethod]
        public void StraightFlushTestMethod()
        {
            List<Card> cards = new List<Card>();
            List<Card> tmp = new List<Card>();
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            Assert.IsTrue(StatusCards.CheckStraightFlush(cards, ref tmp));
            Assert.IsTrue(tmp.Count == 5);

            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false)));

            List<Card> cards2 = new List<Card>();
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.SIX), false));
            Assert.IsFalse(StatusCards.CheckStraightFlush(cards2, ref tmp));
        }

        [TestMethod]
        public void StraightTestMethod()
        {
            List<Card> cards = new List<Card>();
            List<Card> tmp = new List<Card>();
            cards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.SIX), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TEN), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            Assert.IsTrue(StatusCards.CheckStraight(cards, ref tmp));

            Assert.IsTrue(tmp.Count >= 5);
            int idx = 0;
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.FOUR));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.HEART, CardRank.FIVE));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.SPADE, CardRank.SIX));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.SEVEN));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.DIAMOND, CardRank.EIGHT));

            List<Card> cards2 = new List<Card>();
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.QUEEN), false));
            Assert.IsFalse(StatusCards.CheckStraight(cards2, ref tmp));
        }

        [TestMethod]
        public void ThreeOfAKindTestMethod()
        {
            List<Card> cards = new List<Card>();
            List<Card> tmp = new List<Card>();
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.SIX), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.SIX), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            Assert.IsTrue(StatusCards.CheckThreeOfAKind(cards, ref tmp));

            Assert.IsTrue(tmp.Count == 3);
            int idx = 0;
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.CLUB, CardRank.SIX));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.DIAMOND, CardRank.SIX));
            Assert.AreEqual(tmp[idx++].cardType, new CardType(CardSuit.HEART, CardRank.SIX));

            List<Card> cards2 = new List<Card>();
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.QUEEN), false));
            Assert.IsFalse(StatusCards.CheckThreeOfAKind(cards2, ref tmp));
        }

        [TestMethod]
        public void TwoPairTestMethod()
        {
            List<Card> cards = new List<Card>();
            List<Card> tmp = new List<Card>();
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.EIGHT), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.QUEEN), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.SIX), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            Assert.IsTrue(StatusCards.CheckTwoPair(cards, ref tmp));

            Assert.IsTrue(tmp.Count == 4);
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.HEART, CardRank.EIGHT), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.DIAMOND, CardRank.SIX), false)));


            List<Card> cards2 = new List<Card>();
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.QUEEN), false));
            Assert.IsFalse(StatusCards.CheckTwoPair(cards2, ref tmp));
        }

        [TestMethod]
        public void PairTestMethod()
        {
            List<Card> cards = new List<Card>();
            List<Card> tmp = new List<Card>();
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.EIGHT), false));
            cards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            cards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.SIX), false));
            cards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.QUEEN), false));
            cards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            Assert.IsTrue(StatusCards.CheckPair(cards, ref tmp));

            Assert.IsTrue(tmp.Count == 2);
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false)));
            Assert.IsTrue(tmp.Contains(new Card(new CardType(CardSuit.SPADE, CardRank.SIX), false)));

            List<Card> cards2 = new List<Card>();
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            cards2.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            cards2.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            cards2.Add(new Card(new CardType(CardSuit.HEART, CardRank.EIGHT), false));
            Assert.IsFalse(StatusCards.CheckPair(cards2, ref tmp));
        }


    }

    [TestClass]
    public class TestPokerCombinationsOrder
    {
        private PokerModel _model = new PokerModel(5);
        private MiddleField _middleSection = new MiddleField(new Deck(), new List<Player>());
        private Player p1 = new MainPlayer("player1", CharacterTypes.BOB, 2000);
        private Player p2 = new MainPlayer("player2", CharacterTypes.DONALD, 2000);


        [TestMethod]
        public void StraightFlush()
        {
            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.EIGHT), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) >= 1);



            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.EIGHT), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.KING), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);


            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.EIGHT), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);


            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.EIGHT), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) < 0);

        }


        [TestMethod]
        public void FourOfKind()
        {
            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) >= 1);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.KING), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) <= -1);


            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);
        }

        [TestMethod]
        public void FullHouse()
        {
            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.THREE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.FOUR), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.SEVEN), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) < 0);


            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.THREE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.EIGHT), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.THREE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.THREE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);

            _middleSection.CommonityCards.Clear(); //The counting depends here also
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.ACE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);
        }


        [TestMethod]
        public void Straight()
        {
            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.SIX), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.NINE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.SIX), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.KING), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.SIX), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.NINE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.KING), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.KING), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);

            _middleSection.CommonityCards.Clear(); // need to check the order
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.SIX), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.KING), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.KING), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);
        }

        [TestMethod]
        public void Flush()
        {
            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.SEVEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.KING), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.SEVEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.KING), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.SEVEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.KING), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.JACK), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);


            _middleSection.CommonityCards.Clear(); // The order
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.SEVEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);
        }

        [TestMethod]
        public void ThreeOfKind()
        {
            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.KING), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.FOUR), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.SIX), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.KING), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.SEVEN), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.EIGHT), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);


            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.TWO), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.THREE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.SEVEN), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.EIGHT), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) <= -1);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.KING), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.KING), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.KING), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear(); // Order
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.SIX), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.SIX), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.NINE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) < 0);
        }


        [TestMethod]
        public void TwoPair()
        {
            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.KING), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.KING), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.KING), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.KING), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.FIVE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.NINE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) < 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.FIVE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.JACK), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.THREE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.JACK), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) < 0);
        }

        [TestMethod]
        public void Pair()
        {
            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.KING), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.FOUR), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.THREE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.THREE), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.THREE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) < 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SEVEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);
        }

        [TestMethod]
        public void HighCard()
        {
            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) < 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.EIGHT), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.FIVE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.THREE), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.TWO), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.FOUR), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) == 0);

            _middleSection.CommonityCards.Clear();
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.JACK), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.HEART, CardRank.EIGHT), false));
            _middleSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.FIVE), false));
            p1.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.SIX), false);
            p1.hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);
            p2.hand.leftHand = new Card(new CardType(CardSuit.SPADE, CardRank.FOUR), false);
            p2.hand.rightHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false);
            Assert.IsTrue(p1.CompareTo(p2, _middleSection.CommonityCards) > 0);
        }
    }

    [TestClass]
    public class UnitTestingPrizeDistribution
    {
        private MiddleField _middleFieldSection;
        private List<Player> _players;
        private Deck _deck = new Deck();

        private void CheckWinnerMethod(List<Player> playerContainer, MiddleField middleFieldSection)
        {
            List<Player> sortedwinners = new List<Player>();
            foreach (var player in playerContainer) sortedwinners.Add(player);
            sortedwinners.Sort((p1, p2) => p1.CompareTo(p2, middleFieldSection.CommonityCards));
            sortedwinners.Reverse();
            var realwinners = sortedwinners.Where(p => sortedwinners[0].CompareTo(p, middleFieldSection.CommonityCards) == 0).Select(p => p).ToList();
            //int winningPrice = MiddleFieldSection.CommonityBet / winners.Count;
            while (middleFieldSection.PrizeDistribution(realwinners))
            {
                foreach (var player in realwinners)
                {
                    sortedwinners.Remove(player);
                }
                sortedwinners.Sort((p1, p2) => p1.CompareTo(p2, middleFieldSection.CommonityCards));
                sortedwinners.Reverse();
                realwinners = sortedwinners.Where(p => sortedwinners[0].CompareTo(p, middleFieldSection.CommonityCards) == 0).Select(p => p).ToList();

            }
        }


        [TestMethod]
        public void NormalCaseOneClearWinner()
        {
            _players = new List<Player>();
            //Palyer A
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[0].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[0].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player B
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[1].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[1].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player C
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[2].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[2].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player D
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[3].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[3].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            //Player E
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[4].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[4].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            _middleFieldSection = new MiddleField(_deck, _players);
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TEN), false));

            //Licit
            int licitBet = 500;
            int tempValue = 0;
            List<Tuple<PokerGame.Model.Action, int>> tempList = new List<Tuple<PokerGame.Model.Action, int>>();
            _players[0].PlayerAction(ref licitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[1].PlayerAction(ref licitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[2].PlayerAction(ref licitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[4].PlayerAction(ref licitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[3].PlayerAction(ref licitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);



            _middleFieldSection.CollectBets(_players);

            Assert.AreEqual(2500, _middleFieldSection.CommonityBet);
            CheckWinnerMethod(_players, _middleFieldSection);
            Assert.AreEqual(_players[0].Money, 2500);
            Assert.AreEqual(_players[1].Money, 0);
            Assert.AreEqual(_players[2].Money, 0);
            Assert.AreEqual(_players[3].Money, 0);
            Assert.AreEqual(_players[4].Money, 0);
        }


        [TestMethod]
        public void TwoWinnerOneHasLessMoneyInBet()
        {
            _players = new List<Player>();

            //Palyer A
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 200)); //Winner
            _players[0].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[0].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player B
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500)); //Winner
            _players[1].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[1].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player C
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[2].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[2].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player D
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[3].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[3].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            //Player E
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[4].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[4].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            _middleFieldSection = new MiddleField(_deck, _players);
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TEN), false));

            //Licit
            int floorLicitBet = 200;
            int ceilingLicitBet = 500;
            int tempValue = 0;
            List<Tuple<PokerGame.Model.Action, int>> tempList = new List<Tuple<PokerGame.Model.Action, int>>();
            _players[0].PlayerAction(ref floorLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[1].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[2].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[4].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[3].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);


            _middleFieldSection.CollectBets(_players);

            Assert.AreEqual(2200, _middleFieldSection.CommonityBet);
            CheckWinnerMethod(_players, _middleFieldSection);
            Assert.AreEqual(_players[0].Money, 440); //440
            Assert.AreEqual(_players[1].Money, 1760);
            Assert.AreEqual(_players[2].Money, 0);
            Assert.AreEqual(_players[3].Money, 0);
            Assert.AreEqual(_players[4].Money, 0);
        }


        [TestMethod]
        public void ThreeWinnerOneHasLessMoneyInBet()
        {
            _players = new List<Player>();
            //    Palyer A
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 200)); //Winner
            _players[0].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[0].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //    Player B
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500)); //Winner
            _players[1].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[1].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //    Player C
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500)); //Winner
            _players[2].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[2].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //    Player D
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[3].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[3].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            //    Player E
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[4].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[4].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            _middleFieldSection = new MiddleField(_deck, _players);
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TEN), false));

            //    Licit
            int floorLicitBet = 200;
            int ceilingLicitBet = 500;
            int tempValue = 0;
            List<Tuple<PokerGame.Model.Action, int>> tempList = new List<Tuple<PokerGame.Model.Action, int>>();
            _players[0].PlayerAction(ref floorLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[1].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[2].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[4].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[3].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);


            _middleFieldSection.CollectBets(_players);

            Assert.AreEqual(2200, _middleFieldSection.CommonityBet);
            CheckWinnerMethod(_players, _middleFieldSection);
            Assert.AreEqual(_players[0].Money, 293);
            Assert.AreEqual(_players[1].Money, 953);
            Assert.AreEqual(_players[2].Money, 953);
            Assert.AreEqual(_players[3].Money, 0);
            Assert.AreEqual(_players[4].Money, 0);
        }

        [TestMethod]
        public void ThreeWinnerTwoHasSameLessMoneyInBet()
        {
            _players = new List<Player>();
            //Palyer A
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 200)); //Winner
            _players[0].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[0].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player B
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 200)); //Winner
            _players[1].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[1].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player C
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500)); //Winner
            _players[2].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[2].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player D
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[3].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[3].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            //Player E
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[4].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[4].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            _middleFieldSection = new MiddleField(_deck, _players);
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TEN), false));

            //Licit
            int floorLicitBet = 200;
            int ceilingLicitBet = 500;
            int tempValue = 0;
            List<Tuple<PokerGame.Model.Action, int>> tempList = new List<Tuple<PokerGame.Model.Action, int>>();
            _players[0].PlayerAction(ref floorLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[1].PlayerAction(ref floorLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[2].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[4].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[3].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);


            _middleFieldSection.CollectBets(_players);

            Assert.AreEqual(1900, _middleFieldSection.CommonityBet);
            CheckWinnerMethod(_players, _middleFieldSection);
            Assert.AreEqual(_players[0].Money, 253);
            Assert.AreEqual(_players[1].Money, 253);
            Assert.AreEqual(_players[2].Money, 1394);
            Assert.AreEqual(_players[3].Money, 0);
            Assert.AreEqual(_players[4].Money, 0);
        }

        [TestMethod]
        public void ThreeWinnerTwoHasDifferentLessMoneyInBet()
        {
            _players = new List<Player>();
            //Palyer A
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 200)); //Winner
            _players[0].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[0].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player B
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 100)); //Winner
            _players[1].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[1].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player C
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 600)); //Winner
            _players[2].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[2].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player D
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 600));
            _players[3].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[3].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            //Player E
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 600));
            _players[4].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[4].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            _middleFieldSection = new MiddleField(_deck, _players);
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TEN), false));

            //Licit
            int floorLicitBet1 = 100;
            int floorLicitBet2 = 200;
            int ceilingLicitBet = 600;

            int tempValue = 0;
            List<Tuple<PokerGame.Model.Action, int>> tempList = new List<Tuple<PokerGame.Model.Action, int>>();
            _players[0].PlayerAction(ref floorLicitBet2, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[1].PlayerAction(ref floorLicitBet1, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[2].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[4].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[3].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);

            _middleFieldSection.CollectBets(_players);

            Assert.AreEqual(2100, _middleFieldSection.CommonityBet);
            CheckWinnerMethod(_players, _middleFieldSection);
            Assert.AreEqual(_players[0].Money, 233);
            Assert.AreEqual(_players[1].Money, 116);
            Assert.AreEqual(_players[2].Money, 1751);
            Assert.AreEqual(_players[3].Money, 0);
            Assert.AreEqual(_players[4].Money, 0);
        }

        [TestMethod]
        public void OneClearWinnerWithLessMoneyInBet()
        {
            _players = new List<Player>();
            //Palyer A
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 200)); //Winner less bet
            _players[0].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[0].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);

            //Player B
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500)); //2nd "Winner" normal bet
            _players[1].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[1].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player C
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[2].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[2].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player D
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[3].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[3].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            //Player E
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[4].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[4].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            _middleFieldSection = new MiddleField(_deck, _players);
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TEN), false));

            //Licit
            int floorLicitBet = 200;
            int ceilingLicitBet = 500;
            int tempValue = 0;
            List<Tuple<PokerGame.Model.Action, int>> tempList = new List<Tuple<PokerGame.Model.Action, int>>();
            _players[0].PlayerAction(ref floorLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[1].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[2].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[4].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[3].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);


            _middleFieldSection.CollectBets(_players);

            Assert.AreEqual(2200, _middleFieldSection.CommonityBet);
            CheckWinnerMethod(_players, _middleFieldSection);
            Assert.AreEqual(_players[0].Money, 880);
            Assert.AreEqual(_players[1].Money, 1320);
            Assert.AreEqual(_players[2].Money, 0);
            Assert.AreEqual(_players[3].Money, 0);
            Assert.AreEqual(_players[4].Money, 0);
        }


        [TestMethod]
        public void OneClearWinnerWithLessMoneyInBetThenSameCaseTheSecondOne()
        {
            _players = new List<Player>();
            //Palyer A
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 100)); //Winner less bet
            _players[0].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[0].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.ACE), false);

            //Player B
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 200)); //2nd "Winner" less bet
            _players[1].hand.leftHand = new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false);
            _players[1].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player C
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[2].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[2].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);

            //Player D
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[3].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[3].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            //Player E
            _players.Add(new BotPlayer(CharacterTypes.OLAF, 500));
            _players[4].hand.leftHand = new Card(new CardType(CardSuit.HEART, CardRank.JACK), false);
            _players[4].hand.rightHand = new Card(new CardType(CardSuit.CLUB, CardRank.THREE), false);


            _middleFieldSection = new MiddleField(_deck, _players);
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false));
            _middleFieldSection.CommonityCards.Add(new Card(new CardType(CardSuit.DIAMOND, CardRank.TEN), false));

            //Licit
            int floorLicitBet1 = 100;
            int floorLicitBet2 = 200;
            int ceilingLicitBet = 500;
            int tempValue = 0;
            List<Tuple<PokerGame.Model.Action, int>> tempList = new List<Tuple<PokerGame.Model.Action, int>>();
            _players[0].PlayerAction(ref floorLicitBet1, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[1].PlayerAction(ref floorLicitBet2, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[2].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[4].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);
            _players[3].PlayerAction(ref ceilingLicitBet, ref tempValue, tempValue, tempValue, ref tempList, PokerGame.Model.Action.CALL);


            _middleFieldSection.CollectBets(_players);

            Assert.AreEqual(1800, _middleFieldSection.CommonityBet);
            CheckWinnerMethod(_players, _middleFieldSection);
            Assert.AreEqual(_players[0].Money, 360);
            Assert.AreEqual(_players[1].Money, 576);
            Assert.AreEqual(_players[2].Money, 288);
            Assert.AreEqual(_players[3].Money, 288);
            Assert.AreEqual(_players[4].Money, 288);
        }

    }

    [TestClass]
    public class UnitTestingPokerRangsProbability
    {
        [TestMethod]
        public void UnitTestCheckProbablityOfRoyalFlush()
        {
            //Royal flush
            int result = StatusCards.CheckProbablityOfRoyalFlush(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.TEN), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.QUEEN), false),
                });
            Assert.AreEqual(0, result);


            //1 card left to royal flush
            result = StatusCards.CheckProbablityOfRoyalFlush(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.TEN), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.QUEEN), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbablityOfRoyalFlush(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.TEN), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.QUEEN), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.KING), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.JACK), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbablityOfRoyalFlush(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.ACE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.TEN), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.KING), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.TWO), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.JACK), false),
                });
            Assert.AreEqual(1, result);

            //Two or more cards missing for RF
            result = StatusCards.CheckProbablityOfRoyalFlush(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.TEN), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.QUEEN), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.TWO), false),
                });
            Assert.AreEqual(-1, result);
        }


        [TestMethod]
        public void UnitTestCheckProbablityOfStraightFlush()
        {
            int result = StatusCards.CheckProbabilityOfStraightFlush(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.TEN), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.TWO), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.SEVEN), false),
                });
            Assert.AreEqual(0, result);

            result = StatusCards.CheckProbabilityOfStraightFlush(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.TEN), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.TWO), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.SEVEN), false),
                });
            Assert.AreEqual(1, result);


            result = StatusCards.CheckProbabilityOfStraightFlush(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.SEVEN), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.TWO), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.ACE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.TEN), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfStraightFlush(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.TEN), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.SEVEN), false),
                });
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void UnitTestCheckProbablityOfFourOfKind()
        {
            int result = StatusCards.CheckProbabilityOfFourOfKind(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.SEVEN), false),
                });
            Assert.AreEqual(0, result);

            result = StatusCards.CheckProbabilityOfFourOfKind(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.SEVEN), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfFourOfKind(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.SEVEN), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfFourOfKind(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.NINE), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.SEVEN), false),
                });
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void UnitTestCheckProbablityOfFullHouse()
        {
            int result = StatusCards.CheckProbabilityOfFullHouse(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.QUEEN), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.QUEEN), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.QUEEN), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.SIX), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfFullHouse(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.QUEEN), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.QUEEN), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.QUEEN), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.SIX), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.NINE), false),
                });
            Assert.AreEqual(1, result);


            result = StatusCards.CheckProbabilityOfFullHouse(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.ACE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfFullHouse(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.ACE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.NINE), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfFullHouse(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.KING), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.ACE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.FOUR), false),
                });
            Assert.AreEqual(-1, result);

            result = StatusCards.CheckProbabilityOfFullHouse(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.KING), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.KING), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.FOUR), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfFullHouse(
                new List<Card> {
                        new Card(new CardType(CardSuit.HEART, CardRank.KING), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.SIX), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                });
            Assert.AreEqual(0, result);
        }


        [TestMethod]
        public void UnitTestCheckProbablityOfFlush()
        {
            int result = StatusCards.CheckProbabilityOfFlush(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.QUEEN), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.SEVEN), false),
                });
            Assert.AreEqual(0, result);

            result = StatusCards.CheckProbabilityOfFlush(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.QUEEN), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.SEVEN), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfFlush(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.QUEEN), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.THREE), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.SEVEN), false),
                });
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void UnitTestCheckProbablityOfStraight()
        {
            int result = StatusCards.CheckProbabilityOfStraight(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.SEVEN), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.FOUR), false),
                });
            Assert.AreEqual(0, result);

            result = StatusCards.CheckProbabilityOfStraight(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.THREE), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.ACE), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false),
                });
            Assert.AreEqual(0, result);

            result = StatusCards.CheckProbabilityOfStraight(
                 new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.TWO), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.THREE), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false),
                 });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfStraight(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.FOUR), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.SIX), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfStraight(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.FOUR), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.SEVEN), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.TEN), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfStraight(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.THREE), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.FOUR), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.SIX), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.SEVEN), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.NINE), false),
                });
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UnitTestCheckProbablityOfThreeOfKind()
        {
            int result = StatusCards.CheckProbabilityOfThreeOfKind(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.KING), false),
                });
            Assert.AreEqual(0, result);

            result = StatusCards.CheckProbabilityOfThreeOfKind(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.KING), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfThreeOfKind(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.KING), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfThreeOfKind(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FOUR), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.FOUR), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.KING), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.FIVE), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfThreeOfKind(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.EIGHT), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfThreeOfKind(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.CLUB, CardRank.TEN), false),
                });
            Assert.AreEqual(-1, result);
        }


        [TestMethod]
        public void UnitTestCheckProbablityOfTwoPair()
        {
            int result = StatusCards.CheckProbabilityOfTwoPair(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false),
                });
            Assert.AreEqual(0, result);

            result = StatusCards.CheckProbabilityOfTwoPair(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.KING), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfTwoPair(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.KING), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.KING), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false),
                });
            Assert.AreEqual(1, result);

            result = StatusCards.CheckProbabilityOfTwoPair(
                new List<Card> {
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.JACK), false),
                        new Card(new CardType(CardSuit.DIAMOND, CardRank.FIVE), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.EIGHT), false),
                        new Card(new CardType(CardSuit.HEART, CardRank.KING), false),
                        new Card(new CardType(CardSuit.SPADE, CardRank.TWO), false),
                });
            Assert.AreEqual(-1, result);
        }

    }
}
