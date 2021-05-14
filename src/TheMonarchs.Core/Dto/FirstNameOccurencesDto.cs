using System.Collections.Generic;
using System.Linq;

namespace TheMonarchs.Core.Dto
{
    public class FirstNameOccurencesDto
    {

        public string Name { get; set; }

        public IEnumerable<string> Occurences { get; set; }


        public int TotalFound => Occurences.Count();
    }
}
