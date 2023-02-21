using SixLabors.ImageSharp;
using PropertyChanged;
using System.ComponentModel;
using System;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Fonts;
using System.Linq;

namespace ChessDetectonLib
{
      [AddINotifyPropertyChangedInterface]
      /// <summary>
      /// Image to draw over initial image to highlight something
      /// </summary>
      public sealed class HighlighterImage : INotifyPropertyChanged
      {
            #region === Events ===

            public event PropertyChangedEventHandler PropertyChanged;

            #endregion

            #region === Public properties ===

            /// <summary>
            /// Size of the highlighter image
            /// </summary>
            public Size Size { get; set; }

            /// <summary>
            /// Width of the highlighter image
            /// </summary>
            public int Width { get; set; }

            /// <summary>
            /// Height of the highlighter image
            /// </summary>
            public int Height { get; set; }

            /// <summary>
            /// Color of the Image
            /// </summary>
            public Color ImageColor { get; set; }

            /// <summary>
            /// Opacity of the background
            /// </summary>
            public float Opactiy { get; set; }

            /// <summary>
            /// Thickness of the border
            /// </summary>
            public float BorderThickness { get; set; }

            /// <summary>
            /// Color of the border
            /// </summary>
            public Color BorderColor { get; set; }

            /// <summary>
            /// Title in the top-left corner
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// Color of the title text
            /// </summary>
            public Color ForegroundColor { get; set; }

            /// <summary>
            /// Highlighter image to use
            /// </summary>
            public Image Image { get; private set; }

            #endregion

            #region === Private members ===

            /// <summary>
            /// Is used to stop PropertyChanged event from firing until constructor is finished
            /// </summary>
            private bool _constructorFinished = false;

            /// <summary>
            /// Default font family for the title text
            /// </summary>
            private static FontFamily _titleFontFamily = SystemFonts.Get("Roboto");

            #endregion

            #region === Constructor ===

            /// <summary>
            /// Creates new instance of <see cref="HighlighterImage"/>
            /// </summary>
            /// <param name="properties">Properties of the highlighter image</param>
            public HighlighterImage(HighlighterImageProperties properties)
            {
                  Size = properties.Size;
                  Width = properties.Width;
                  Height = properties.Height;
                  ImageColor = properties.ImageColor;
                  Opactiy = properties.Opactiy;
                  BorderThickness = properties.BorderThickness;
                  BorderColor = properties.BorderColor;
                  Title = properties.Title;
                  ForegroundColor = properties.ForegroundColor;

                  // Drawing the Image property
                  UpdateHighlighterImage();

                  _constructorFinished = true;
            }

            /// <summary>
            /// Private constructor for static functions that return instances of <see cref="HighlighterImage"/>
            /// </summary>
            private HighlighterImage()
            {

            }

            #endregion

            #region === Public functions ===

            /// <summary>
            /// Creates a new instance of <see cref="HighlighterImage"/> for input image
            /// </summary>
            /// <param name="image">Image to generate <see cref="HighlighterImage"/> to</param>
            /// <param name="properties">Properties of the highlighter image</param>
            /// <returns><see cref="HighlighterImage"/></returns>
            public static HighlighterImage GetForImage(Image image, HighlighterImageProperties? properties = null)
            {
                  return new HighlighterImage(properties == null ? new HighlighterImageProperties(image.Width, image.Height) : 
                        (HighlighterImageProperties)properties);
            }

            /// <summary>
            /// Creates a new instance of <see cref="HighlighterImage"/> for input image
            /// </summary>
            /// <param name="image">Image to generate <see cref="HighlighterImage"/> to</param>
            /// <param name="title">Text of the title</param>
            /// <returns><see cref="HighlighterImage"/></returns>
            public static HighlighterImage GetForImage(Image image, string title = "") =>
                  GetForImage(image, new HighlighterImageProperties(image.Width, image.Height, title: title));

            /// <summary>
            /// Draws <see cref="Image"/> over input image
            /// </summary>
            /// <param name="initialImage">Image to draw on</param>
            /// <param name="location">Location to draw highlighter image</param>
            public void DrawOverImage(Image initialImage, Point location)
            {
                  // Drawing the highlighter image over initial image
                  initialImage.Mutate(operation => operation.DrawImage(Image, location, 1f));
            }

            #endregion

            #region === Private functions ===

