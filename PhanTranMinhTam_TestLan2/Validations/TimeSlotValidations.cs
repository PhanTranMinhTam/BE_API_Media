using FluentValidation;
using PhanTranMinhTam_TestLan2.Models;

namespace PhanTranMinhTam_TestLan2.Validations
{
    public class TimeSlotValidations : AbstractValidator<TimeSlotDTO>
    {
        //public TimeSlotValidations()
        //{
        //    // Rule for StartTime - Must be a valid time format (e.g., "14:30")
        //    RuleFor(x => x.StartTime)
        //        .NotEmpty().WithMessage("StartTime is required.")
        //        .Matches(@"^([01]\d|2[0-3]):([0-5]\d)$").WithMessage("StartTime must be in HH:mm format.");

        //    // Rule for EndTime - Must be a valid time format and after StartTime
        //    RuleFor(x => x.EndTime)
        //        .NotEmpty().WithMessage("EndTime is required.")
        //        .Matches(@"^([01]\d|2[0-3]):([0-5]\d)$").WithMessage("EndTime must be in HH:mm format.")
        //        .GreaterThan(x => x.StartTime).WithMessage("EndTime must be after StartTime.");

        //    // Rule for DayOfWeek - Must be a valid enum value
        //    RuleFor(x => x.DayOfWeek)
        //        .IsInEnum().WithMessage("DayOfWeek must be a valid day of the week.");
        //}

        //// Custom method to compare times as strings
        //private bool GreaterThan(string endTime, string startTime)
        //{
        //    if (DateTime.TryParseExact(startTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime start) &&
        //        DateTime.TryParseExact(endTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime end))
        //    {
        //        return end > start;
        //    }

        //    return false;
        //}
    }
}
