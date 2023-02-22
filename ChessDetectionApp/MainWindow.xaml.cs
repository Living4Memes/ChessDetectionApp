using ChessDetectonLib;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace ChessDetectionApp
{
      /// <summary>
      /// Логика взаимодействия для MainWindow.xaml
      /// </summary>
      public partial class MainWindow : Window
      {
            public MainWindow()
            {
                  InitializeComponent();
            }

            private void Window_Loaded(object sender, RoutedEventArgs e)
            {

            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                  Directory.GetFiles(@"C:\Users\Defender\Source\Repos\Living4Memes\ChessDetectionApp\ChessDetectonLib\Demos\DemoResult\").ToList().ForEach(x => File.Delete(x));

                  for (int i = 0; i < 10; i++)
                  {
                        HighlighterImage highlighter = new HighlighterImage(
                              HighlighterImageProperties.DefaultInstance);

                        highlighter.Width *= i + 1;
                        highlighter.Height *= i + 1;
                        highlighter.BorderThickness += 10 * i;

                        Image initial = Image.Load(@"C:\Users\Defender\Source\Repos\Living4Memes\ChessDetectionApp\ChessDetectonLib\Demos\DemoSource\ChessDemo.png");

                        highlighter.DrawOverImage(initial, new SixLabors.ImageSharp.Point(i*10, i*10));

                        initial.SaveAsPng($@"C:\Users\Defender\Source\Repos\Living4Memes\ChessDetectionApp\ChessDetectonLib\Demos\DemoResult\HighlighterImageResult-{i}.png");
                  }

                  Environment.Exit(0);
            }
      }
}
