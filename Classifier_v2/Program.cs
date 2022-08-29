using Classifier_v2;

string filePath = @"C:\Users\eskw\Documents\Privat\C#\Naive Bayes Classifier v1\data\tweets.csv";
Console.WriteLine(filePath);
List<Tweet> tweets = Tweets.FromCSV(filePath);
Console.WriteLine(tweets.Count);

int guessed = 0;
int total = 0;

foreach (Tweet tweet in tweets)
{
    total++;
    Priors.AddTweet(tweet);
    Prediction prediction = Prediction.PredictSentiments(tweet);

    if (prediction.OrderedSentiments.First() == tweet.Sentiment) guessed++;

    if (total % 100 == 0)
    {
        double successRate = 1d * guessed / total * 100;
        Console.WriteLine($"{successRate:0.#}% (correctly classified {guessed} of {total} tweets)");
        Console.WriteLine(prediction);
        Console.WriteLine("-------------------------------------------------------");
    }
}