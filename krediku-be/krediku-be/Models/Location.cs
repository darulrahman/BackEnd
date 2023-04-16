using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace krediku_be.Models
{
    [Table("ms_storage_location")]
    public class Location
    {
        [Key]
        [Column("location_id")]
        public string Id { get; set; } = string.Empty;
        [Column("location_name")]
        public string Name { get; set; } = string.Empty;
    }
}
