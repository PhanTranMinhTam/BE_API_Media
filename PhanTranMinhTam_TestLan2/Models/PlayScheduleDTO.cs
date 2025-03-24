namespace PhanTranMinhTam_TestLan2.Models
{
    public class PlayScheduleDTO
    {
        public int MusicId { get; set; }
        public int RecurrenceRuleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TimeSlotId { get; set; }

    }
}
