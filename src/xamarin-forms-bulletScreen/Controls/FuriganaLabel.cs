using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BulletScreen.Extension;
using BulletScreen.Model;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BulletScreen.Controls
{
    /// <summary>
    ///     Label
    ///     Contain list of character <see cref="FuriganaCharacter" />
    /// </summary>
    public class FuriganaLabel : FuriganaLabel<FuriganaCharacter, FuriganaText>
    {
    }

    /// <summary>
    ///     Label
    ///     contain list of character <see cref="FuriganaText" />
    /// </summary>
    public class FuriganaLabel<Character, TextModel> : Layout<Character> where Character : FuriganaCharacter, new()
        where TextModel : FuriganaText, new()
    {
        private bool _autoChangeNewLine = true;
        private double _characterFontSize = 15;
        private double _characterSpacing = 1;

        //property
        private double _furiganaFontSize = 8;

        private double _furiganaSpacing;
        private StackOrientation _orientation = StackOrientation.Horizontal;
        private double _romajiFontSize = 7;
        private double _romajiSpacing;
        private ObservableCollection<TextModel> _text;

        private Color? _textColor;

        //local
        private int _totalLines = 1;

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
        ///     Spacing between two chatacters
        /// </summary>
        public double Spacing
        {
            get => _characterSpacing;
            set
            {
                _characterSpacing = value;
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
        ///     Auto change new-line
        /// </summary>
        public bool AutoChangeNewLine
        {
            get => _autoChangeNewLine;
            set
            {
                if (_autoChangeNewLine != value)
                {
                    _autoChangeNewLine = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Orientation
        /// </summary>
        public StackOrientation Orientation
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
        ///     Model
        /// </summary>
        public ObservableCollection<TextModel> Text
        {
            get => _text;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(TextModel) + "Cannot be null");

                if (_text != value)
                {
                    _text = value;
                    _text.CollectionChanged += (a, b) => { OnPropertyChanged(); };
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
                    Children.ForEach(X => X.FuriganaFontSize = FuriganaFontSize);
                    ForceLayout();
                    break;
                case "CharacterFontSize":
                    Children.ForEach(X => X.CharacterFontSize = CharacterFontSize);
                    ForceLayout();
                    break;
                case "RomajiFontSize":
                    Children.ForEach(X => X.RomajiFontSize = RomajiFontSize);
                    ForceLayout();
                    break;
                case "Spacing":
                    ForceLayout();
                    break;
                case "FuriganaSpacing":
                    Children.ForEach(X => X.FuriganaSpacing = FuriganaSpacing);
                    ForceLayout();
                    break;
                case "RomajiSpacing":
                    Children.ForEach(X => X.RomajiSpacing = RomajiSpacing);
                    ForceLayout();
                    break;
                case "TextColor":
                    Children.ForEach(X => X.TextColor = TextColor);
                    break;
                case "AutoChangeNewLine":
                    ForceLayout();
                    break;
                case "Orientation":
                    //Children.ForEach(X => X.Orientation = Orientation.GetOppositeOrientation());
                    //ForceLayout();
                    InitialText();
                    break;
                case "Text":
                    InitialText();
                    break;
            }
        }

        /// <summary>
        ///     Context change
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            //change model
            if (BindingContext is ObservableCollection<TextModel> text)
                Text = text;
            else
                throw new ArgumentException("Binding must be the type of : " + nameof(ObservableCollection<TextModel>));
        }

        /// <summary>
        ///     property change
        ///     now is clean and re-generate everying
        ///     TODO : optomize this
        /// </summary>
        protected virtual void InitialText()
        {
            if (Text != null)
            {
                //generate list text
                Children.Clear();
                foreach (var singleChar in Text ?? new ObservableCollection<TextModel>())
                {
                    var furiganaText = new Character();
                    furiganaText.FuriganaFontSize = FuriganaFontSize;
                    furiganaText.CharacterFontSize = CharacterFontSize;
                    furiganaText.RomajiFontSize = RomajiFontSize;
                    furiganaText.FuriganaSpacing = FuriganaSpacing;
                    furiganaText.RomajiSpacing = RomajiSpacing;
                    furiganaText.Orientation = Orientation.GetOppositeOrientation();
                    furiganaText.Text = singleChar;
                    //if need change text color
                    if (TextColor != null)
                        furiganaText.TextColor = TextColor;
                    //add in child
                    Children.Add(furiganaText);
                }
            }

            //force layout
            ForceLayout();
        }

        /// <summary>
        ///     TODO : IDK what does it means ,this code is from another place
        /// </summary>
        /// <param name="widthConstraint"></param>
        /// <param name="heightConstraint"></param>
        /// <returns></returns>
        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            if (WidthRequest > 0)
                widthConstraint = Math.Min(widthConstraint, WidthRequest);
            if (HeightRequest > 0)
                heightConstraint = Math.Min(heightConstraint, HeightRequest);

            var internalWidth = double.IsPositiveInfinity(widthConstraint)
                ? double.PositiveInfinity
                : Math.Max(0, widthConstraint);
            var internalHeight = double.IsPositiveInfinity(heightConstraint)
                ? double.PositiveInfinity
                : Math.Max(0, heightConstraint);

            return DoHorizontalMeasure(internalWidth, internalHeight);
        }

        /// <summary>
        ///     TODO : IDK what does it means ,this code is from another place
        /// </summary>
        /// <param name="widthConstraint"></param>
        /// <param name="heightConstraint"></param>
        /// <returns></returns>
        private SizeRequest DoHorizontalMeasure(double widthConstraint, double heightConstraint)
        {
            _totalLines = 1;
            var newLine = false;

            double width = 0;
            double height = 0;
            double minWidth = 0;
            double minHeight = 0;
            double widthUsed = 0;
            double heightUsed = 0;


            if (Orientation == StackOrientation.Horizontal)
            {
                foreach (var item in Children)
                {
                    var size = item.Measure(widthConstraint, heightConstraint);
                    height = Math.Max(height, size.Request.Height);

                    var newWidth = width + size.Request.Width + Spacing;
                    if (newWidth > widthConstraint || newLine)
                    {
                        _totalLines++;
                        widthUsed = Math.Max(width, widthUsed);
                        width = size.Request.Width;
                        newLine = false;
                    }
                    else
                    {
                        width = newWidth;
                    }

                    //change new line in next character
                    if (item.Text.ChangeNewLine)
                        newLine = true;


                    minHeight = Math.Max(minHeight, size.Minimum.Height);
                    minWidth = Math.Max(minWidth, size.Minimum.Width);
                }

                if (_totalLines > 1)
                {
                    width = Math.Max(width, widthUsed);
                    height = (height + Spacing) * _totalLines - Spacing; // via MitchMilam 
                }

                return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
            }
            foreach (var item in Children)
            {
                var size = item.Measure(widthConstraint, heightConstraint);
                width = Math.Max(width, size.Request.Width);

                var newHeight = height + size.Request.Height + Spacing;
                if (newHeight > heightConstraint || newLine)
                {
                    _totalLines++;
                    heightUsed = Math.Max(height, heightUsed);
                    height = size.Request.Height;
                    newLine = false;
                }
                else
                {
                    height = newHeight;
                }

                //change new line in next character
                if (item.Text.ChangeNewLine)
                    newLine = true;

                minHeight = Math.Max(minHeight, size.Minimum.Height);
                minWidth = Math.Max(minWidth, size.Minimum.Width);
            }

            if (_totalLines > 1)
            {
                width = Math.Max(width, widthUsed);
                height = (height + Spacing) * _totalLines - Spacing; // via MitchMilam 
            }

            return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
        }

        /// <summary>
        ///     TODO : IDK what does it means ,this code is from another place
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            double rowWidth = 0;
            double rowHeight = 0;
            double yPos = y, xPos = x;
            _totalLines = 1;

            var newLine = false;

            if (Orientation == StackOrientation.Horizontal)
            {
                foreach (var child in Children.Where(c => c.IsVisible))
                {
                    var request = child.Measure(width, height);

                    var childWidth = request.Request.Width;
                    var childHeight = request.Request.Height;

                    rowHeight = Math.Max(rowHeight, childHeight);

                    if (xPos + childWidth > width || newLine)
                    {
                        xPos = x;
                        yPos += rowHeight + Spacing;
                        rowHeight = 0;
                        newLine = false;
                        _totalLines++;
                    }

                    //change new line in next character
                    if (child.Text.ChangeNewLine)
                        newLine = true;

                    var region = new Rectangle(xPos, yPos, childWidth, childHeight);
                    LayoutChildIntoBoundingRegion(child, region);
                    xPos += region.Width + Spacing;
                }
            }
            else
            {
                //cal the total lines first
                foreach (var child in Children.Where(c => c.IsVisible))
                {
                    var request = child.Measure(width, height);
                    var childWidth = request.Request.Width;
                    var childHeight = request.Request.Height;

                    rowWidth = Math.Max(rowWidth, childWidth);

                    if (yPos + childHeight > height || newLine)
                    {
                        xPos = xPos + rowWidth + Spacing;
                        yPos = y;
                        rowWidth = 0;
                        newLine = false;
                        _totalLines++;
                    }

                    //change new line in next character
                    if (child.Text.ChangeNewLine)
                        newLine = true;

                    var region = new Rectangle(xPos, yPos, childWidth, childHeight);
                    LayoutChildIntoBoundingRegion(child, region);
                    yPos += region.Height + Spacing;
                }


                yPos = y;
                xPos = x;

                //adjust the position
                foreach (var child in Children.Where(c => c.IsVisible))
                {
                    var request = child.Measure(width, height);
                    var childWidth = request.Request.Width;
                    var childHeight = request.Request.Height;

                    rowWidth = Math.Max(rowWidth, childWidth);
                    var lineSpacing = rowWidth + Spacing;

                    if (yPos + childHeight > height || newLine)
                    {
                        xPos = xPos - lineSpacing;
                        yPos = y;
                        rowWidth = 0;
                        newLine = false;
                    }

                    //change new line in next character
                    if (child.Text.ChangeNewLine)
                        newLine = true;

                    var region = new Rectangle(xPos + lineSpacing * (_totalLines - 1), yPos, childWidth, childHeight);
                    LayoutChildIntoBoundingRegion(child, region);
                    yPos += region.Height + Spacing;
                }
            }
        }
    }
}