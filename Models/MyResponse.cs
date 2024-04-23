using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class MyResponse

    {
        [Key]
        public int? Id { get; set; }

        public string? Massage {  get; set; }
        public List<Quest>? quests { get; set; }

        public List<Title_Ques>? titles { get; set; }
        public List <string>? str {  get; set; }
    }
}
