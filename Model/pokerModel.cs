using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PokerGame.Model
{
    public class PokerModel
    {
        public event EventHandler<PlayersEventArg> CardAllocation;

        private void OnCardAllocation()
        {
            if (CardAllocation != null)
            {
                CardAllocation(this, new PlayersEventArg(playerContainer));
            }
        }

        public PokerModel(int playersNumber, int startingMoney = 2000)
        {
            if (playersNumber > 5 || playersNumber < 1) throw new ArgumentException();
            PlayersNum = playersNumber;
            StartingMoney = startingMoney;
        }

        public int StartingMoney { private get; set; }
        public int PlayersNum { private get; set; }
        public List<Player> playerContainer; 
        public MainPlayer p;

        //private Card CardGenerator(Random rand, bool isUpdsideDown = true)
        //{
        //    int tmpCardSuit = rand.Next(1, 4);
        //    int tmpCardRank = rand.Next(2,14);
        //    Card result = new Card(new CardType((CardSuit)tmpCardSuit, (CardRank)tmpCardRank), isUpdsideDown);
        //    return result;
        //}

        public void GeneratePlayers()
        {
            playerContainer = new List<Player>();
            Random rand = new Random();

            p = new MainPlayer("Daniel", CharacterTypes.BOB, StartingMoney);

            for(int i = 0; i < PlayersNum; i++)
            {
                int tempCharacterType = rand.Next(0, 5);
                CharacterTypes character = (CharacterTypes)tempCharacterType;
                playerContainer.Add(new BotPlayer(character, StartingMoney));
                
            }

            playerContainer.Add(p);

        }

        public void StartRound()
        {
            foreach (var player in playerContainer)
            {
                Random rand = new Random(); // look for if it is worth to create every time
                player.hand.leftHand = MiddleField.CardGenerator(rand, false);
                player.hand.rightHand = MiddleField.CardGenerator(rand, false);
            }
            playerContainer[playerContainer.Count - 1].hand.leftHand.isUpSideDown = false;
            playerContainer[playerContainer.Count - 1].hand.rightHand.isUpSideDown = false;

            OnCardAllocation();


            //foreach (var player in playerContainer)
            //{
            //    Random rand = new Random(); // look for if it is worth to create every time
            //    player.hand.rightHand = CardGenerator(rand, true);
            //}
            //playerContainer[playerContainer.Count - 1].hand.rightHand.isUpSideDown = false;
            //OnCardAllocation();

        }

    }
}
