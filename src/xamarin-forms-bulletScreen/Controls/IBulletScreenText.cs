using System;
using System.Collections.Generic;
using System.Text;
using BulletScreen.Model;

namespace BulletScreen.Controls
{
    public interface IBulletScreenText : ICommentGuid
    {
        int Row { get; set; }
    }
}
