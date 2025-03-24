using FluentValidation;
using PhanTranMinhTam_TestLan2.Models;

namespace PhanTranMinhTam_TestLan2.Validations
{
    public class PlayScheduleValidation : AbstractValidator<PlayScheduleDTO>
    {
        //public PlayScheduleValidation()
        //{
        //    // Rule for MusicId - Must be a positive integer
        //    RuleFor(x => x.MusicId)
        //        .GreaterThan(0).WithMessage("MusicId must be a positive integer.");

        //    // Rule for RecurrenceRuleId - Must be a positive integer
        //    RuleFor(x => x.RecurrenceRuleId)
        //        .GreaterThan(0).WithMessage("RecurrenceRuleId must be a positive integer.");

        //    // Rule for DateRangeId - Must be a positive integer
        //    RuleFor(x => x.DateRangeId)
        //        .GreaterThan(0).WithMessage("DateRangeId must be a positive integer.");

        //    // Rule for TimeSlotId - Must be a positive integer
        //    RuleFor(x => x.TimeSlotId)
        //        .GreaterThan(0).WithMessage("TimeSlotId must be a positive integer.");
        //}
    }
}
