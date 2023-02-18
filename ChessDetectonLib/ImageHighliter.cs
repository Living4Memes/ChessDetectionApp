using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing.Processing;
using System.Linq;
using System;
using System.Net.Http.Headers;
using System.Windows;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.Generic;

namespace ChessDetectonLib
{
      public static class ImageHighliter
      {
            /// <summary>
            /// Highlights areas on the image with mask
            /// </summary>
            /// <param name="initialImage">Initial image to highlight on</param>
            /// <param name="rectanglesToHighlight">Areas to highlight</param>
            /// <returns></returns>
            public static Task<Image> HighlightWithMask(Image initialImage, params Rectangle[] rectanglesToHighlight)
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
                  foreach(Rectangle rect in rectanglesToHighlight)
                        // Draw current rectangle on mask with transparent color
                        mask.Mutate(operation => operation.Clear(brush, rect));

                  // Apply mask to the initial image
                  result.Mutate(operation => operation.DrawImage(mask, PixelColorBlendingMode.Normal, 0.5f));

                  //CreateDemonstrationImageAsync(initialImage, mask, result);

                  // Return result image
                  return Task.FromResult(result);
            }

            /// <summary>
            /// Highlights areas on the image with random colors
            /// </summary>
            /// <param name="initialImage">Initial image to highlight on</param>
            /// <param name="rectanglesToHighlight">Areas to highlight</param>
            /// <returns></returns>
            public static Task<Image> HighlightWithColor(Image initialImage, params Rectangle[] rectanglesToHighlight)
            {
                  // Initial image
                  Image result = initialImage.Clone(operation => operation.Opacity(1f));
                  // Options to draw highlighter image over initial image
                  GraphicsOptions options = new GraphicsOptions { ColorBlendingMode = PixelColorBlendingMode.Normal, BlendPercentage = 1f, };

                  // Draw each rectangle on the image
                  foreach (Rectangle rect in rectanglesToHighlight)
                  {
                        // Current highlighter image
                        Image currentHighlighterImage = GetHighlighterImage(rect.Width, rect.Height).Result;
                        // Drawing current highlighter image over initial image
                        result.Mutate(operation => operation.DrawImage(currentHighlighterImage, new Point(rect.Left, rect.Top), options));
                  }

                  //CreateDemonstrationImageAsync(initialImage, initialImage, result);

                  // Return result image
                  return Task.FromResult(result);
            }

            /// <summary>
            /// Creates highlighter image with random color
            /// </summary>
            /// <param name="width">Width of the image</param>
            /// <param name="height">Height of the image</param>
            /// <param name="borderThickness">Thickness of the border of the image</param>
            /// <returns></returns>
            public static Task<Image> GetHighlighterImage(int width, int height, float borderThickness = 10f)
            {
                  // Result image
                  Image result = new Image<Rgba32>(width, height);
                  // Random numbers generator for colors
                  Random rnd = new Random();
                  // Random color for the image
                  System.Drawing.Color[] colors = (System.Drawing.Color[])typeof(System.Drawing.Color).GetEnumValues();
                  System.Drawing.Color tempColor = colors[rnd.Next(0, colors.Length)];
                  Color color = Color.FromRgb(tempColor.R, tempColor.G, tempColor.B);
                  // Options to fill the image
                  DrawingOptions options = new DrawingOptions() { GraphicsOptions = new GraphicsOptions() { BlendPercentage = 0.3f } };
                  // Pen to use for drawing border
                  Pen pen = new Pen(color, borderThickness);

                  // Filling image with random color
                  result.Mutate(operation => operation.Fill(options, color));

                  // If border is visible
                  if (borderThickness > 0)
                  {
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
                  return Task.FromResult(result);
            }

            /// <summary>
            /// Creates demonstration image that contains three versions of image: initial, mask and the result
            /// </summary>
            /// <param name="initialImage">Initial image</param>
            /// <param name="maskImage"> Mask image</param>
            /// <param name="resultImage">Result</param>
            public static Task<Image> CreateDemonstrationImageAsync(params Image[] images)
            {
                  // Getting size of the grid to insert images to
                  int gridSize = 0;
                  do
                        gridSize++;
                  while (Math.Pow(gridSize, 2) < images.Length);

                  // Getting size of the result image
                  Image demonstrationImage = GetBlankDemonstrationImage(gridSize, images.Select(x => x.Size()));
                  // Offset for images to draw
                  Point offset = new Point(0, 0);

                  // Drawing all images on the demonstration image
                  for (int i = 0; i < gridSize; i++)
                  {
                        for (int j = 0; j < gridSize; j++)
                        {
                              // Index of current image
                              int currentImageIndex = i * gridSize + j;
                              // If current line could not be filled, break the cycle
                              if (currentImageIndex > images.Length - 1)
                                    break;
                              // Drawing current image on the demonstration image
                              demonstrationImage.Mutate(operation => operation.DrawImage(images[currentImageIndex], offset, 1f));
                              // Increasing horizontal offset to width of all images drawn in current line
                              offset.X += images[currentImageIndex].Width;
                        }

                        // Nullifying horizontal offset
                        offset.X = 0;
                        // If there is no other elements, break the cycle
                        if (i * gridSize + gridSize >= images.Length)
                              break;
                        // Increasing vertical offset to max of heights of images for current line
                        offset.Y += images.Skip(i * gridSize).Take(gridSize).Max(x => x.Height);
                  }

                  return Task.FromResult(demonstrationImage);
            }

            /// <summary>
            /// Creates demonstration image based on size of all images
            /// </summary>
            /// <param name="imagesSize">Sizes of all images</param>
            /// <returns>Blank image to draw inner images on</returns>
            private static Image GetBlankDemonstrationImage(int gridSize, IEnumerable<Size> imagesSize)
            {
                  // Width of the image
                  int width = imagesSize.OrderByDescending(x => x.Width).Take(gridSize).Sum(x => x.Width);
                  // Height of the image
                  int height = imagesSize.OrderByDescending(x => x.Height).Take(gridSize).Sum(x => x.Height);

                  // TODO:
                  // Cut unnecessary height

                  // Returning blank image
                  return new Image<Rgba32>(width, height, Color.White);
            }
      }
}
