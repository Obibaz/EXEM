using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class Title_Ques
    {
        public int Id { get; set; }

        public string Title { get; set; }
        [JsonIgnore]
        public virtual List<Quest> Quest1 { get; set; }
    }
}
