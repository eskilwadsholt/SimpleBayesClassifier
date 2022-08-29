using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier_v2
{
    internal class Tweets
    {
        public static List<Tweet> FromCSV(string filePath)
        {
            List<Tweet> result = new List<Tweet>();

            using (var reader = new StreamReader(filePath))
            {
                StringBuilder sb = new StringBuilder();

                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();

                    if (line == null) continue;

                    Tweet? tweet = null;

                    if (line.Count(x => x == '"') > 1) tweet = Extract(line);
                    else
                    {
                        sb.Append(line);
                        if (sb.ToString().Count(x => x == '"') > 1) tweet = Extract(sb.ToString());
                    }

                    if (tweet != null)
                    {
                        result.Add(tweet);
                        sb = new StringBuilder();
                    }
                }
            }

            return result;
        }

        private static Tweet? Extract(string? line)
        {
            if (line == null) return null;

            string[] headersAndContent = line.Split('"');

            if (headersAndContent.Length < 2) return null;

            string[] headers = headersAndContent[0].Split(',');

            if (headers.Length < 3) return null;

            Tweet tweet = new Tweet();
            tweet.Id = headers[0];
            tweet.Entity = headers[1];
            tweet.Sentiment = headers[2];
            tweet.Content = headersAndContent[1];

            return tweet;
        }
    }
}
