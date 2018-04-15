using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BulletScreen.Controls
{
    /// <summary>
    /// Text
    /// </summary>
    public class BulletScreenText : Label , IBulletScreenText
    {
        /// <summary>
        /// Comment Guid
        /// </summary>
        public Guid CommentGuid { get; set; }

        /// <summary>
        /// Row
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public BulletScreenText()
        {

        }
    }
}
