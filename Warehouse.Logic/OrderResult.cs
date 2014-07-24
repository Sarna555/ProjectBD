using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Logic
{
    class OrderResult
    {
        public int Id { get; set; }
        public String nazwa_stanu { get; set; }
        public String nadawca { get; set; }
        public String odbiorca { get; set; }
        public DateTime data_nadania { get; set; }
        public DateTime data_odbioru { get; set; }
    }
}