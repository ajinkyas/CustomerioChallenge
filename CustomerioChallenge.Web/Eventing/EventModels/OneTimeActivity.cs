using CustomerioChallenge.Summarization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerioChallenge.Eventing.EventModels
{
    public class OneTimeActivity : Activity
    {
        public string Name { get; set; }

        public override void Store()
        {
            if (!DataReadStore.HasCustomer(User_id))
            {
                DataWriteStore.AddCustomer(User_id, new Dictionary<string, string>(), Timestamp);
            }

            DataWriteStore.AddOrUpdateOneTimeActivityToCustomer(Id, Name, User_id);
        }
    }
}
