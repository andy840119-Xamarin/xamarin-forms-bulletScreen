using System;
using System.Runtime.CompilerServices;
using BulletScreen.Extension;
using BulletScreen.Helper;
using BulletScreen.Model;
using Xamarin.Forms;

namespace BulletScreen.Controls
{
    /// <summary>
    ///     Character
    /// </summary>
    public class FuriganaCharacter : StackLayout
    {
        /// <summary>
        ///     Ctor
        /// </summary>
        public FuriganaCharacter()
        {
            Spacing = 0;
            base.Orientation = StackOrientation.Horizontal;
            ChangeOrientation();
        }

        /// <summary>
        ///     Size
        /// </summary>
        public double FuriganaFontSize
        {
            get => _furiganaFontSize;
            set
            {
                _furiganaFontSize = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Size
        /// </summary>
        public double CharacterFontSize
        {
            get => _characterFontSize;
            set
            {
                _characterFontSize = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Size
        /// </summary>
        public double RomajiFontSize
        {
            get => _romajiFontSize;
            set
            {
                _romajiFontSize = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Spacing between furigana and character
        /// </summary>
        public double FuriganaSpacing
        {
            get => _furiganaSpacing;
            set
            {
                _furiganaSpacing = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Spacing between romaji and character
        /// </summary>
        public double RomajiSpacing
        {
            get => _romajiSpacing;
            set
            {
                _romajiSpacing = value;
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
        ///     Orientation
        /// </summary>
        public new StackOrientation Orientation
        {
            get => _orientation;
            set
            {
                if (_orientation != value)
                {
                    _orientation = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Text
        /// </summary>
        public FuriganaText Text
        {
            get => _furiganaText;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(FuriganaText) + "Cannot be null");

                if (_furiganaText != value)
                {
                    _furiganaText = value;
                    _furiganaText.PropertyChanged += (a, b) => { OnPropertyChanged(); };
                }
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     property change
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName)
            {
                case "FuriganaFontSize":
                    UpdateStyle();
                    break;
                case "CharacterFontSize":
                    UpdateStyle();
                    break;
                case "RomajiFontSize":
                    UpdateStyle();
                    break;
                case "FuriganaSpacing":
                    UpdateStyle();
                    break;
                case "RomajiSpacing":
                    UpdateStyle();
                    break;
                case "TextColor":
                    UpdateStyle();
                    break;
                case "Orientation":
                    ChangeOrientation();
                    break;
                case "Text":
                    UpdateText();
                    break;
            }
        }

        /// <summary>
        ///     Change orientation
        /// </summary>
        protected virtual void ChangeOrientation()
        {
            if (base.Orientation != Orientation)
            {
                _characterLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };

                _furiganaLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };

                _romajiLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };

                _furiganaSpacingBox = new BoxView();
                _romajiSpacingBox = new BoxView();

                if (Orientation == StackOrientation.Vertical)
                {
                    base.Orientation = StackOrientation.Vertical;
                    _furiganaSpacingBox.WidthRequest = 0;
                    _romajiSpacingBox.WidthRequest = 0;

                    //rotate
                    _romajiLabel.Rotation = 0;
                    //clear
                    Children.Clear();
                    //furigana
                    Children.Add(_furiganaLabel);
                    //spacing
                    Children.Add(_furiganaSpacingBox);
                    //character
                    Children.Add(_characterLabel);
                    //spacing
                    Children.Add(_romajiSpacingBox);
                    //romaji
                    Children.Add(_romajiLabel);
                }
                else
                {
                    base.Orientation = StackOrientation.Horizontal;

                    _furiganaLabel.WidthRequest = FuriganaFontSize;
                    _characterLabel.WidthRequest = CharacterFontSize;
                    //rotate
                    _romajiLabel.Rotation = 90;
                    //because even rotate 90 degree, width still affect stackLayout,
                    //so add *3 to tricky aviod that(romaji text)
                    _romajiLabel.WidthRequest = RomajiFontSize * 3;
                    _furiganaSpacingBox.HeightRequest = 0;
                    _romajiSpacingBox.HeightRequest = 0;
                    //clear
                    Children.Clear();
                    //romaji
                    Children.Add(_romajiLabel);
                    //spacing
                    Children.Add(_romajiSpacingBox);
                    //character
                    Children.Add(_characterLabel);
                    //spacing
                    Children.Add(_furiganaSpacingBox);
                    //furigana
                    Children.Add(_furiganaLabel);
                }
                //update text
                UpdateText();
                //update style
                UpdateStyle();
            }
        }

        /// <summary>
        ///     Update style
        /// </summary>
        protected void UpdateStyle()
        {
            //font size
            _furiganaLabel.FontSize = FuriganaFontSize;
            _characterLabel.FontSize = CharacterFontSize;
            _romajiLabel.FontSize = RomajiFontSize;
            if (TextColor != null)
            {
                //color
                _furiganaLabel.TextColor = TextColor.Value;
                _characterLabel.TextColor = TextColor.Value;
                _romajiLabel.TextColor = TextColor.Value;
            }
            if (Orientation == StackOrientation.Vertical)
            {
                //spacing
                _furiganaSpacingBox.HeightRequest = FuriganaSpacing;
                _romajiSpacingBox.HeightRequest = RomajiSpacing;
            }
            else
            {
                //spacing
                _furiganaSpacingBox.WidthRequest = FuriganaSpacing;
                _romajiSpacingBox.WidthRequest = RomajiSpacing;
            }
        }

        /// <summary>
        ///     Update text
        /// </summary>
        protected void UpdateText()
        {
            if (Text != null)
            {
                var orientation = Orientation.GetOppositeOrientation();
                _furiganaLabel.FormattedText =
                    FormattedStringHelper.MakeOrientationFormattedString(
                        string.IsNullOrEmpty(Text.Furigana) ? " " : Text.Furigana, orientation);
                _characterLabel.FormattedText =
                    FormattedStringHelper.MakeOrientationFormattedString(
                        string.IsNullOrEmpty(Text.Character) ? " " : Text.Character, orientation);
                _romajiLabel.Text = string.IsNullOrEmpty(Text.Romaji) ? " " : Text.Romaji;

                if (_furiganaText.TextColor != null)
                {
                    _furiganaLabel.TextColor = Text.TextColor.Value;
                    _characterLabel.TextColor = Text.TextColor.Value;
                    _romajiLabel.TextColor = Text.TextColor.Value;
                }
            }
        }

        #region UI

        private Label _characterLabel;
        private Label _furiganaLabel;
        private Label _romajiLabel;

        private BoxView _furiganaSpacingBox;
        private BoxView _romajiSpacingBox;

        private FuriganaText _furiganaText;

        #endregion

        #region Property

        private double _furiganaFontSize = 8;
        private double _characterFontSize = 15;
        private double _romajiFontSize = 7;
        private double _furiganaSpacing;
        private double _romajiSpacing;
        private Color? _textColor;
        private StackOrientation _orientation = StackOrientation.Vertical;

        #endregion
    }
}