using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace panakota8
{
    public class Law : BaseEntity
    {
        public string LawName { get; set; }
        public DateTime OfferDate { get; set; }
    }
}
