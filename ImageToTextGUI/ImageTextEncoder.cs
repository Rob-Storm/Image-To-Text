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

            using(StreamWriter writeText = new StreamWriter(@$"{OutputPath}\output.txt"))
            using(Image<Rgba32> image = Image.Load<Rgba32>(ImagePath))
            {
                for(int h = 0; h < image.Height; h++) 
                {
                    for(int w = 0; w < image.Width; w++) 
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

        public static Image<Rgba32> DecodeImageAsString(string TextPath)
        {
            const int RED_OFFSET = 0, GREEN_OFFSET = 2, BLUE_OFFSET = 4, ALPHA_OFFSET = 6, HEX_LENGTH = 8;

            using(StreamReader reader = new StreamReader(TextPath)) 
            using(Image<Rgba32> Image = new Image<Rgba32>(128, 128))
            {
                byte[] colorArray = new byte[128^2];

                for(int h = 0; h < 128; h++)
                {
                    for (int w = 0; w < 128; w++)
                    {
                        if(!string.IsNullOrEmpty(reader.ReadLine()))
                        {
                            string? hexColor = reader.ReadLine();


                            /*
                            byte red, green, blue, alpha;

                            red = Encoding.ASCII.GetBytes(hexColor!.Substring(RED_OFFSET, HEX_LENGTH))[0];
                            green = Encoding.ASCII.GetBytes(hexColor!.Substring(GREEN_OFFSET, HEX_LENGTH))[0];
                            blue = Encoding.ASCII.GetBytes(hexColor!.Substring(BLUE_OFFSET, HEX_LENGTH))[0];
                            alpha = Encoding.ASCII.GetBytes(hexColor!.Substring(ALPHA_OFFSET, HEX_LENGTH))[0];
                            */
                        }

                    }
                }

                return Image;
            }
        }

        private bool CheckPath(FileType fileType, string path)
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
            }

            return string.Compare(Path.GetExtension(path), pathFileType, StringComparison.OrdinalIgnoreCase) == 0;
        }

    }
}
