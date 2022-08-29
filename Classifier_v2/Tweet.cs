using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier_v2
{
    internal class Tweet
    {
        public string Id { get; set; }
        public string Entity { get; set; }
        public string Sentiment { get; set; }
        public string Content { get; set; }
    }
}
