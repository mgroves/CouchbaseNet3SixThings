using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Couchbase.KeyValue;
using CouchbaseNet3SixThings.Common;
using NUnit.Framework;

namespace CouchbaseNet3SixThings_3X
{
    [TestFixture]
    public class Get : Base
    {
        [Test]
        public async Task GetReturnIsDifferent()
        {
            // insert a document, no Document<T> necessary
            var id = Guid.NewGuid().ToString();
            var pizza = new Pizza
            {
                SizeInches = 14,
                Toppings = new List<string> { "Pepperoni", "Mushrooms" },
                ExtraCheese = true
            };
            await Collection.InsertAsync(id, pizza);

            // get a document, no type necessary
            IGetResult result = await Collection.GetAsync(id);
            // use ContentAs to serialize to a given type
            Pizza myPizza = result.ContentAs<Pizza>();

            Assert.That(myPizza.SizeInches, 
                Is.EqualTo(pizza.SizeInches));
        }
    }
}