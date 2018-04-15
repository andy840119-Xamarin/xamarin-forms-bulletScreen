using System;
using System.Collections.Generic;
using System.Text;

namespace BulletScreen.Model
{
    public interface ICommentStayTime
    {
        /// <summary>
        /// how much time comment stay on the screen
        /// </summary>
        double StayTime { get; set; }
    }
}
