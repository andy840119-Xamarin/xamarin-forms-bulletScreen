using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BulletScreen.Model;

namespace BulletScreen.Controls
{
    /// <summary>
    /// Bullet screen view property
    /// </summary>
    public interface IBulletScreenView<TComment> where TComment : IComment
    {
        /// <summary>
        /// Text Opcity
        /// </summary>
        double TextOpcity { get; set; }

        /// <summary>
        /// Cutrrent time
        /// </summary>
        double CurrentTime { get; set; }

        /// <summary>
        /// List comments
        /// </summary>
        ObservableCollection<TComment> Comments { get; set; }
    }
}
