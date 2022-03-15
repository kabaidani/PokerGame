using System;
using System.Collections.Generic;

namespace PokerGame.Model
{
    public enum CardSuit
    {
        NOCARD,
        DIAMOND,
        CLUB,
        HEART,
        SPADE
    }

    public enum CardRank
    {
        NOCARD = 0,
        TWO = 2,
        THREE = 3,
        FOUR = 4,
        FIVE = 5,
        SIX = 6,
        SEVEN = 7,
        EIGHT = 8,
        NINE = 9,
        TEN = 10,
        JACK = 11,
        QUEEN = 12,
        KING = 13,
        ACE = 14
    }

    public enum CharacterTypes
    {
        BOB,
        DONALD,
        JOSEPH,
        KHAN,
        OLAF,
        WANDA
    }

    public struct Hand
    {
        public Card leftHand;
        public Card rightHand;

        public Hand(Card leftHand, Card rightHand)
        {
            this.leftHand = leftHand;
            this.rightHand = rightHand;
        }
    }

    public struct CardType
    {
        public CardType(CardSuit cardSuit, CardRank cardRank)
        {
            this.cardSuit = cardSuit;
            this.cardRank = cardRank;
        }
        public CardSuit cardSuit;
        public CardRank cardRank;
    }

    public struct Card
    {
        public Card(CardType cardType, bool isUpSideDown)
        {
            this.cardType = cardType;
            this.isUpSideDown = isUpSideDown;
        }

        public CardType cardType;
        public bool isUpSideDown;
    }

    public enum Action
    {
        FOLD,
        RAISE,
        CALL,
        CHECK,
        NOACTION
    }


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

        public Player(string staticName, CharacterTypes character, int money)
        {
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
            chosenAction = (Action)rand.Next(0, 4);
            LastAction = chosenAction;
            if (chosenAction == Action.FOLD)
            {
                this.hand.leftHand.cardType.cardRank = CardRank.NOCARD;
                this.hand.leftHand.cardType.cardSuit = CardSuit.NOCARD;
                this.LastAction = Action.FOLD;
                this.InGame = false;
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

    public class MiddleField
    {
        public List<Card> CommonityCards { private set; get; }
        public int CommonityBet { private set; get; }
        private int _comCardsNumber;

        public static Card CardGenerator(Random rand, bool isUpdsideDown = true)
        {
            int tmpCardSuit = rand.Next(1, 4);
            int tmpCardRank = rand.Next(2, 14);
            Card result = new Card(new CardType((CardSuit)tmpCardSuit, (CardRank)tmpCardRank), isUpdsideDown);
            return result;
        }

        public MiddleField()
        {
            _comCardsNumber = 0;
            CommonityCards = new List<Card>();
            Random rand = new Random(); // Check if this is more effective than construct every time
            for(int i = 0; i<5; i++)
            {
                if(i < 3)
                {
                    CommonityCards.Add(CardGenerator(rand, false));
                    _comCardsNumber++;
                }
                else
                {
                    CommonityCards.Add(CardGenerator(rand));
                }
            }
        }

        public void UnfoldNextCard()
        {
            if (_comCardsNumber == 5) return;
            CommonityCards[_comCardsNumber] = new Card(CommonityCards[_comCardsNumber].cardType, false);
            _comCardsNumber++;
        }

        public void CollectBets(List<Player> players)
        {
            int ammount = 0;
            foreach(var player in players)
            {
                ammount += player.CollectBet();
            }

            CommonityBet += ammount;
        }
    }
}
