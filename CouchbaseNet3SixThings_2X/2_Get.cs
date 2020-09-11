using System;
using System.Collections.Generic;
using Couchbase;
using CouchbaseNet3SixThings.Common;
using NUnit.Framework;

namespace CouchbaseNet3SixThings_2X
{
    [TestFixture]
    public class Get : Base
    {
        [Test]
        public void GetReturnIsDifferent()
        {
            // insert a document, uses Document<T>
            var doc = new Document<Pizza>();
            doc.Id = Guid.NewGuid().ToString();
            doc.Content = new Pizza
            {
                SizeInches = 14,
                Toppings = new List<string> {"Pepperoni", "Mushrooms"},
                ExtraCheese = true
            };
            Bucket.Insert(doc);

            // get a document, uses Get<T>
            IOperationResult<Pizza> result = Bucket.Get<Pizza>(doc.Id);
            Pizza myPizza = result.Value;

            Assert.That(doc.Content.SizeInches, 
                Is.EqualTo(myPizza.SizeInches));
        }
    }
}