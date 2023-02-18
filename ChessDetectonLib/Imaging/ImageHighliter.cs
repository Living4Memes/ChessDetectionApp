using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;

namespace ChessDetectonLib
{
      public static class ImageHighliter
      {
            // TODO:
            // Add text option to highlighters
            // Maybe add border to inmasked areas in mask highlighter

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
                        Image currentHighlighterImage = GetHighlighterImage(rect.Width, rect.Height, rect.Height);
                        // Drawing current highlighter image over initial image
                        result.Mutate(operation => operation.DrawImage(currentHighlighterImage, new Point(rect.Left, rect.Top), options));
                  }

                  // Return result image
                  return result;
            }

            /// <summary>
            /// Creates highlighter image with random color
            /// </summary>
            /// <param name="size">Size of the highlighter image to create</param>
            /// <param name="borderThickness">Thickness of the border around the image</param>
            /// <exception cref="ArgumentException"></exception>
            private static Image GetHighlighterImage(Size size, float borderThickness = 10f) =>
                  GetHighlighterImage(size.Width, size.Height, borderThickness);
            /// <summary>
            /// Creates highlighter image with random color
            /// </summary>
            /// <param name="width">Width of the highlighter image to create</param>
            /// <param name="height">Height of the highlighter image to create</param>
            /// <param name="borderThickness">Thickness of the border around the image</param>
            /// /// <exception cref="ArgumentException"></exception>
            private static Image GetHighlighterImage(int width, int height, float borderThickness = 10f)
            {
                  if (width < 0)
                        throw new ArgumentException("Width of the highlighter image could not be negative", nameof(width));
                  else if (height < 0)
                        throw new ArgumentException("Height of the highlighter image could not be negative", nameof(width));
                  else if (borderThickness < 0f)
                        throw new ArgumentException("Border thickness of the highlighter image could not be negative", nameof(borderThickness));

                  // Result image
                  Image result = new Image<Rgba32>(width, height);
                  // Options to fill the image
                  DrawingOptions options = new DrawingOptions() { GraphicsOptions = new GraphicsOptions() { BlendPercentage = 0.3f } };

                  // Filling image with random color
                  result.Mutate(operation => operation.Fill(options, ColorsProvider.NextRandomColor));

                  // If border is visible
                  if (borderThickness > 0)
                  {
                        // Pen to use for drawing border
                        Pen pen = new Pen(ColorsProvider.NextRandomColor, borderThickness);
                        // Points of the shape of the border
                        PointF[] borderPoints = new PointF[5]
                        {
                              new PointF(0, 0),
                              new PointF(0, height -1f),
                              new PointF(width - 1f, height  - 1f),
                              new PointF(width - 1f, 0),
                              new PointF(0, 0),
                        };

                        // Drawing the border
                        result.Mutate(operation => operation.DrawPolygon(pen, borderPoints));
                  }

                  // Returning the image
                  return result;
            }
      }
}
