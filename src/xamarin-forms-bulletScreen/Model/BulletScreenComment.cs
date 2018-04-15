using System;
using System.Collections.Generic;
using System.Text;

namespace BulletScreen.Model
{
    /// <summary>
    /// ModelText
    /// </summary>
    public class BulletScreenComment : IBulletScreenComment , ICommentMovingSpeed , ICommentStayTime
    {
        #region Common

        /// <summary>
        /// Comment
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Which time send the common
        /// </summary>
        public double CommentTime { get; set; }

        /// <summary>
        /// Orientation
        /// </summary>
        public CommentOrientation Orientation { get; set; }

        /// <summary>
        /// Hex Color
        /// Format is #RRGGBBAA or #RRGGBB
        /// </summary>
        public string HexColor { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public CommentTextSize Size { get; set; }

        #endregion

        #region Flow comment

        /// <summary>
        /// Speed
        /// </summary>
        public float Speed { get; set; }

        #endregion

        #region StayCommnt

        /// <summary>
        /// StayTime
        /// </summary>
        public double StayTime { get; set; }

        #endregion
    }
}
