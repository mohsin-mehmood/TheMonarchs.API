using System.Collections.Generic;
using System.Linq;

namespace TheMonarchs.Core.Dto
{
    public class HouseMonarchsDto
    {

        public string House { get; set; }
        public IEnumerable<MonarchDto> Monarchs { get; set; }

        public long YearsRuled => (long)Monarchs.Sum(m => m.YearsRuled);

    }
}
