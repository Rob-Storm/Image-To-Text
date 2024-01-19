using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
public enum FileType
{
    PNG,
    TXT
}

namespace ImageToText
{
    public class ImageTextEncoder
    {
        public static string EncodeImageAsString(string ImagePath, string OutputPath)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            if(CheckPath(FileType.PNG, ImagePath) && Path.Exists(OutputPath))
            {
                using (StreamWriter writeText = new StreamWriter(@$"{OutputPath}\output.txt"))
                using (Image<Rgba32> image = Image.Load<Rgba32>(ImagePath))
                {
                    for (int h = 0; h < image.Height; h++)
                    {
                        for (int w = 0; w < image.Width; w++)
                        {
                            Rgba32 pixelColor = image[w, h];

                            byte red, green, blue, alpha;

                            red = pixelColor.R;
                            green = pixelColor.G;
                            blue = pixelColor.B;
                            alpha = pixelColor.A;

                            byte[] colorChannelArray = { red, green, blue, alpha };

                            writeText.WriteLine(Convert.ToHexString(colorChannelArray));
                        }
                    }
                }

                stopwatch.Stop();

                MessageBox.Show($"Image at {ImagePath} has been converted to text. Elapsed Time: {stopwatch.ElapsedMilliseconds} miliseconds", "Image Converted", MessageBoxButton.OK, MessageBoxImage.Information);

                return string.Empty;
            }
            else
            {
                MessageBox.Show("One or more paths are not valid!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                return string.Empty;
            }

        }

        public static Image DecodeImageAsString(string TextPath, string OutputPath)
        {
            if(CheckPath(FileType.TXT ,TextPath) && Path.Exists(OutputPath))
            {
                const int RED_CHANNEL_OFFSET = 0, GREEN_CHANNEL_OFFSET = 2, BLUE_CHANNEL_OFFSET = 4, ALPHA_CHANNEL_OFFSET = 6, BYTE_LENGTH = 2;

                const int IMAGE_WIDTH = 128, IMAGE_HEIGHT = 128;

                using (StreamReader reader = new StreamReader(TextPath))
                using (Image<Rgba32> Image = new Image<Rgba32>(128, 128))
                {

                    const int COLOR_ARRAY_SIZE = IMAGE_WIDTH * IMAGE_HEIGHT;

                    byte[] colorArray = new byte[COLOR_ARRAY_SIZE];

                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();

                    for (int h = 0; h < IMAGE_HEIGHT; h++)
                    {
                        for (int w = 0; w < IMAGE_WIDTH; w++)
                        {
                            if (!string.IsNullOrEmpty(reader.ReadLine()))
                            {
                                string? hexColorString = reader.ReadLine();
                                Rgba32 pixelColor;

                                byte red, green, blue, alpha;

                                red = Encoding.ASCII.GetBytes(hexColorString!.Substring(RED_CHANNEL_OFFSET, BYTE_LENGTH))[0];
                                green = Encoding.ASCII.GetBytes(hexColorString!.Substring(GREEN_CHANNEL_OFFSET, BYTE_LENGTH))[0];
                                blue = Encoding.ASCII.GetBytes(hexColorString!.Substring(BLUE_CHANNEL_OFFSET, BYTE_LENGTH))[0];
                                alpha = Encoding.ASCII.GetBytes(hexColorString!.Substring(ALPHA_CHANNEL_OFFSET, BYTE_LENGTH))[0];

                                pixelColor = new Rgba32(red, green, blue, alpha);

                                Image[w, h] = pixelColor;
                            }
                        }
                    }

                    Image.Save(@$"{OutputPath}\output.png");

                    stopwatch.Stop();

                    MessageBox.Show($"Text file at {TextPath} has been converted to an Image. Elapsed Time: {stopwatch.ElapsedMilliseconds} miliseconds", "Text File Converted", MessageBoxButton.OK, MessageBoxImage.Information);

                    return Image;
                }
               
            }
            else
            {
                MessageBox.Show("One or more paths are not valid!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                return null;
            }
        }

        internal static bool CheckPath(FileType fileType, string path)
        {
            string pathFileType = string.Empty;

            switch(fileType)
            {
                case FileType.PNG:
                    pathFileType = ".png";
                    break;

                case FileType.TXT:
                    pathFileType = ".txt";
                    break;
                default:
                    pathFileType = string.Empty; 
                    break;

            }

            return string.Compare(Path.GetExtension(path), pathFileType, StringComparison.OrdinalIgnoreCase) == 0;
        }

    }
}
