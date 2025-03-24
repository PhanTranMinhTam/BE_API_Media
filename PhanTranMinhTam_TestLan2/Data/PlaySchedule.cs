using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanTranMinhTam_TestLan2.Data
{
    public class PlaySchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; }
        public int MusicId { get; set; }
        public int RecurrenceRuleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TimeSlotId { get; set; }
        public Music Musics { get; set; }
        public TimeSlot timeSlot { get; set; }
        public RecurrenceRule RecurrenceRule { get; set; }

    }
}

