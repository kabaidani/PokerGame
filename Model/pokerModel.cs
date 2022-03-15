using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PokerGame.Model
{
    public class ActionEvenArgs : EventArgs
    {
        public Player Player { get; private set; }

        public ActionEvenArgs(Player player)
        {
            Player = player;
        }
    }

    public class PokerModel
    {
        public event EventHandler<PlayersEventArg> CardAllocation;
        public event EventHandler<CommonityCardsEventArgs> UnfoldCardEvent;
        public event EventHandler<ActionEvenArgs> PlayerActionEvent;
        public event EventHandler<PokerPlayerEventArgs> SignPlayerEvent;

        private int _actualLicitBet = 100;
        public int getActualLicitBet() { return _actualLicitBet; }

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
            _middleFieldSection = new MiddleField();
        }

        public int StartingMoney { private get; set; } // Not sure if it is okey
        public int PlayersNum { private get; set; }
        public List<Player> playerContainer; 
        public MainPlayer p;


        public MiddleField _middleFieldSection; // need to be private but needs to handle the events


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
            _actualLicitBet = 200;
            foreach (var player in playerContainer)
            {
                Random rand = new Random(); // look for if it is worth to create every time
                player.hand.leftHand = MiddleField.CardGenerator(rand);
                player.hand.rightHand = MiddleField.CardGenerator(rand);
            }
            //playerContainer[playerContainer.Count - 1].hand.leftHand.isUpSideDown = false;
            //playerContainer[playerContainer.Count - 1].hand.rightHand.isUpSideDown = false;
            OnCardAllocation();
        }

        public void TestUnFoldMiddleCards()
        {
            _middleFieldSection.UnfoldNextCard();
            OnUnfoldCardEvent(_middleFieldSection.CommonityCards);
        }
        

        public void MainPlayerAction(Action action)
        {
            p.PlayerAction(ref _actualLicitBet, action);
            OnPlayerActionEvent(p);
        }

        public async void AsyncFirstRound()
        {
            StartRound();
            await Task.Delay(200 * 12);
            for(int i = 0; i<playerContainer.Count-1; i++)
            {
                OnSignPlayerEvent(playerContainer[i]);
                await Task.Delay(1000);
                playerContainer[i].PlayerAction(ref _actualLicitBet);
                OnPlayerActionEvent(playerContainer[i]);
                OnSignPlayerEvent(playerContainer[i]);
            }


        }

    }
}
