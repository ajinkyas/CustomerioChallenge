using CustomerioChallenge.Summarization.DomainModels;
using System.Collections.Generic;

namespace CustomerioChallenge.ViewModels
{
    public class PagedCustomers
    {
        public List<Customer> Customers { get; set; }
        public PageData Meta { get; set; }
    }

    public class PageData
    {
        public int Page { get; internal set; }
        public int Per_page { get; internal set; }
        public int Total { get; internal set; }
    }
}