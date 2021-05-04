using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace panakota8
{
    public class DeputyActivity
    {
        public string Name { get; set; }
        public List<Vote> Votes { get; set; }
    }
}
