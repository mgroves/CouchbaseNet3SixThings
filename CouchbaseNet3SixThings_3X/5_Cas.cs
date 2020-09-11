using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Couchbase.KeyValue;
using CouchbaseNet3SixThings.Common;
using NUnit.Framework;

namespace CouchbaseNet3SixThings_3X
{
    public class Cas : Base
    {
        [Test]
        public async Task WhereCasWorks()
        {
            // insert a new document
            var id = Guid.NewGuid().ToString();
            var pizza = new Pizza { SizeInches = 14, Toppings = new List<string> { "Pepperoni", "Mushroom" }, ExtraCheese = true};
            await Collection.InsertAsync(id, pizza);

            // get document with CAS
            IGetResult getResult = await Collection.GetAsync(id);
            Assert.That(getResult.Cas, Is.GreaterThan(0));

            // replace with CAS
            pizza.ExtraCheese = false;
            var replaceOptions = new ReplaceOptions();
            replaceOptions.Cas(getResult.Cas);    // there IS a CAS option for replace
            IMutationResult replaceResult =
                await Collection.ReplaceAsync(id, pizza, replaceOptions);
            Assert.That(replaceResult.Cas,
                Is.Not.EqualTo(getResult.Cas));
            Assert.That(replaceResult.Cas,
                Is.GreaterThan(0));

            // upsert with CAS?
            pizza.ExtraCheese = true;
            var upsertOptions = new UpsertOptions();
            // upsertOptions.Cas(. . . )   there is no Cas option here!
            IMutationResult upsertResult = 
                await Collection.UpsertAsync(id, pizza, upsertOptions);
            Assert.That(upsertResult.Cas, 
                Is.GreaterThan(0));
            Assert.That(upsertResult.Cas,
                Is.Not.EqualTo(getResult.Cas));
            Assert.That(upsertResult.Cas,
                Is.Not.EqualTo(replaceResult.Cas));

            // if you have a CAS value, use replace instead
        }
    }
}