﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Diagnostics;
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

            Console.WriteLine($"Image at {ImagePath} has been converted to text");
            Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds} miliseconds");

            return string.Empty;
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
