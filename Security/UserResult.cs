using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public class UserResult
    {
        public int user_ID { get; set; }
        public String name { get; set; }
        public String surname { get; set; }
        public String email { get; set; }
        public List<String> operations { get; set; }


        public UserResult()
        {

        }
    }
}
