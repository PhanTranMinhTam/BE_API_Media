using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanTranMinhTam_TestLan2.Data
{
    public enum RecurrencePatternType
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
    public class RecurrenceRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecurrenceRuleId { get; set; }
        public RecurrencePatternType Pattern { get; set; }
        public int Interval { get; set; }
        public ICollection<PlaySchedule> Schedules { get; set; }
    }
}
