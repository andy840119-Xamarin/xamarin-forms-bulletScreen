using System;
using System.Collections.Generic;
using System.Text;

namespace BulletScreen.Controls
{
    /// <summary>
    /// Bullet screen view property
    /// </summary>
    public interface IBulletScreenView
    {
        /// <summary>
        /// Text Opcity
        /// </summary>
        double TextOpcity { get; set; }

        /// <summary>
        /// Cutrrent time
        /// </summary>
        double CurrentTime { get; set; }
    }
}
