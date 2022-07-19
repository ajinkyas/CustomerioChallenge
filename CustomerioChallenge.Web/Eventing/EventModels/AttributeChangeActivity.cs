using CustomerioChallenge.Summarization;
using CustomerioChallenge.Summarization.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerioChallenge.Eventing.EventModels
{
    public class AttributeChangeActivity : Activity
    {
        public override void Store()
        {
            if (DataReadStore.HasCustomer(User_id))
            {
                DataWriteStore.UpdateCustomer(User_id, Data, Timestamp);
            }
            else
            {
                DataWriteStore.AddCustomer(User_id, Data, Timestamp);
            }
        }
    }
}
