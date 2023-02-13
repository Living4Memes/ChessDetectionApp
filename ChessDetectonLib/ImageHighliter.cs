using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing.Processing;
using System.Linq;

namespace ChessDetectonLib
{
      public static class ImageHighliter
      {
            /// <summary>
            /// Highlights areas on the image
            /// </summary>
            /// <param name="initialImage">Initial image to highlight on</param>
            /// <param name="rectanglesToHighlight">Areas to highlight</param>
            /// <returns></returns>
            public static Image Highlight(Image initialImage, params RectangleF[] rectanglesToHighlight)
            {
                  // Initial image
                  Image result = initialImage.Clone(x => x.Opacity(1f));
                  // Color of the mask
                  Color maskColor = Color.Gray;
                  // Mask for each rectangle to highlight
                  Image mask = new Image<Rgba32>(initialImage.Width, initialImage.Height, maskColor);
                  // Recolor brush to draw transparent rectangles
                  RecolorBrush brush = new RecolorBrush(maskColor, Color.Transparent, 1f);

                  // Draw each rectangle on mask
                  foreach(RectangleF rect in rectanglesToHighlight)
                        // Draw current rectangle on mask with transparent color
                        mask.Mutate(x => x.Clear(brush, rect));

                  // Apply mask to the initial image
                  result.Mutate(x => x.DrawImage(mask, PixelColorBlendingMode.Normal, 0.5f));

                  DemonstrateResultAsync(initialImage, mask, result);

                  // Return result image
                  return result;
            }

            internal async static void DemonstrateResultAsync(Image initialImage, Image maskImage, Image resultImage)
            {
                  Size resultSize = new Size(initialImage.Width + maskImage.Width + resultImage.Width, new int[] { initialImage.Height, maskImage.Height, resultImage.Height }.Max() );
                  Image<Rgba32> demonstrationImage = new Image<Rgba32>(resultSize.Width, resultSize.Height, Color.White);

                  Point initialImageLocation = new Point(0, 0);
                  Point maskImageLocation = new Point(maskImage.Width , 0);
                  Point resultImageLocation = new Point(maskImage.Width + resultImage.Width, 0);

                  demonstrationImage.Mutate(x => x.DrawImage(initialImage, initialImageLocation, 1f));
                  demonstrationImage.Mutate(x => x.DrawImage(maskImage, maskImageLocation, 1f));
                  demonstrationImage.Mutate(x => x.DrawImage(resultImage, resultImageLocation, 1f));

                  await demonstrationImage.SaveAsPngAsync("demonstration.png");
            }
      }
}
