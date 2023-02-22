using System.ComponentModel.DataAnnotations;

namespace IMAOCMS.Core.Entites
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
