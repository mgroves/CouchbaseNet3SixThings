using System;
using Couchbase;
using CouchbaseNet3SixThings.Common;
using NUnit.Framework;

namespace CouchbaseNet3SixThings_2X
{
    public class Exceptions : Base
    {
        [Test]
        public void ExceptionsAreDifferent()
        {
            // try to get a document that doesn't exist
            IOperationResult<Pizza> doc =
                Bucket.Get<Pizza>(Guid.NewGuid().ToString());

            // exceptions are returned, not thrown
            Assert.That(doc.Message, Is.Not.Null);
            Assert.That(doc.Exception, Is.Not.Null);
        }
    }
}