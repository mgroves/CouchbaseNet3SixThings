using System;
using System.Threading.Tasks;
using Couchbase.Core.Exceptions.KeyValue;
using NUnit.Framework;

namespace CouchbaseNet3SixThings_3X
{
    public class Exceptions : Base
    {
        [Test]
        public void ExceptionsAreDifferent()
        {
            Assert.ThrowsAsync<DocumentNotFoundException>(async () =>
            {
                // try to get a document that doesn't exist
                await Collection.GetAsync(Guid.NewGuid().ToString());
            });
        }

        [Test]
        public async Task UseTryCatchInstead()
        {
            try
            {
                // try to get a document that doesn't exist
                await Collection.GetAsync(Guid.NewGuid().ToString());
            }
            catch (DocumentNotFoundException ex)
            {
                Assert.Pass();
            }

            Assert.Fail();  // this should never be reached
        }
    }
}