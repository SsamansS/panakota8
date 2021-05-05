using System.Collections.Generic;

namespace panakota8
{
    public class Supplier : BaseEntity
    {
        public string SupplierName { get; set; }
        public List<Product> Products { get; set; }
    }
}
