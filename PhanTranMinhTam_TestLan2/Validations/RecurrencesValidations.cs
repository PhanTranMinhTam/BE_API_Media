using FluentValidation;
using PhanTranMinhTam_TestLan2.Models;

namespace PhanTranMinhTam_TestLan2.Validations
{
    public class RecurrencesValidations : AbstractValidator<RecurrenceRuleDTO>
    {
        //public RecurrencesValidations()
        //{
        //    // Rule for Pattern - Must be a valid enum value
        //    RuleFor(x => x.Pattern)
        //        .IsInEnum().WithMessage("Pattern must be a valid recurrence pattern type.");

        //    // Rule for Interval - Must be a positive integer
        //    RuleFor(x => x.Interval)
        //        .GreaterThan(0).WithMessage("Interval must be a positive integer.");
        //}
    }
}