            /// <summary>
            /// Redaraws the Image property when other property was changed
            /// </summary>
            private void UpdateHighlighterImage()
            {
                  // Result image
                  Image result = new Image<Rgba32>(Width, Height);
                  // Options to fill the image
                  DrawingOptions options = new DrawingOptions() { GraphicsOptions = new GraphicsOptions() { BlendPercentage = 0.3f } };
                  // Getting color for the image
                  Color backgroundColor = ColorsProvider.NextRandomColor;

                  // Filling image with random color
                  result.Mutate(operation => operation.Fill(options, backgroundColor));

                  // If border is visible
                  if (BorderThickness > 0)
                  {
                        // Pen to use for drawing border
                        Pen pen = new Pen(ColorsProvider.NextRandomColor, BorderThickness);
                        // Points of the shape of the border
                        PointF[] borderPoints = new PointF[5]
                        {
                              new PointF(0, 0),
                              new PointF(0, Height -1f),
                              new PointF(Width - 1f, Height  - 1f),
                              new PointF(Width - 1f, 0),
                              new PointF(0, 0),
                        };

                        // Drawing the border
                        result.Mutate(operation => operation.DrawPolygon(pen, borderPoints));
                  }

                  if (!string.IsNullOrEmpty(Title))
                  {
                        // Font for the title text
                        Font titleFont = new Font(SystemFonts.Families.Single(x => x.Name == "Courier New"), 14f);

                        // Drawing title
                        result.Mutate(operation => operation.DrawText(Title, titleFont, ColorsProvider.GetReadableForegroundColor(backgroundColor), new PointF(BorderThickness, BorderThickness)));
                  }

                  // Returning the image
                  Image =  result;
            }

            // TODO:
            // Text drawing func
            // Maybe add font property, but I think I'll make it constant;

            /// <summary>
            /// Calculates size of rectangle to insert input text to
            /// </summary>
            /// <param name="imageWidth">Width of the image</param>
            /// <param name="imageHeight">Height of the image</param>
            /// <param name="borderThickness">Thickness of the border of the image</param>
            /// <param name="font">Font of the title text</param>
            /// <param name="text">Text of the title</param>
            /// <param name="desiredFontSize">Desired size of the font</param>
            /// <param name="verticalMargin">Top and bottom margin of the text</param>
            /// <returns></returns>
            private static Rectangle GetTextRectangle(int imageWidth, int imageHeight, float borderThickness, Font font, string text, float desiredFontSize = 14f, float verticalMargin = 1f)
            {
                  // Container of the title text
                  FontRectangle textContainer = TextMeasurer.Measure(text, new TextOptions(font));
                  // Default size and location of the text container
                  RectangleF textContainter = new RectangleF(borderThickness, borderThickness + verticalMargin, imageWidth / 2, 20);

                  throw new NotImplementedException();
            }

            /// <summary>
            /// Being triggered by <see cref="PropertyChanged" event/>
            /// </summary>
            private void OnPropertyChanged(object o, PropertyChangedEventArgs args)
            {
                  // If the property that was changed is Image, return (Image setter is private)
                  if (args.PropertyName == nameof(Image))
                        return;

                  // Validating value of the changed property
                  ValidateProperty(args.PropertyName);

                  // If constructor is finished
                  if(_constructorFinished)
                        // Redrawing the highlighter image
                        UpdateHighlighterImage();
            }

            /// <summary>
            /// Valedates all known properties
            /// </summary>
            /// <param name="propertyName">Name of the property to validate</param>
            /// <exception cref="ArgumentException"></exception>
            /// <exception cref="NotImplementedException"></exception>
            private void ValidateProperty(string propertyName)
            {
                  switch (propertyName)
                  {
                        case nameof(Size):
                              {
                                    if (Size.Width <= 0 || Size.Height <= 0)
                                          throw new ArgumentException("Width and Height of Size must be bigger than 0", nameof(Size));
                                    break;
                              }
                        case nameof(Width):
                              {
                                    if (Width <= 0)
                                          throw new ArgumentException("Width must be bigger than 0", nameof(Width));
                                    break;
                              }
                        case nameof(Height):
                              {
                                    if (Width <= 0)
                                          throw new ArgumentException("Height must be bigger than 0", nameof(Height));
                                    break;
                              }
                        case nameof(Opactiy):
                              {
                                    if (Opactiy < 0 || Opactiy > 1)
                                          throw new ArgumentException("Opacity must be between 0 and 1", nameof(Opactiy));
                                    break;
                              }
                        case nameof(BorderThickness):
                              {
                                    if (BorderThickness < 0)
                                          throw new ArgumentException("Thickness of the border must be bigger than 0", nameof(BorderThickness));
                                    else if (BorderThickness * 2 > Width)
                                          throw new ArgumentException("Width of the border could not be bigger than width of the image", nameof(BorderThickness));
                                    else if (BorderThickness * 2 > Height)
                                          throw new ArgumentException("Hieght of the border could not be bigger than height of the image", nameof(BorderThickness));

                                    break;
                              }
                        default:
                              {
                                    // If the property that was changed does not exist in class atm, throw exception. Made this to make sure I will not forget anything.
                                    if (propertyName != nameof(BorderColor) && propertyName != nameof(ForegroundColor) &&
                                          propertyName != nameof(ImageColor) && propertyName != nameof(Title))
                                          throw new NotImplementedException($"Property \"{propertyName}\" was not found and could not be validated.");

                                    break;
                              }
                  }
            }

            #endregion
      }
}
