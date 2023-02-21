using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDetectonLib.Structs
{
      internal struct HighlighterImageProperties
      {
            #region === Default instance ===

            public static HighlighterImageProperties DefaultInstance => new HighlighterImageProperties(200, 300);

            #endregion

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
            public float Opactiy { get;  }

            /// <summary>
            /// Color of the border
            /// </summary>
            public Color BorderColor { get; }

            /// <summary>
            /// Title in the top-left corner of the highlighter image
            /// </summary>
            public string Title { get; }

            /// <summary>
            /// Color of the title text
            /// </summary>
            public Color ForegroundColor { get; }

            #endregion

            #region === Constructor ===

            /// <summary>
            /// Creates properties for <see cref="HighlighterImage"/>
            /// </summary>
            /// <param name="width">Width of the image</param>
            /// <param name="height">Height of the image</param>
            /// <param name="imageColor">Optional. Color of the image</param>
            /// <param name="opacity">Optional. Opacity of the background  (from 0 to 1)</param>
            /// <param name="borderColor">Optional. Color of the border</param>
            /// <param name="title">Optional. Title of the image</param>
            /// <param name="foregroundColor">Optional. Color of text of the title</param>
            /// <exception cref="ArgumentException"/>
            public HighlighterImageProperties(int width, int height, Color? imageColor = null, float opacity = 0.5f, Color? borderColor = null, string title = "", Color? foregroundColor = null)
            {
                  if (opacity < 0 || opacity > 1)
                        throw new ArgumentException("Opacity must be a number between 0 and 1", nameof(opacity));

                  Size = new Size(width, height);
                  Width = width;
                  Height = height;
                  ImageColor = imageColor == null ? ColorsProvider.NextRandomColor : (Color)imageColor;
                  Opactiy = opacity;
                  BorderColor = borderColor == null ? ColorsProvider.NextRandomColor : ImageColor;
                  Title = title;
                  ForegroundColor = foregroundColor == null ? ColorsProvider.GetReadableForegroundColor(BorderColor): ImageColor;
            }

            /// <summary>
            /// Creates properties for <see cref="HighlighterImage"/>
            /// </summary>
            /// <param name="size">Size of the highlighter image</param>
            /// <param name="imageColor">Color of the image</param>
            /// <param name="opactiy">Opacity of the background</param>
            /// <param name="borderColor">Color of the border</param>
            /// <param name="title">Title of the image</param>
            /// <param name="foregroundColor">Optional. Color of text of the title</param>
            /// <exception cref="ArgumentException"/>
            public HighlighterImageProperties(Size size, Color? imageColor = null, float opactiy = 0.5f, Color? borderColor = null, string title = "", Color? foregroundColor = null) : 
                  this(size.Width, size.Height, imageColor, opactiy, borderColor, title, foregroundColor) { }

            #endregion

            /// <summary>
            /// Creates properties for <see cref="HighlighterImage"/> from input <see cref="Image"/>
            /// </summary>
            /// <param name="image">Input image</param>
            /// <param name="imageColor">Optional. Color of the image</param>
            /// <param name="opacity">Optional. Opacity of the background  (from 0 to 1)</param>
            /// <param name="borderColor">Optional. Color of the border</param>
            /// <param name="title">Optional. Title of the image</param>
            /// <param name="foregroundColor">Optional. Color of text of the title</param>
            /// <returns><see cref="HighlighterImage"/></returns>
            /// <exception cref="ArgumentException"/>
            public static HighlighterImageProperties ExtractFromImage(Image image, Color? imageColor = null, float opacity = 0.5f, Color? borderColor = null, string title = "", Color? foregroundColor = null)
            {
                  return new HighlighterImageProperties(image.Width, image.Height, imageColor, opacity, borderColor, title, foregroundColor);
            }
      }
}
