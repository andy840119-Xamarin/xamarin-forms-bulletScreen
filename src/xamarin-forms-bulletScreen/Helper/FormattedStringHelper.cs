using System;
using Xamarin.Forms;

namespace BulletScreen.Helper
{
    /// <summary>
    ///     Convert text to FormattedString
    /// </summary>
    public static class FormattedStringHelper
    {
        /// <summary>
        ///     Make orientation foramtted string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="orientation"></param>
        /// <returns></returns>
        public static FormattedString MakeOrientationFormattedString(string str, StackOrientation orientation)
        {
            if (orientation == StackOrientation.Vertical)
                return MakeTextVertical(str);
            return MakeTextHorizontal(str);
        }

        /// <summary>
        ///     Gemerate Vertical text
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static FormattedString MakeTextVertical(string str)
        {
            var formattedString = new FormattedString();

            //generate lise spans
            for (var i = 0; i < str.Length; i++)
            {
                //text
                formattedString.Spans.Add(new Span {Text = str[i].ToString()});
                //new line
                if (i < str.Length - 1)
                    formattedString.Spans.Add(new Span {Text = Environment.NewLine});
            }

            return formattedString;
        }

        /// <summary>
        ///     Gemerate Vertical text
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static FormattedString MakeTextHorizontal(string str)
        {
            return new FormattedString
            {
                Spans =
                {
                    new Span {Text = str}
                }
            };
        }
    }
}