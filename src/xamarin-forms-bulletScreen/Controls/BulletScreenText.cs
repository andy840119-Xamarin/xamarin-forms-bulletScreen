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
        /// how much time comment stay on the screen
        /// </summary>
        public double StayTime { get; set; }

        public BulletScreenText()
        {

        }
        
    }
}
