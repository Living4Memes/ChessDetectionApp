using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDetectonLib.Structs
{
      public struct MaskHighlighterImageProperties
      {
            #region === Public properties ===

            /// <summary>
            /// Size of the mask image
            /// </summary>
            public Size Size { get; }

            /// <summary>
            /// Width of the mask image
            /// </summary>
            public int Width { get; }

            /// <summary>
            /// Height of the mask image
            /// </summary>
            public int Height { get; }

            /// <summary>
            /// Color of the mask
            /// </summary>
            public Color BackgroundColor { get; }

            /// <summary>
            /// Opacity of the mask image
            /// </summary>
            public float Opacity { get; }

            /// <summary>
            /// Thickness of the border around each highlighted object
            /// </summary>
            public float BorderThickness { get; }

            /// <summary>
            /// Color of the border around each object
            /// </summary>
            public Color BorderColor { get; }

            #endregion

            #region === Constructor ===

            public MaskHighlighterImageProperties(int width, int height, Color? backgroundColor = null, float opacity = 0.5f, float borderThickness = 10f, Color? borderColor = null)
            {
                  if (width <= 0 || height <= 0)
                        throw new ArgumentException("Width and height of the image must be bigger than 0");
                  if (opacity < 0 || opacity > 1)
                        throw new ArgumentException("Opacity must be a number between 0 and 1", nameof(opacity));
                  if (borderThickness < 0)
                        throw new ArgumentException("Thickness of the border must be a number between 0 and 1", nameof(borderThickness));

                  Size = new Size(width, height);
                  Width = width;
                  Height = height;
                  BackgroundColor = backgroundColor == null ? Color.Red : (Color)backgroundColor;
                  Opacity = opacity;
                  BorderThickness = borderThickness;
                  BorderColor = backgroundColor == null ? Color.Red : (Color)borderColor;
            }

            #endregion
      }
}
