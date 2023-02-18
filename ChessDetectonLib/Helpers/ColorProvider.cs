using SixLabors.ImageSharp;
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
            internal static Color NextRandomColor { get => knownColors[_rnd.Next(knownColors.Length)].ToImageSharpColor(); }
      }
}
