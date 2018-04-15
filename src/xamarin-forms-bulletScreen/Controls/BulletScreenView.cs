using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BulletScreen.Controls
{
    /// <summary>
    /// Bullet screen view
    /// </summary>
    /// <typeparam name="Text"></typeparam>
    public class BulletScreenView<Text> : Layout<Text> , IBulletScreenView where Text : BulletScreenText
    {

        /// <summary>
        /// Text Opcity
        /// </summary>
        public double TextOpcity
        {
            get => this.Opacity;
            set => Opacity = value;
        }

        /// <summary>
        /// Cutrrent time
        /// </summary>
        public double CurrentTime { get; set; }

        /// <summary>
        /// Layout child
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            throw new NotImplementedException();
        }
    }
}
