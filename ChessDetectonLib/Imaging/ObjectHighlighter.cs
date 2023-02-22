using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using SixLabors.Fonts;
using System.Linq;

namespace ChessDetectonLib
{
      /// <summary>
      /// Is used to highlight objects on images
      /// </summary>
      public static class ObjectHighlighter
      {
            // TODO:
            // Maybe add border in masked areas in mask highlighter
            // Create class for mask

            /// <summary>
            /// Highlights areas on the image with mask
            /// </summary>
            /// <param name="initialImage">Initial image to highlight on</param>
            /// <param name="rectanglesToHighlight">Areas to highlight</param>
            /// <returns>New image with highlighted areas</returns>
            public static Image HighlightWithMask(Image initialImage, params Rectangle[] rectanglesToHighlight)
            {
                  // Initial image
                  Image result = initialImage.Clone(operation => operation.Opacity(1f));
                  // Color of the mask
                  Color maskColor = Color.Gray;
                  // Mask for each rectangle to highlight
                  Image mask = new Image<Rgba32>(initialImage.Width, initialImage.Height, maskColor);
                  // Recolor brush to draw transparent rectangles
                  RecolorBrush brush = new RecolorBrush(maskColor, Color.Transparent, 1f);

                  // Draw each rectangle on mask
                  foreach (Rectangle rect in rectanglesToHighlight)
                        // Draw current rectangle on mask with transparent color
                        mask.Mutate(operation => operation.Clear(brush, rect));

                  // Apply mask to the initial image
                  result.Mutate(operation => operation.DrawImage(mask, PixelColorBlendingMode.Normal, 0.5f));

                  // Return result image
                  return result;
            }

            /// <summary>
            /// Highlights areas on the image with random colors
            /// </summary>
            /// <param name="initialImage">Initial image to highlight on</param>
            /// <param name="rectanglesToHighlight">Areas to highlight</param>
            /// <returns>New image with highlighted areas</returns>
            public static Image HighlightWithColor(Image initialImage, params Rectangle[] rectanglesToHighlight)
            {
                  // Initial image
                  Image result = initialImage.Clone(operation => operation.Opacity(1f));
                  // Options to draw highlighter image over initial image
                  GraphicsOptions options = new GraphicsOptions { ColorBlendingMode = PixelColorBlendingMode.Normal, BlendPercentage = 1f, };

                  // Draw each rectangle on the image
                  foreach (Rectangle rect in rectanglesToHighlight)
                  {
                        // Current highlighter image
                        //Image currentHighlighterImage = GetHighlighterImage(rect.Width, rect.Height, rect.Height);
                        // Drawing current highlighter image over initial image
                        //result.Mutate(operation => operation.DrawImage(currentHighlighterImage, new Point(rect.Left, rect.Top), options));
                  }

                  // Return result image
                  return result;
            }
      }
}
