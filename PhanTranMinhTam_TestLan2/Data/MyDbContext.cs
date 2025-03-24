using Microsoft.EntityFrameworkCore;

namespace PhanTranMinhTam_TestLan2.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }
        //public DbSet<DateRange> DateRanges { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<PlaySchedule> PlaySchedule { get; set; }
        public DbSet<RecurrenceRule> RecurrenceRules { get; set; }
        public DbSet<TimeSlot> TimeSlot { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Music entity
            modelBuilder.Entity<Music>()
                .HasKey(m => m.MediaId);

            modelBuilder.Entity<Music>()
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Music>()
                .Property(m => m.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<Music>()
                .Property(m => m.MediaType)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Music>()
                .Property(m => m.Duration)
                .IsRequired();

            // Configure PlaySchedule entity
            modelBuilder.Entity<PlaySchedule>()
                .HasKey(ps => ps.ScheduleId);

            modelBuilder.Entity<PlaySchedule>()
                .HasOne(ps => ps.Musics)
                .WithMany(m => m.Schedules)
                .HasForeignKey(ps => ps.MusicId);

            modelBuilder.Entity<PlaySchedule>()
                .HasOne(ps => ps.RecurrenceRule)
                .WithMany(rr => rr.Schedules)
                .HasForeignKey(ps => ps.RecurrenceRuleId);


            modelBuilder.Entity<PlaySchedule>()
                .HasIndex(s => new { s.RecurrenceRuleId, s.MusicId, s.ScheduleId })
                .IsUnique();
            // Configure RecurrenceRule entity
            modelBuilder.Entity<RecurrenceRule>()
                .HasKey(rr => rr.RecurrenceRuleId);

            modelBuilder.Entity<RecurrenceRule>()
                .Property(rr => rr.Pattern)
                .IsRequired();

            modelBuilder.Entity<RecurrenceRule>()
                .Property(rr => rr.Interval)
                .IsRequired();



            // Configure TimeSlot entity
            modelBuilder.Entity<TimeSlot>()
                .HasKey(ts => ts.TimeSlotId);

            modelBuilder.Entity<TimeSlot>()
                .Property(ts => ts.StartTime)
                .IsRequired();

            modelBuilder.Entity<TimeSlot>()
                .Property(ts => ts.EndTime)
                .IsRequired();

            modelBuilder.Entity<TimeSlot>()
                .Property(ts => ts.DayOfWeek)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
