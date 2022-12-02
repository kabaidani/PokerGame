using PokerGame.Model;
using System;
using System.Collections.ObjectModel;

namespace PokerGame.ViewModel
{
    public class CharacterSelecterViewModel : ViewModelBase
    {  
        private bool _startGameButtonAvaible;
        private int _startingMoney;

        
        public DelegateCommand StartGameCommand { get; set; }
        public ObservableCollection<CharacterField> Fields { get; set; }
        public bool StartGameButtonAvaible
        {
            get { return _startGameButtonAvaible; }
            set
            {
                if(_startGameButtonAvaible != value)
                {
                    _startGameButtonAvaible = value;
                    OnPropertyChanged("StartGameButtonAvaible");
                }
            }
        }
        public CharacterTypes SelectedCharacter { get; private set; }
        public string StartingMoney
        {
            get
            {
                return _startingMoney.ToString();
            }
            set
            {
                if(value != _startingMoney.ToString())
                {
                    int stmoney = 0;
                    try
                    {
                        stmoney = Int32.Parse(value);
                    }
                    catch (FormatException)
                    {
                        OnPropertyChanged("StartingMoney"); // Propably it is handled in the xaml.cs
                        return;
                    }
                    if(stmoney < 1000 || stmoney > 5000)
                    {
                        OnPropertyChanged("StartingMoney"); //Should drop a message window
                        return;
                    }
                    _startingMoney = stmoney;
                    OnPropertyChanged("StartingMoney");

                }
            }
        }


        public event EventHandler StartGameEvent;
        

        public CharacterSelecterViewModel(int gridSize)
        {
            _startingMoney = 1000;
            _startGameButtonAvaible = false;
            StartGameCommand = new DelegateCommand(p => StartGame());
            Fields = new ObservableCollection<CharacterField>();
            int counter = 0;
            for(int i = 0; i<3; i++)
            {
                for(int j = 0; j<4; j++)
                {
                    Fields.Add(new CharacterField
                    {
                        Character = (CharacterTypes)(counter++),
                        X = i,
                        Y = j,
                        Number = i * gridSize + j,
                        SelectCommand = new DelegateCommand(param => SelectCharacter(System.Convert.ToInt32(param)))

                    });
                }
            }
        }


        private void SelectCharacter(int index)
        {
            StartGameButtonAvaible = true;
            foreach (var p in Fields) p.Signed = false;
            Fields[index].Signed = true;
        }

        private void OnStartGameEvent()
        {
            if(StartGameEvent != null)
            {
                StartGameEvent(this, EventArgs.Empty);
            }
        }

        private CharacterTypes GetSelectedCharacter()
        {
            foreach(var p in Fields)
            {
                if (p.Signed) return p.Character;
            }
            throw new PokerGameException("No character had been selected");
        }

        private void StartGame()
        {
            SelectedCharacter = GetSelectedCharacter(); //should handler the exception
            OnStartGameEvent();
        }

    }
}
