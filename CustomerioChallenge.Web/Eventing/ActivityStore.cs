using CustomerioChallenge.Eventing.EventModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerioChallenge.Eventing
{
    public class ActivityStore
    {
        //public static LinkedList<Activity> Data { get; set; }
        
        public static void Load(string path)
        {
            //Data = new LinkedList<Activity>();
            var jsonRows = File.ReadLines(path);
            foreach (var jsonRow in jsonRows)
            {
                var parsedObject = JsonConvert.DeserializeObject<JObject>(jsonRow);
                if (parsedObject["user_id"] == null)
                {
                    continue;
                }
                
                Activity activity = GetActivity(parsedObject);
                activity?.Store();
                //Data.AddLast(activity);
            }
        }

        public static void AddAttributeChangeActivity(int customerId, IDictionary<string, string> attributes)
        {
            AttributeChangeActivity attributeChangeActivity = new AttributeChangeActivity
            {
                Data = attributes,
                Id = Guid.NewGuid().ToString(),
                User_id = customerId,
                Timestamp = GetCurrentEpochTime()
            };

            //could raise an event and store will happen there but not a problem for now
            attributeChangeActivity.Store();
        }

        private static long GetCurrentEpochTime()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        private static Activity GetActivity(JObject parsed)
        {
            var type = parsed["type"].ToString();
            Activity activity = null;
            switch (type)
            {
                case "attributes":
                    activity = parsed.ToObject<AttributeChangeActivity>();
                    break;
                case "event":
                    activity = parsed.ToObject<OneTimeActivity>();
                    break;
                default:
                    break;
            }

            return activity;
        }
    }
}
