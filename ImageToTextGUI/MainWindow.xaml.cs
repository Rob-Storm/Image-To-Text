using ImageToText;
using System.Windows;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace ImageToTextGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void DecodeButton_Click(object sender, RoutedEventArgs e)
        {
            //ImageElement.Source = ImageTextEncoder.DecodeImageAsString(FilePathTextBox.Text); 
        }

        private void EncodeButton_Click(object sender, RoutedEventArgs e)
        {
            ImageTextEncoder.EncodeImageAsString(FilePathTextBox.Text, OutputPathTextBox.Text);
        }
    }
}