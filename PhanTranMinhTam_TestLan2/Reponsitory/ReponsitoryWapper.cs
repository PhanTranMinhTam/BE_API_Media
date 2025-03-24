using Microsoft.EntityFrameworkCore.Storage;
using PhanTranMinhTam_TestLan2.Data;
using PhanTranMinhTam_TestLan2.Reponsitory;

namespace PhanTranMinhTam_TestLan2.Repository
{
    public interface IMusicRepository : IRepositoryBase<Music> { }
    public interface IMusicPlayScheduleRepository : IRepositoryBase<PlaySchedule> { }
    //public interface IDateRangeRepository : IRepositoryBase<DateRange> { }
    public interface IRecurrenceRuleRepository : IRepositoryBase<RecurrenceRule> { }
    public interface ITimeSlotRepository : IRepositoryBase<TimeSlot> { }

    public interface IRepositoryWrapper
    {
        IMusicRepository Music { get; }
        IMusicPlayScheduleRepository MusicPlaySchedule { get; }
        /// <summary>
        /// IDateRangeRepository DateRange { get; }
        /// </summary>
        IRecurrenceRuleRepository RecurrenceRule { get; }
        ITimeSlotRepository TimeSlot { get; }
        void Save();
        Task SaveAsync();
        IDbContextTransaction BeginTransaction();
    }

    public class MusicRepository : ReponsitoryBase<Music>, IMusicRepository
    {
        public MusicRepository(MyDbContext context) : base(context) { }
    }
    public class MusicPlaysacheduleRepository : ReponsitoryBase<PlaySchedule>, IMusicPlayScheduleRepository
    {
        public MusicPlaysacheduleRepository(MyDbContext context) : base(context) { }
    }
    //public class DateRangeRepository : ReponsitoryBase<DateRange>, IDateRangeRepository
    //{
    //    public DateRangeRepository(MyDbContext context) : base(context) { }
    //}
    public class RecurrenceRuleRepository : ReponsitoryBase<RecurrenceRule>, IRecurrenceRuleRepository
    {
        public RecurrenceRuleRepository(MyDbContext context) : base(context) { }
    }
    public class TimeSlotRepository : ReponsitoryBase<TimeSlot>, ITimeSlotRepository
    {
        public TimeSlotRepository(MyDbContext context) : base(context) { }
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IMusicRepository music;
        private IMusicPlayScheduleRepository playSchedule;
        //private IDateRangeRepository dateRange;
        private IRecurrenceRuleRepository recurrenceRule;
        private ITimeSlotRepository timeSlot;
        private readonly MyDbContext context;

        public RepositoryWrapper(MyDbContext context)
        {
            this.context = context;
        }

        public IMusicRepository Music => music ??= new MusicRepository(context);
        public IMusicPlayScheduleRepository MusicPlaySchedule => playSchedule ??= new MusicPlaysacheduleRepository(context);
        //public IDateRangeRepository DateRange => dateRange ??= new DateRangeRepository(context);
        public IRecurrenceRuleRepository RecurrenceRule => recurrenceRule ??= new RecurrenceRuleRepository(context);
        public ITimeSlotRepository TimeSlot => timeSlot ??= new TimeSlotRepository(context);

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }
    }
}
