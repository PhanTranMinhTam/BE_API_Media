using PhanTranMinhTam_TestLan2.Data;
using PhanTranMinhTam_TestLan2.Models;

namespace PhanTranMinhTam_TestLan2.Mappings
{
    public class MappingMusic : AutoMapper.Profile
    {
        public MappingMusic()
        {
            CreateMap<MusicDTO, Music>()
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => TimeSpan.Parse(src.Duration)));
            CreateMap<RecurrenceRule, RecurrenceRuleDTO>().ReverseMap();
            //CreateMap<DateRange, DateRangeDTO>().ReverseMap();
            CreateMap<TimeSlot, TimeSlotDTO>().ReverseMap();
            CreateMap<PlaySchedule, PlayScheduleDTO>()
            .ForMember(dest => dest.MusicId, opt => opt.MapFrom(src => src.MusicId))
            .ForMember(dest => dest.RecurrenceRuleId, opt => opt.MapFrom(src => src.RecurrenceRuleId))
            .ForMember(dest => dest.TimeSlotId, opt => opt.MapFrom(src => src.TimeSlotId))
            .ReverseMap();
        }
    }
}
