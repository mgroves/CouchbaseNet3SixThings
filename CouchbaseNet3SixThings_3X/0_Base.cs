using System.Threading.Tasks;
using Couchbase;
using Couchbase.KeyValue;
using NUnit.Framework;

namespace CouchbaseNet3SixThings_3X
{
    public abstract class Base
    {
        protected ICluster Cluster;
        protected IBucket Bucket;
        protected ICouchbaseCollection Collection;

        [SetUp]
        public async Task Setup()
        {
            Cluster = await Couchbase.Cluster.ConnectAsync(
                "couchbase://localhost",
                "Administrator",
                "password");

            Bucket = await Cluster.BucketAsync("matt");
            Collection = Bucket.DefaultCollection();
        }

        [TearDown]
        public async Task Teardown()
        {
            await Cluster.DisposeAsync();
        }
    }
}