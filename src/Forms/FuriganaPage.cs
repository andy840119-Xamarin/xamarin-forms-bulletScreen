using BulletScreen.Controls;
using CustomLayoutsDemo.ViewModels;
using Xamarin.Forms;

namespace CustomLayoutsDemo
{
    public class FuriganaPage : ContentPage
    {
        private readonly FuriganaPageViewModel _viewModel;

        /// <summary>
        ///     Ctor
        /// </summary>
        public FuriganaPage()
        {
            Title = "Furigana text";
            _viewModel = new FuriganaPageViewModel();
            BindingContext = _viewModel;

            Content = new FuriganaLabel
            {
                CharacterFontSize = 20,
                RomajiFontSize = 12,
                FuriganaFontSize = 10,
                Orientation = StackOrientation.Vertical,
                BindingContext = _viewModel.Texts,
            };
        }
    }
}