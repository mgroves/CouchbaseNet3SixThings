using System;
using System.Threading.Tasks;
using Couchbase.KeyValue;
using NUnit.Framework;

namespace CouchbaseNet3SixThings_3X
{
    [TestFixture]
    public class EverythingAsync : Base
    {
        [Test]
        public async Task InsertDocument()
        {
            // no synchronous option!
            // _collection.Insert( . . . ) does not exist

            // asynchronous
            string id2 = Guid.NewGuid().ToString();
            var doc2 = new {foo = "bar", v = "3.x"};
            await Collection.InsertAsync(id2, doc2);

            // key/value no long available at Bucket level
            // _bucket.Insert( . . . ) does not exist

            IExistsResult existsResult = await Collection.ExistsAsync(id2);
            Assert.That(existsResult.Exists, Is.True);
        }
    }
}