using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WordSearcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WordSearcher.IWildcardSearcher.WordSearcher wcs;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            WordSearcher.DictionaryHolder.DictionaryHolder file = null;
            if (result == true)
            {
                string[] words;

                string ext = System.IO.Path.GetExtension(dlg.FileName);
                switch (ext)
                {
                    case ".txt":
                        file = new WordSearcher.DictionaryHolder.TXTFile(dlg.FileName);
                        break;
                    default:
                        break;
                }

                words = file.GetWordsArr();

                wcs = new WordSearcher.IWildcardSearcher.WordSearcher(words);
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (wcs == null)
            {
                MessageBox.Show("Select dictionary first!\nFile > Load dictionary...");
                return;
            }

            lstBox.Items.Clear();

            string text = SanitizeInput(txtBox.Text);
            txtBox.Text = text;

            IEnumerable<string> list = new List<string>();

            list = wcs.SearchWords(text);

            foreach(string word in list)
            {
                lstBox.Items.Add(word);
            }

            // MessageBox.Show(text);
        }

        private string SanitizeInput(string text)
        {
            string cleanText = Regex.Replace(text, @"[^А-Яа-я\*\?]+", String.Empty);

            return cleanText;
        }
    }
}
