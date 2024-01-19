namespace ImageToText
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose an Image Path.");

            string? imagePath = Console.ReadLine();

            if(!string.IsNullOrEmpty(imagePath))
            {
                ImageTextEncoder.EncodeImageAsString(imagePath);
            }
        }
    }
}
