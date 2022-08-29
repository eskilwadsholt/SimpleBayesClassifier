using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Classifier_v2
{
    internal class Prediction
    {
        public Dictionary<string, double> Probabilities { get; set; } = new Dictionary<string, double>();
        public List<string> OrderedSentiments { get; set; } = new List<string>();
        public Tweet? Tweet { get; set; }

        public override string ToString()
        {
            if (Tweet == null) return string.Empty;

            StringBuilder sb = new StringBuilder()
                .Append(Tweet.Sentiment)
                .Append(":\t")
                .AppendLine(Tweet.Content);

            foreach (string sentiment in OrderedSentiments)
            {
                sb.AppendLine($"\t{sentiment}:\t{Probabilities[sentiment]:0.######}");
            }

            return sb.ToString();
        }

        public static Prediction PredictSentiments(Tweet? tweet)
        {
            Prediction result = new Prediction();

            if (tweet != null) result.Tweet = tweet;

            foreach (var sentiment in Priors.Sentiments.Keys)
            {
                double sentimentPrior = Math.Log(Priors.Sentiments[sentiment].TweetCount + 1) - Math.Log(Priors.TotalTweetCount + 1);
                double wordsGivenSentiment = 0;

                var words = WordStats.GetWordsFrom(tweet);
                if (words != null && words.Length > 1)
                {
                    foreach (var word in words) wordsGivenSentiment += WordGivenSentiment(word, sentiment);
                }

                result.Probabilities[sentiment] = sentimentPrior + wordsGivenSentiment;
            }

            result.OrderedSentiments = (from x in result.Probabilities orderby x.Value descending select x.Key).ToList();

            return result;
        }

        private static double WordGivenSentiment(string word, string sentiment)
        {
            if (!Priors.Sentiments.ContainsKey(sentiment)) return 0;

            double result = -Math.Log(Priors.Sentiments[sentiment].WordCount + 1);

            if (Priors.Sentiments[sentiment].Words.ContainsKey(word)) result += Math.Log(Priors.Sentiments[sentiment].Words[word] + 1);

            return result;
        }
    }
}
