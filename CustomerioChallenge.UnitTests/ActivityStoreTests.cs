using CustomerioChallenge.Eventing;
using CustomerioChallenge.Summarization;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CustomerioChallenge.UnitTests
{
    public class ActivityStoreTests
    {
        [Fact]
        public void GivenDatWithCornerCases_WhenActivityStoreLoads_DataIsSummerisedCorrectlyInDataStore()
        {
            //todo: divide this test in smaller tests per case

            //act
            ActivityStore.Load(@"C:\Ajinkya\POC\CustomerioChallenge\CustomerioChallenge.UnitTests\TestData\all_corner_cases.data");
            
            //assert
            var customers = DataReadStore.List();

            var customer = customers.Single(c => c.Id == 11);
            Assert.Equal(2, customer.OneTimeActivities.Count);
            Assert.Single(customer.OneTimeActivities.ElementAt(0).Value);
            Assert.Single(customer.OneTimeActivities.ElementAt(1).Value);
            Assert.True(customer.AttributesInfo.ContainsKey("city1"));
            Assert.False(customer.AttributesInfo.ContainsKey("city"));
            Assert.Equal("197.183.11.234", customer.AttributesInfo["ip"].Value);
        }

        [Fact]
        public void GivenNewAttributeChangeActivity_WhenAddedToActivityStore_DataIsSummerisedCorrectlyInDataStore()
        {
            //act
            ActivityStore.AddAttributeChangeActivity(99, new Dictionary<string, string> { { "name", "Ajinkya" }, { "mob", "9995551112" } });
            
            //assert
            var customers = DataReadStore.List();
            Assert.Single(customers);

            var customer = customers.Single(c => c.Id == 99);
            Assert.Equal(0, customer.OneTimeActivities.Count);
            Assert.True(customer.AttributesInfo.ContainsKey("name"));
            Assert.True(customer.AttributesInfo.ContainsKey("mob"));
            Assert.Equal("Ajinkya", customer.AttributesInfo["name"].Value);
            Assert.Equal("9995551112", customer.AttributesInfo["mob"].Value);
        }
    }
}
