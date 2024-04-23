using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class Quest
    {
        [Key]
        public int? Id { get; set; } // Зробити ключ необов'язковим

        public string Vopros { get; set; }
        public string Quests1 { get; set; }
        public string Quests2 { get; set; }
        public string Quests3 { get; set; }
        public string Quests4 { get; set; }

        public string right {  get; set; }

        public int Title_QuesId { get; set; }

        public virtual Title_Ques Title_Ques { get; set; }
        //public virtual User User { get; set; }
    }
}
