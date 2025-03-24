namespace PhanTranMinhTam_TestLan2.Data
{
    public class TimeSlot
    {
        public int TimeSlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public ICollection<PlaySchedule> PlaySchedules { get; set; }
    }
}
