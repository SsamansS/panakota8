using System.ComponentModel.DataAnnotations;

namespace panakota8
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
