using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreAPI.Domain.Entity
{
    [Table("BillingItem")]
    public class BillingItemEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Company")]
        public Guid Company { get; set; }

        [Column("ClientId")]
        public long ClientId { get; set; }

        [Column("ItemCode")]
        [MaxLength(20)]
        public string ItemCode { get; set; }

        [Column("ItemDescription")]
        [MaxLength(1000)]
        public string ItemDescri { get; set; }

        [Column("PackageId")]
        public Guid? PackageId { get; set; }

        [Column("Active")]
        [MaxLength(1)]
        public string Active { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Column("EndDate")]
        public DateTime? EndDate { get; set; }

    }
}
