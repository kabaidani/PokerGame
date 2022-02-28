using System;

namespace PokerGame.Model
{
    public enum CardSuit
    {
        DIAMOND,
        CLUB,
        HEART,
        SPADE,
        NOCARD
    }

    public enum CardRank
    {
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
        ACE = 14,
        NOCARD = 15
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

    public struct CardType
    {
        public CardType(CardSuit cardSuit, CardRank cardRank)
        {
            this.cardSuit = cardSuit;
            this.cardRank = cardRank;
        }
        public CardSuit cardSuit; // Consider to use Proprtys
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
        public Tuple<Card, Card> Hand { protected set; get; }

        public Player(string staticName, CharacterTypes character, int money, Tuple<Card, Card> hand)
        {
            //PlayerName = playerName;
            StaticName = staticName;
            Character = character;
            BetChips = 0;
            Money = money;
            LastAction = Action.NOACTION;
            InGame = true;
            Hand = hand;
        }

        public abstract void ChosenAction(Action chosenAction, int betValue = 0);
    }

    public class BotPlayer : Player
    {
        private static int _characterCounter = 1;
        public BotPlayer(CharacterTypes character, int money, Tuple<Card, Card> hand) : base("Character" + _characterCounter.ToString(), character, money, hand)
        {
            _characterCounter++;
        }

        public override void ChosenAction(Action chosenAction, int betValue = 0)
        {
            
        }
    }

    public class MainPlayer : Player
    {
        public string PlayerName { private set; get; }

        public MainPlayer(string playerName, CharacterTypes character, int money, Tuple<Card, Card> hand) : base("MainPlayer", character, money, hand)
        {
            PlayerName = playerName;
        }

        public override void ChosenAction(Action chosenAction, int betValue = 0)
        {
            if (chosenAction == Action.FOLD)
            {
                //Event for fold
            } else if (chosenAction == Action.CALL)
            {
                Money -= betValue;
                //Event for call
            } else if (chosenAction == Action.CHECK)
            {
                //Event for check
            } else if (chosenAction == Action.RAISE)
            {
                Money -= betValue;
                //Event for raise
            }
        }

    }
}
