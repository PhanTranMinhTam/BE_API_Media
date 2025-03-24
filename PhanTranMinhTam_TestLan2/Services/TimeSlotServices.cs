using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhanTranMinhTam_TestLan2.Data;
using PhanTranMinhTam_TestLan2.Models;
using PhanTranMinhTam_TestLan2.Repository;

namespace PhanTranMinhTam_TestLan2.Services
{
    public interface ITimeSlotServices
    {
        Task<IEnumerable<TimeSlot>> GetAllTimeSlotsAsync();
        Task<TimeSlot> GetTimeSlotByIdAsync(int id);
        Task AddTimeSlot(TimeSlotDTO timeslotDto);
        Task<bool> UpdateTimeSlotAsync(int id, TimeSlotDTO timeSlotDto);
        Task<bool> DeleteTimeSlotAsync(int id);
    }
    public class TimeSlotServices : ITimeSlotServices
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public TimeSlotServices(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<IEnumerable<TimeSlot>> GetAllTimeSlotsAsync()
        {
            return await _repositoryWrapper.TimeSlot.FindAll().ToListAsync();
        }
        public async Task<TimeSlot> GetTimeSlotByIdAsync(int id)
        {
            return await _repositoryWrapper.TimeSlot.FindByCondition(u => u.TimeSlotId == id).FirstOrDefaultAsync();
        }
        public async Task AddTimeSlot(TimeSlotDTO timeslotDto)
        {
            TimeSpan StartTime;
            TimeSpan EndTime;
            if (TimeSpan.TryParse(timeslotDto.StartTime, out StartTime) && TimeSpan.TryParse(timeslotDto.EndTime, out EndTime))
            {
            }
            else
            {
                throw new ArgumentException("Invalid duration format.");
            }
            TimeSlot timeSlot = _mapper.Map<TimeSlot>(timeslotDto);
            timeSlot.StartTime = StartTime;
            timeSlot.EndTime = EndTime;

            _repositoryWrapper.TimeSlot.Create(timeSlot);
            await _repositoryWrapper.SaveAsync();
        }
        public async Task<bool> UpdateTimeSlotAsync(int id, TimeSlotDTO timeSlotDto)
        {
            TimeSlot? existingTimeSlot = await _repositoryWrapper.TimeSlot.FindByCondition(u => u.TimeSlotId == id).FirstOrDefaultAsync();
            if (existingTimeSlot == null)
            {
                return false;
            }
            _mapper.Map(timeSlotDto, existingTimeSlot);

            _repositoryWrapper.TimeSlot.Update(existingTimeSlot);
            await _repositoryWrapper.SaveAsync();

            return true;
        }
        public async Task<bool> DeleteTimeSlotAsync(int id)
        {

            TimeSlot? existingTimeSlot = await _repositoryWrapper.TimeSlot.FindByCondition(u => u.TimeSlotId == id).FirstOrDefaultAsync();

            if (existingTimeSlot == null)
            {
                return false;
            }

            _repositoryWrapper.TimeSlot.Delete(existingTimeSlot);
            await _repositoryWrapper.SaveAsync();

            return true;
        }
    }
}
