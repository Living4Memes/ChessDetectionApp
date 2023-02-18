using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessDetectonLib
{
      internal static class Debugging
      {
            /// <summary>
            /// Creates demonstration image that contains three versions of image: initial, mask and the result
            /// </summary>
            /// <param name="initialImage">Initial image</param>
            /// <param name="maskImage"> Mask image</param>
            /// <param name="resultImage">Result</param>
            internal static Task<Image> CreateDemonstrationImageAsync(params Image[] images)
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
