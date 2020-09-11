using System.Collections.Generic;

namespace CouchbaseNet3SixThings.Common
{
    public class Pizza
    {
        public int SizeInches { get; set; }
        public List<string> Toppings { get; set; }
        public bool ExtraCheese { get; set; }
        public string Type => "Pizza";
    }
}