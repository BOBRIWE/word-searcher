using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace WordSearcher
{
    public class WildCard
    {
        private static int _originalPatternLength;

        private static List<List<bool>> _lookup;

        private static char[] _str;
        private static char[] _pattern;
        

        public static string CleanPattern(string pattern)
        {
            char[] p = pattern.ToCharArray();

            // a**b***c --> a*b*c
            int writeIndex = 0;
            int cutLength = 0;
            bool isFirst = true;
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i] == '*')
                {
                    if (isFirst)
                    {
                        p[writeIndex++] = p[i];
                        isFirst = false;
                    } else
                    {
                        cutLength++;
                    }
                }
                else
                {
                    p[writeIndex++] = p[i];
                    isFirst = true;
                }
            }

            _originalPatternLength = writeIndex;

            string newPattern = new string(p);

            return newPattern.Substring(0, newPattern.Length - cutLength);
        }

        public static bool Match(string str, string pattern)
        {
            _str = str.ToCharArray();
            _pattern = CleanPattern(pattern).ToCharArray();

            _lookup = new List<List<bool>>();
            for (int i = 0; i < str.Length + 1; i++)
            {
                _lookup.Add(new List<bool>());
                for (int j = 0; j < _originalPatternLength + 1; j++)
                {
                    _lookup[i].Add(false);
                }
            }

            _lookup[0][0] = true;

            for (int i = 1; i < _lookup.Count; i++)
            {
                for (int j = 1; j < _lookup[0].Count; j++)
                {
                    switch(_pattern[j - 1])
                    {
                        case '?':
                            ExclamationPattern(i, j);
                            break;
                        case '*':
                            StarPattern(i, j);
                            break;
                        default:
                            DefaultPattern(i, j);
                            break;
                    }
                }
            }

            return _lookup[str.Length][pattern.Length];
        }

        private static void StarPattern(int i, int j)
        {
            if (_originalPatternLength > 0 && _pattern[0] == '*')
            {
                _lookup[0][1] = true;
            }

            _lookup[i][j] = _lookup[i - 1][j] || _lookup[i][j - 1];
        }

        private static void ExclamationPattern(int i, int j)
        {
            _lookup[i][j] = _lookup[i - 1][j - 1];
        }

        private static void DefaultPattern(int i, int j)
        {
            if(_str[i - 1] == _pattern[j - 1])
            {
                _lookup[i][j] = _lookup[i - 1][j - 1];
            }
        }
    }
}
