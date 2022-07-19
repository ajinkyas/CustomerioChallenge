using CustomerioChallenge.Summarization.DomainModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerioChallenge.Summarization
{
    public class DataWriteStore
    {
        internal static void AddOrUpdateOneTimeActivityToCustomer(string activityId, string oneTimeActivityName, int customerId)
        {
            var customerToUpdate = DataStore.Customers[customerId];
            if (customerToUpdate.OneTimeActivities.ContainsKey(oneTimeActivityName))
            {
                customerToUpdate.OneTimeActivities[oneTimeActivityName].Add(activityId);
            }
            else
            {
                customerToUpdate.OneTimeActivities[oneTimeActivityName] = new HashSet<string> { activityId };
            }
        }
        
        internal static void DeleteCustomer(int customerId)
        {
            DataStore.Customers.Remove(customerId);
        }

        internal static void UpdateCustomer(int customerId, IDictionary<string, string> attributes, long timestamp)
        {
            Customer customerToUpdate = DataStore.Customers[customerId];
            foreach (var attribute in attributes)
            {
                if (attribute.Value is null)
                {
                    //assuming that we set attribute value to null to remove that attr
                    RemoveAttribute(attribute.Key, customerToUpdate, timestamp);
                    continue;
                }

                if (customerToUpdate.AttributesInfo.ContainsKey(attribute.Key))
                {
                    if (customerToUpdate.AttributesInfo[attribute.Key].LastUpdatedTimestamp < timestamp)
                    {
                        AddOrUpdateAttribute(attribute, customerToUpdate, timestamp);
                    }
                }
                else
                {
                    AddOrUpdateAttribute(attribute, customerToUpdate, timestamp);
                }
            }
        }

        private static void RemoveAttribute(string attributeKey, Customer customerToUpdate, long timestamp)
        {
            customerToUpdate.AttributesInfo.Remove(attributeKey);
            customerToUpdate.UpdatedTimestamp = timestamp;
        }

        internal static void AddCustomer(int customerId, IDictionary<string, string> attributes, long timestamp)
        {
            var newCustomer = new Customer()
            {
                Id = customerId,
                AttributesInfo = new Dictionary<string, TimedValue>(),
                OneTimeActivities = new Dictionary<string, HashSet<string>>(),
                UpdatedTimestamp = timestamp
            };
            foreach (var attribute in attributes)
            {
                if (attribute.Value is not null)
                {
                    newCustomer.AttributesInfo.Add(attribute.Key, new TimedValue() { Value = attribute.Value, LastUpdatedTimestamp = timestamp });
                }
            }
            DataStore.Customers.Add(newCustomer.Id, newCustomer);
        }

        private static void AddOrUpdateAttribute(KeyValuePair<string, string> newAttribute, Customer customerToUpdate, long timestamp)
        {
            customerToUpdate.AttributesInfo[newAttribute.Key] = new TimedValue { Value = newAttribute.Value, LastUpdatedTimestamp = timestamp };
            customerToUpdate.UpdatedTimestamp = timestamp;
        }
    }
}
