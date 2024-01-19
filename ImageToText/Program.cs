namespace ImageToText
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose an Image Path.");
            string? imagePath = Console.ReadLine();

            Console.WriteLine("Choose an Output Path.");
            string? outputPath = Console.ReadLine();

            if(!string.IsNullOrEmpty(imagePath) && !string.IsNullOrEmpty(outputPath))
            {
                ImageTextEncoder.EncodeImageAsString(imagePath, outputPath);
            }
        }
    }
}
