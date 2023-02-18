using SixLabors.ImageSharp;

namespace ChessDetectonLib
{
      internal static class Extensions
      {
            internal static Color ToImageSharpColor(this System.Drawing.Color color)
            {
                  return Color.FromRgb(color.R, color.G, color.B);
            }
      }
}
