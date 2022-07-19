using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerioChallenge.Eventing.EventModels
{
    public abstract class Activity
    {
        public string Id { get; set; }
        public long Timestamp { get; set; }
        public int User_id { get; set; }
        public IDictionary<string,string> Data { get; set; }

        public abstract void Store();
    }
}
