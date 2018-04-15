using System;
using System.Collections.Generic;
using System.Text;

namespace BulletScreen.Model
{
    /// <summary>
    /// Basic comment component
    /// </summary>
    public interface IComment : ICommentGuid
    {
        /// <summary>
        /// Comment
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Which time send the common
        /// </summary>
        double CommentTime { get; set; }

        /// <summary>
        /// Orientation
        /// </summary>
        CommentOrientation Orientation { get; set; }

        /// <summary>
        /// Hex Color
        /// Format is #RRGGBBAA or #RRGGBB
        /// </summary>
        string HexColor { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        CommentTextSize Size { get; set; }
    }
}
