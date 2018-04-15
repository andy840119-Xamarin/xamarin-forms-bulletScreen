using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BulletScreen.Model
{
    /// <summary>
    ///     Define each of character
    /// </summary>
    public class FuriganaText : INotifyPropertyChanged
    {
        private bool _changeNewLine;
        private string _character;
        private string _furigana;
        private string _romaji;
        private Color? _textColor;

        /// <summary>
        ///     Ctor
        /// </summary>
        public FuriganaText()
        {
        }

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="character"></param>
        /// <param name="furigana"></param>
        /// <param name="romaji"></param>
        public FuriganaText(string character, string furigana = "", string romaji = "")
        {
            Character = character;
            Furigana = furigana;
            Romaji = romaji;
        }

        /// <summary>
        ///     Furigana
        /// </summary>
        public string Furigana
        {
            get => _furigana;
            set
            {
                _furigana = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Character
        /// </summary>
        public string Character
        {
            get => _character;
            set
            {
                _character = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Romaji
        /// </summary>
        public string Romaji
        {
            get => _romaji;
            set
            {
                _romaji = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Text color
        /// </summary>
        public Color? TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Change new line
        /// </summary>
        public bool ChangeNewLine
        {
            get => _changeNewLine;
            set
            {
                _changeNewLine = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}