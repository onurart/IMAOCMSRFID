using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMAOCMS.Core.Entites
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public int? CreatorId { get; set; }
        public DateTime CreatedDate { get; set; }


        [ForeignKey(nameof(UpdaterId))]
        public int? UpdaterId { get; set; }
        public DateTime? UpdatedDate { get; set; }


        [ForeignKey(nameof(DeleterId))]
        public int? DeleterId { get; set; }

        public DateTime? DeletedDate { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsActive { get; set; }
    }
}
