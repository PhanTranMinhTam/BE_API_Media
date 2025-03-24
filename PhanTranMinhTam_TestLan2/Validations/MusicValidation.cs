using FluentValidation;
using PhanTranMinhTam_TestLan2.Models;

namespace PhanTranMinhTam_TestLan2.Validations
{
    public class MusicValidation : AbstractValidator<MusicDTO>
    {
        //private readonly IRepositoryWrapper _repositoryWrapper;

        //public MusicValidation(IRepositoryWrapper repositoryWrapper)
        //{
        //    _repositoryWrapper = repositoryWrapper;

        //    // Rule for Title
        //    RuleFor(x => x.Title)
        //        .NotEmpty().WithMessage("Title is required.");

        //    // Rule for MediaType - It must be either "audio" or "video"
        //    RuleFor(x => x.MediaType)
        //        .NotEmpty().WithMessage("MediaType is required.")
        //        .Must(BeAValidMediaType).WithMessage("MediaType must be 'audio' or 'video'.");

        //    // Rule for FilePath - Ensure file is not null and has a valid extension
        //    RuleFor(x => x.FilePath)
        //        .NotNull().WithMessage("File is required.")
        //        .Must(HaveValidExtension).WithMessage("Invalid file format. Only MP3, MP4, WAV, or AVI files are allowed.");

        //    // Rule for Duration - Optional but must be a valid time format (e.g., "00:03:45")
        //    RuleFor(x => x.Duration)
        //        .NotEmpty().WithMessage("Duration is required.")
        //        .Matches(@"^\d{2}:\d{2}:\d{2}$").WithMessage("Invalid duration format. Use 'HH:mm:ss'.");
        //}

        //// Custom method to validate media type
        //private bool BeAValidMediaType(string mediaType)
        //{
        //    string[] validTypes = new[] { "audio", "video" };
        //    return validTypes.Contains(mediaType.ToLower());
        //}

        //// Custom method to validate file extension
        //private bool HaveValidExtension(IFormFile file)
        //{
        //    if (file == null)
        //        return false;

        //    string[] allowedExtensions = new[] { ".mp3", ".mp4", ".wav", ".avi" };
        //    string extension = Path.GetExtension(file.FileName).ToLower();
        //    return allowedExtensions.Contains(extension);
        //}
    }
}
