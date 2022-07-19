using CustomerioChallenge.Summarization.DomainModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerioChallenge.Summarization
{
    public class DataReadStore
    {
        internal static bool HasCustomer(int user_id)
        {
            return DataStore.Customers.ContainsKey(user_id);
        }

        public static IReadOnlyCollection<Customer> List()
        {
            return new ReadOnlyCollection<Customer>(DataStore.Customers.Values.ToList());
        }

        internal static Customer GetCustomer(int id) {
            if (DataStore.Customers.TryGetValue(id, out Customer customer))
            {
                return customer;
            }

            return null;
        }
    }
}
