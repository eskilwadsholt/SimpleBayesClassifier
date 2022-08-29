using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier_v2
{
    internal class Priors
    {
        public static int TotalTweetCount { get; set; } = 0;
        public static Dictionary<string, WordStats> Sentiments { get; } = new Dictionary<string, WordStats>();

        public static void AddTweet(Tweet? tweet)
        {
            if (tweet == null) return;

            if (!Sentiments.ContainsKey(tweet.Sentiment)) Sentiments[tweet.Sentiment] = new WordStats();

            var words = WordStats.GetWordsFrom(tweet);
            if (words == null || words.Length == 0) return;
                
            TotalTweetCount++;
            Sentiments[tweet.Sentiment].TweetCount++;
            Sentiments[tweet.Sentiment].AddWords(words);
        }
    }
}
