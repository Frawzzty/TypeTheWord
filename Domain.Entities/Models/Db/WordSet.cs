using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Db
{
    public class WordSet
    {
        
        public WordSet()
        {
            
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public List<string> WordList { get; set; } = new List<string>();
    }
}
