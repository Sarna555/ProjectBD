using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    class ProductResult
    {

        public string name { get; set; }
        public List<string> categories { get; set; }
        public System.DateTime date { get; set; }
        public string pallet { get; set; }

    }
}
