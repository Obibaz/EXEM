using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable] ////ЩО ЦЕ?????????????????????????????
    public class User
    {
        public int? Id { get; set; } 
        public string Login { get; set; }
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public int rez {  get; set; }

  
        public virtual List<Rezult> rezults { get; set; }

    }
}
