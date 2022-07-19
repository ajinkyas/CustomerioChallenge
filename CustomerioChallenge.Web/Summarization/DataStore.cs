using CustomerioChallenge.Summarization.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerioChallenge.Summarization
{
    public class DataStore
    {
        public static IDictionary<int, Customer> Customers { get; private set; } = new Dictionary<int, Customer>();
    }
}
