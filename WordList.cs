using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessTheLetter
{
    public class WordList
    {
        private readonly List<string> _words;
        private readonly Random _random = new Random();

        public WordList(IEnumerable<string> words)
        {
            _words = new List<string>(words);
        }

        // Get a random word from the given List
        private string GetRandomWord(IList<string> list)
        {
            var i = _random.Next() % list.Count;
            return list[i];
        }

        // Get a random word (of given max length) containing as close as possible to half of the letters from given List
        // Returns null if no such word found
        public string GetRandomWord(IEnumerable<char> letters, int maxLength)
        {
            var lettersSet = new HashSet<char>(letters);

            var subList = _words.Where(w => w.Length <= maxLength).ToList();

            var l = lettersSet.Count / 2;
            if (l > maxLength)
                l = maxLength;

            while (l > 0)
            {
                var matches = subList.Where(w => lettersSet.Intersect(w).Count() == l).ToList();

                if (matches.Count > 0)
                    return GetRandomWord(matches);
    
                 --l;
            }

            return null;
        }

    }
}
