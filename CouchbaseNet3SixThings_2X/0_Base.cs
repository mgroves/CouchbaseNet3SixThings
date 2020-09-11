using System;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using NUnit.Framework;

namespace CouchbaseNet3SixThings_2X
{
    public abstract class Base
    {
        protected Cluster Cluster;
        protected IBucket Bucket;

        [SetUp]
        public void Setup()
        {
            Cluster = new Cluster(new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri("http://localhost:8091")
                },
                UseSsl = false
            });
            Cluster.Authenticate(
                "Administrator",
                "password");

            Bucket = Cluster.OpenBucket("matt");

            SetupIndex();
        }

        private void SetupIndex()
        {
            var manager = Bucket.CreateManager();
            manager.CreateN1qlPrimaryIndex(false);
        }

        [TearDown]
        public void TearDown()
        {
            Cluster.Dispose();
        }
    }
}