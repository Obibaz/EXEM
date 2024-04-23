using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class Rezult
    {
        [Key]
        public int Id { get; set; }
        public string? TitleRez { get; set; }

        public int rez {  get; set; }

        public DateTime? DateRez { get; set; }


        public virtual User UserRez { get; set; }



        
       
    }
}
