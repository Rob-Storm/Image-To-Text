using System.IO;
using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
public enum FileType
{
    PNG,
    TXT
}

namespace ImageToText
{
    public class ImageTextEncoder
    {
        public static string EncodeImageAsString(string ImagePath)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            using(Image image = Image.Load<Rgba32>(ImagePath))
            {
                Rgba32 color;

                for(int h = 0; h < image.Height; h++) 
                {
                    for(int w = 0; w < image.Width; w++) 
                    {
                       
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
