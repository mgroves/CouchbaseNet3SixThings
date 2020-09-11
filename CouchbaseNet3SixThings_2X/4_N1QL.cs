using System;
using System.Collections.Generic;
using System.Linq;
using Couchbase.N1QL;
using CouchbaseNet3SixThings.Common;
using NUnit.Framework;

namespace CouchbaseNet3SixThings_2X
{
    public class N1QL : Base
    {
        [Test]
        public void N1QlOnCluster()
        {
            // put some data in
            var myPizza = new Pizza { SizeInches = 14, Toppings = new List<string> { "Pepperoni", "Cheese"}, ExtraCheese = true};
            Bucket.Insert(Guid.NewGuid().ToString(), myPizza);

            // create QueryResult object
            // 1) to specify N1QL
            // 2) to specify ScanConsistency
            // 3) to specify arguments
            // 4) ... etc ...
            QueryRequest qr = new QueryRequest(@$"
                SELECT p.*
                FROM {Bucket.Name} p
                WHERE p.type = $type");
            qr.ScanConsistency(ScanConsistency.RequestPlus);
            qr.AddNamedParameter("$type", "Pizza");

            // can be executed on cluster
            IQueryResult<Pizza> result = Cluster.Query<Pizza>(qr);

            Assert.That(result.Rows.Any(r => r.ExtraCheese),
                Is.True);
        }

        [Test]
        public void N1QlOnBucket()
        {
            // put some data in
            var myPizza = new Pizza { SizeInches = 14, Toppings = new List<string> { "Pepperoni", "Cheese" }, ExtraCheese = true };
            Bucket.Insert(Guid.NewGuid().ToString(), myPizza);

            // create QueryResult object
            QueryRequest qr = new QueryRequest(@$"
                SELECT p.*
                FROM {Bucket.Name} p
                WHERE p.type = $type");
            qr.ScanConsistency(ScanConsistency.RequestPlus);
            qr.AddNamedParameter("$type", "Pizza");

            // can be executed on bucket too!
            IQueryResult<Pizza> result = Bucket.Query<Pizza>(qr);

            Assert.That(result.Rows.Any(r => r.ExtraCheese),
                Is.True);
        }
    }
}