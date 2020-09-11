using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase.Query;
using CouchbaseNet3SixThings.Common;
using NUnit.Framework;

namespace CouchbaseNet3SixThings_3X
{
    public class N1QL : Base
    {
        [Test]
        public async Task N1QLIsOnlyForCluster()
        {
            // put some data in
            var myPizza = new Pizza { SizeInches = 14, Toppings = new List<string> { "Pepperoni", "Cheese" }, ExtraCheese = true };
            await Collection.InsertAsync(Guid.NewGuid().ToString(), myPizza);

            // run QueryAsync directly on the cluster
            //  QueryOptions as a parameter for:
            // 1 ScanConsistency
            // 2 Parameter
            // 3 ... etc ... 
            IQueryResult<Pizza> result =
                await Cluster.QueryAsync<Pizza>(@$"
                SELECT p.*
                FROM {Bucket.Name} p
                WHERE p.type = $type",
                new QueryOptions()
                    .ScanConsistency(QueryScanConsistency.RequestPlus)
                    .Parameter("$type", "Pizza"));

            // result.Rows is now IAsyncEnumerable<T>
            var list = await result.Rows.ToListAsync();

            Assert.That(list.Any(r => r.ExtraCheese), Is.True);
        }
    }
}