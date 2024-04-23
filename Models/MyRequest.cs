using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
 

namespace Models
{
    [Serializable]
    public class MyRequest
    {
        [Key]
        public int? Id {  get; set; }
        public string? Header { get; set; }

        public User? AuthUser { get; set; }

        public Title_Ques? quest { get; set; }

        public List <Quest>? chquest {  get; set; } 


    }
}
