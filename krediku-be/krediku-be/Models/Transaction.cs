using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace krediku_be.Models
{
    [Table("tr_bpkb")]
    public class Transaction
    {
        [Key]
        [Column("agreement_number")]
        public string AgreementNumber { get; set; }
        [Column("bpkb_no")]
        public string BpkbNumber { get; set; }
        [Column("branch_id")]
        public string BranchId { get; set; }
        [Column("bpkb_date")]
        public DateTime BpkbDate { get; set; }
        [Column("faktur_no")]
        public string FakturNumber { get; set; }
        [Column("faktur_date")]
        public DateTime FakturDate { get; set; }
        [Column("location_id")]
        public string LocationId { get; set; }
        [Column("police_no")]
        public string PoliceNumber { get; set; }
        [Column("bpkb_date_in")]
        public DateTime BpkbDateInput { get; set; }
        
    }
}
