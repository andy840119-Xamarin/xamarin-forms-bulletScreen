using Xamarin.Forms;

namespace BulletScreen.Extension
{
    public static class StackOrientationExtension
    {
        /// <summary>
        ///     get positive orientation
        /// </summary>
        /// <param name="orientation"></param>
        /// <returns></returns>
        public static StackOrientation GetOppositeOrientation(this StackOrientation orientation)
        {
            return orientation == StackOrientation.Vertical ? StackOrientation.Horizontal : StackOrientation.Vertical;
            ;
        }
    }
}