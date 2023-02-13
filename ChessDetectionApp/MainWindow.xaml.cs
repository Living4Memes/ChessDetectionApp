using ChessDetectonLib;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = SixLabors.ImageSharp.Color;

namespace ChessDetectionApp
{
      /// <summary>
      /// Логика взаимодействия для MainWindow.xaml
      /// </summary>
      public partial class MainWindow : Window
      {
            public BitmapImage BG { get; private set; }
            public MainWindow()
            {
                  InitializeComponent();
            }

            private void Window_Loaded(object sender, RoutedEventArgs e)
            {
                  Image initialImage = Image.Load(@"C:\Users\vladk\Downloads\Chess.jpg");
                  RectangleF mask = new RectangleF(1142, 72, 100, 230);
                  Image resultImage = ImageHighliter.Highlight(initialImage, mask);

                  BG = new BitmapImage(new Uri(@"C:\Users\vladk\source\repos\ChessDetectionApp\ChessDetectionApp\bin\Debug\demonstration.png"));
                  image.Source = BG;
            }
      }
}
