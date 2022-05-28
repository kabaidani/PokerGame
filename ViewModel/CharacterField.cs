using PokerGame.Model;

namespace PokerGame.ViewModel
{
    public class CharacterField : ViewModelBase
    {
        private CharacterTypes _character;
        private bool _signed = false;
        public bool Signed { 
            get { return _signed; } 
            set
            {
                if (value != _signed)
                {
                    _signed = value;
                    OnPropertyChanged("CharacterUrl");
                }
            }
        }
        public string CharacterUrl
        {
            get
            {
                if(!_signed) return "../../Image/" + _character.ToString() + ".png";
                else return "../../Image/SignedCharacters/" + _character.ToString() + ".png";
            }

        }
        public CharacterTypes Character
        {
            get { return _character; }
            set
            {
                if(_character != value)
                {
                    _character = value;
                    OnPropertyChanged("Character");
                }
            }
        }

        public void PropertyChange()
        {
            OnPropertyChanged("CharacterUrl");
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Number { get; set; }
        public DelegateCommand SelectCommand { set; get; }
    }
}
