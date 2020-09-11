using System;
using System.Collections.Generic;
using Couchbase;
using CouchbaseNet3SixThings.Common;
using NUnit.Framework;

namespace CouchbaseNet3SixThings_2X
{
    public class Cas : Base
    {
        [Test]
        public void WhereCanIUseCas()
        {
            // insert a new document
            var id = Guid.NewGuid().ToString();
            var pizza = new Pizza { SizeInches = 14, Toppings = new List<string> { "Pepperoni", "Mushroom" }, ExtraCheese = true };
            Bucket.Insert(id, pizza);

            // get document with CAS
            IOperationResult<Pizza> getResult = Bucket.Get<Pizza>(id);
            Assert.That(getResult.Cas, Is.GreaterThan(0));

            // replace with CAS
            pizza.ExtraCheese = false;
            var replaceDoc = new Document<Pizza>
            {
                Id = id,
                Content = pizza,
                Cas = getResult.Cas
            };
            IDocumentResult<Pizza> replaceResult = Bucket.Replace(replaceDoc);
            Assert.That(replaceResult.Document.Cas,
                Is.GreaterThan(0));
            Assert.That(replaceResult.Document.Cas,
                Is.Not.EqualTo(getResult.Cas));

            // upsert with CAS
            pizza.ExtraCheese = true;
            var upsertDoc = new Document<Pizza>
            {
                Id = id,
                Content = pizza,
                Cas = replaceDoc.Cas
            };
            IDocumentResult<Pizza> upsertResult = Bucket.Upsert(upsertDoc);
            Assert.That(upsertResult.Success, Is.True);
        }

        [Test]
        public void UpsertWeirdness()
        {
            var id = Guid.NewGuid().ToString();
            var pizza = new Pizza { SizeInches = 14, Toppings = new List<string> { "Pepperoni", "Mushroom" }, ExtraCheese = true };

            // upsert with CAS - completely new doc
            var upsertNewDoc = new Document<Pizza>
            {
                Id = id,
                Content = pizza,
                Cas = 12345             // this is a completely new document, so what does CAS mean?
            };
            IDocumentResult<Pizza> upsertNewResult =
                Bucket.Upsert(upsertNewDoc);

            // if a CAS value is present, upsert == replace
            // since this is a brand new document, it will fail
            // because document can't be found
            Assert.That(upsertNewResult.Exception, Is.Not.Null);
            Assert.That(upsertNewResult.Success, Is.False);
        }
    }
}