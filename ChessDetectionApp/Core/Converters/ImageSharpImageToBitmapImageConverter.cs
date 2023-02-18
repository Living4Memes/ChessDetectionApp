using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ChessDetectionApp
{
    public class ImageSharpImageToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If input value is not an image from ImageSharp, throw an exception
            if (value.GetType() != typeof(Image<Rgba32>))
                throw new ArgumentException("Input parameter is not an ImageSharp Image", nameof(value));

            // Converting the image to byte array
            var iMemoryGroup = ((Image<Rgba32>)value).GetPixelMemoryGroup();
            var memoryGroup = iMemoryGroup.ToArray()[0];
            byte[] pixelData = MemoryMarshal.AsBytes(memoryGroup.Span).ToArray();


            // Creating BitmapImage from byte array
            BitmapImage image = new BitmapImage();
            using (var ms = new MemoryStream(pixelData))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.Default;
                image.StreamSource = ms;
                image.EndInit();
            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
