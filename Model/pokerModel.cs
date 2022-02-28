using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace PokerGame.Model
{
    public class PokerModel
    {

        public event EventHandler CardAllocation;

        private void OnCardAllocation()
        {
            if (CardAllocation != null)
            {
                CardAllocation(this, EventArgs.Empty);
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

        private Card CardGenerator(Random rand, bool isUpdsideDown = true)
        {
            int tmpCardSuit = rand.Next(0, 3);
            int tmpCardRank = rand.Next(2,14);
            Card result = new Card(new CardType((CardSuit)tmpCardSuit, (CardRank)tmpCardRank), isUpdsideDown);
            return result;
        }

        public void GeneratePlayers()
        {
            playerContainer = new List<Player>();
            Random rand = new Random();

            var cardLHS = CardGenerator(rand, false);
            var cardRHS = CardGenerator(rand, false);

            p = new MainPlayer("Daniel", CharacterTypes.BOB, StartingMoney, new Tuple<Card, Card>(cardLHS, cardRHS));

            for(int i = 0; i < PlayersNum; i++)
            {
                int tempCharacterType = rand.Next(0, 5);
                CharacterTypes character = (CharacterTypes)tempCharacterType;

                cardLHS = CardGenerator(rand, false);
                cardRHS = CardGenerator(rand, false);
                playerContainer.Add(new BotPlayer(character, StartingMoney, new Tuple<Card, Card>(cardLHS, cardRHS)));
                
            }

            playerContainer.Add(p);
            
        }

        public async void AsyncStartRound()
        {
            await Task.Delay(3000);
            OnCardAllocation();
        }
    }
}
