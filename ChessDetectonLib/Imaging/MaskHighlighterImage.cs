using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using System.ComponentModel;

namespace ChessDetectonLib.Imaging
{
      [AddINotifyPropertyChangedInterface]
      public class MaskHighlighterImage : INotifyPropertyChanged
      {
            #region === Events ===

            public event PropertyChangedEventHandler PropertyChanged;

            #endregion

            #region === Public properties ===

            /// <summary>
            /// Size of the mask image
            /// </summary>
            public Size Size { get; set; }

            /// <summary>
            /// Width of the mask image
            /// </summary>
            public int Width { get; set; }

            /// <summary>
            /// Height of the mask image
            /// </summary>
            public int Height { get; set; }

            /// <summary>
            /// List of all areas in the image to highlight
            /// </summary>
            public List<RectangleF> HighlightedObjects { get; }

            /// <summary>
            /// Color of the mask
            /// </summary>
            public Color BackgroundColor { get; set; }

            /// <summary>
            /// Thickness of the border around each highlighted object
            /// </summary>
            public float BorderThickness { get; set; }

            /// <summary>
            /// Color of the border around each object
            /// </summary>
            public Color BorderColor { get; set; }

            /// <summary>
            /// Mask image to use
            /// </summary>
            public Image Image { get; private set; }

            #endregion

            #region === Constructor

            public MaskHighlighterImage()
            {

            }

            #endregion
      }
}
