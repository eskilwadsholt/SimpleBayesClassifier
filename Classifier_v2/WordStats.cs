using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier_v2
{
    internal class WordStats
    {
        public int TweetCount { get; set; } = 0;
        public int WordCount { get; set; } = 0;
        public Dictionary<string, int> Words { get; } = new Dictionary<string, int>();

        internal void AddWords(string[] words)
        {
            WordCount += words.Length;

            foreach (var word in words)
            {
                if (!Words.ContainsKey(word)) Words.Add(word, 0);

                Words[word]++;
            }
        }

        public static string[] GetWordsFrom(Tweet? tweet)
        {
            if (tweet == null) return new string[0];

            return tweet.Content.ToLower().Split(' ');
        }
    }
}
