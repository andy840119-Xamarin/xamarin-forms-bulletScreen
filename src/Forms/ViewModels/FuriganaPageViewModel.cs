using System.Collections.ObjectModel;
using BulletScreen.Model;

namespace Form.ViewModels
{
    public class FuriganaPageViewModel : BaseViewModel
    {
        public ObservableCollection<FuriganaText> Texts = new ObservableCollection<FuriganaText>();

        public FuriganaPageViewModel()
        {
            Texts.Clear();
            Texts.Add(new FuriganaText("終", "お", "o"));
            Texts.Add(new FuriganaText("わ", "", "wa"));
            Texts.Add(new FuriganaText("る", "", "ru"));
            Texts.Add(new FuriganaText("ま", "", "ma"));
            Texts.Add(new FuriganaText("で", "", "de")
            {
                ChangeNewLine = true
            });
            Texts.Add(new FuriganaText("は", "", "waaaaaaaaa"));
            Texts.Add(new FuriganaText("終", "おおおおおお"));
            Texts.Add(new FuriganaText("わ"));
            Texts.Add(new FuriganaText("ら"));
            Texts.Add(new FuriganaText("な"));
            Texts.Add(new FuriganaText("い"));
            Texts.Add(new FuriganaText("よ"));
            Texts.Add(new FuriganaText("..."));
            //demo for multi-line
            Texts.Add(new FuriganaText("終", "お", "o"));
            Texts.Add(new FuriganaText("わ", "", "wa"));
            Texts.Add(new FuriganaText("る", "", "ru"));
            Texts.Add(new FuriganaText("ま", "", "ma"));
            Texts.Add(new FuriganaText("で", "", "de"));
            Texts.Add(new FuriganaText("は", "", "waaaaaaaaa"));
            Texts.Add(new FuriganaText("終", "おおおおおお"));
            Texts.Add(new FuriganaText("わ"));
            Texts.Add(new FuriganaText("ら"));
            Texts.Add(new FuriganaText("な"));
            Texts.Add(new FuriganaText("い"));
            Texts.Add(new FuriganaText("よ"));
        }
    }
}