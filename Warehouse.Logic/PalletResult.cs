using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Logic
{
    class PalletResult
    {
        public int Id { get; set; }
        public String kod_miejsca_w_mag { get; set; }
        public String kod_palety { get; set; }
        public int id_zamowienia { get; set; }
    }
}
