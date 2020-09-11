using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CouchbaseNet3SixThings_2X
{
    [TestFixture]
    public class EverythingAsync : Base
    {
        [Test]
        public async Task InsertDocument()
        {
            // synchronous
            string id1 = Guid.NewGuid().ToString();
            var doc1 = new {foo = "bar", v = "2.7.x" };
            Bucket.Insert(id1, doc1);

            // asynchronous
            string id2 = Guid.NewGuid().ToString();
            var doc2 = new { foo = "bar", v = "2.7.x" };
            await Bucket.InsertAsync(id2, doc2);

            Assert.That(Bucket.Exists(id1));
            Assert.That(Bucket.Exists(id2));
        }
    }
}