using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Linq;

namespace ChessDetectonLib
{
      internal static class ColorsProvider
      {
            /// <summary>
            /// Known colors to take from
            /// </summary>
            private static System.Drawing.Color[] knownColors = Enum.GetValues(typeof(System.Drawing.KnownColor))
                        .Cast<System.Drawing.KnownColor>()
                        .Select(x => System.Drawing.Color.FromKnownColor(x))
                        .ToArray();

            /// <summary>
            /// Random numbers generator for color selection
            /// </summary>
            private static Random _rnd = new Random();

            /// <summary>
            /// Returns random color from known colors avaliable in <see cref="System.Drawing.Color"/>
            /// </summary>
            internal static Color NextRandomColor => GetNextRandomColor();

            /// <summary>
            /// Creates random color from known colors avaliable in <see cref="System.Drawing.Color"/>
            /// </summary>
            /// <returns>Random <see cref="Color"/></returns>
            internal static Color GetNextRandomColor()
            {
                  return knownColors[_rnd.Next(knownColors.Length)].ToImageSharpColor();
            }

            /// <summary>
            /// Returns black or white color that will be easily visible on input background color
            /// </summary>
            /// <param name="backgroundColor">Background color of the text</param>
            /// <returns>Foreground <see cref="Color"/> (black or white depending on background color)</returns>
            internal static Color GetReadableForegroundColor(Color backgroundColor)
            {
                  // Converting color to pixel to extract RGB values
                  Rgba32 backgroundColorPixel = backgroundColor.ToPixel<Rgba32>();
                  return (((backgroundColorPixel.R + backgroundColorPixel.G + backgroundColorPixel.B) / 3) > 128) ? Color.Black : Color.White;
            }

      }
}
