using ChessDetectonLib.Structs;
using SixLabors.ImageSharp;
using System;

namespace ChessDetectonLib
{
      /// <summary>
      /// Image to draw over initial image to highlight something
      /// </summary>
      internal sealed class HighlighterImage
      {
            #region === Public fields ===

            /// <summary>
            /// Size of the highlighter image
            /// </summary>
            public Size Size { get; }

            /// <summary>
            /// Width of the highlighter image
            /// </summary>
            public int Width { get; }

            /// <summary>
            /// Height of the highlighter image
            /// </summary>
            public int Height { get; }

            /// <summary>
            /// Color of the Image
            /// </summary>
            public Color ImageColor { get; }

            /// <summary>
            /// Opacity of the background
            /// </summary>
            public float Opactiy { get; }

            /// <summary>
            /// Color of the border
            /// </summary>
            public Color BorderColor { get; }

            /// <summary>
            /// Title in the top-left corner
            /// </summary>
            public string Title { get; }

            /// <summary>
            /// Color of the title text
            /// </summary>
            public Color ForegroundColor { get; }

            /// <summary>
            /// Highlighter image to use
            /// </summary>
            public Image Image { get; private set; }

            #endregion

            #region === Constructor ===

            private HighlighterImage()
            {

            }

            public HighlighterImage(HighlighterImageProperties properties)
            {
                  Size = properties.Size;
                  Width = properties.Width;
                  Height = properties.Height;
                  ImageColor = properties.ImageColor;
                  Opactiy = properties.Opactiy;
                  BorderColor = properties.BorderColor;
                  Title = properties.Title;
                  ForegroundColor = properties.ForegroundColor;
            }

            #endregion

            /// <summary>
            /// Creates a new instance of <see cref="HighlighterImage"/> for input image
            /// </summary>
            /// <param name="image">Image to generate <see cref="HighlighterImage"/> to</param>
            /// <param name="title">Optional title</param>
            /// <returns></returns>
            public static HighlighterImage GetForImage(Image image, string title = "")
            {
                  throw new NotImplementedException();
            }
      }
}
