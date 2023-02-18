using ChessDetectonLib;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Documents;
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
                  
            }

            private async void DoTesting()
            {
                  Image initialImage = Image.Load(@"C:\Users\Defender\source\repos\Living4Memes\ChessDetectionApp\ChessDetectonLib\Demos\DemoSource\ChessDemo.png");
                  Rectangle mask = new Rectangle(100, 100, 50, 50);
                  string[] files = Directory.GetFiles(@"C:\Users\Defender\source\repos\Living4Memes\ChessDetectionApp\ChessDetectonLib\Demos\DemoResult");
                  if (files.Length > 0)
                  {
                        foreach (string path in files)
                              if (path.EndsWith(".png"))
                                    File.Delete(path);
                  }

                  const int countOfImages = 1000;
                  List<Image> images = new List<Image>();
                  for (int i = 0; i < countOfImages; i++)
                  {
                        textblock.Text = $"Drawing {i}/{countOfImages}";
                        Image resultImage = await ImageHighliter.GetHighlighterImage(mask.Width, mask.Height);
                        images.Add(resultImage);
                  }

                  ImageHighliter.CreateDemonstrationImageAsync(images.ToArray()).Result.SaveAsPng(@"C:\Users\Defender\source\repos\Living4Memes\ChessDetectionApp\ChessDetectonLib\Demos\DemoResult\DemonstrationImage.png");

                  Environment.Exit(0);
            }

            private void button_Click(object sender, RoutedEventArgs e)
            {
                  DoTesting();
            }
      }
}
