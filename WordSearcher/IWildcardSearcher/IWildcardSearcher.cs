using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;


namespace WordSearcher.IWildcardSearcher
{
    interface IWildcardSearcher
    {
        void AddWord(string word); // добавление слова в словарь
        IEnumerable<string> SearchWords(string pattern); // поиск слов, подходящих под шаблон
    }

    public class WordSearcher : IWildcardSearcher
    {
        private List<string> _words;

        public WordSearcher(string[] words)
        {
            _words = new List<string>();
            for(int i = 0; i < words.Length; i++)
            {
                AddWord(words[i]);
            }
        }

        public void AddWord(string word)
        {
            _words.Add(word);
        }

        public IEnumerable<string> SearchWords(string pattern)
        {
            List<string> newWords = new List<string>();
            foreach (string word in _words)
            {
                if(WildCard.Match(word, pattern))
                {
                    newWords.Add(word);
                }
            }

            return newWords;
        }  
    }
}
