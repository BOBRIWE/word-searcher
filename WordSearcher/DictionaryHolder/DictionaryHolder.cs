using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace WordSearcher.DictionaryHolder
{
    interface DictionaryHolder
    {
        string[] GetWordsArr();
    }

    public class TXTFile : DictionaryHolder
    {
        private string _fileName;

        public TXTFile(string fileName)
        {
            _fileName = fileName;
        }

        public string[] GetWordsArr()
        {
            List<string> wordsList = new List<string>();

            StreamReader sr = new StreamReader(_fileName, Encoding.Default);

            string line;
            List<string> words;
            
            while ((line = sr.ReadLine()) != null)
            {
                line = Sanitize(line);
                if (line == "") continue;

                words = line.Split(' ').ToList();

                wordsList.AddRange(words);  
            }

            return wordsList.Distinct().ToArray();
        }

        private string Sanitize(string str)
        {
            return Regex.Replace(str, @"[^А-Яа-я\s]+", "");
        }

        public string[] GetWordsArrOld()
        {
            // работает только если текстовый файл сохранен в кодировке системы
            string text = System.IO.File.ReadAllText(_fileName, Encoding.Default);

            // замена переносов строки на пробелы
            text = Regex.Replace(text, @"(\r\n|\r|\n)", " ");

            // очистка всего текста от символов. Остаются только пробелы и кириллица
            string cleanText = Regex.Replace(text, @"[^А-Яа-я\s]+", String.Empty);

            string[] words = cleanText.Split(' ').Distinct().ToArray();

            return words;
        }
    }
}
