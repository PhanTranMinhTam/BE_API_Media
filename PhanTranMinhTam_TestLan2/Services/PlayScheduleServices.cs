using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhanTranMinhTam_TestLan2.Data;
using PhanTranMinhTam_TestLan2.Models;
using PhanTranMinhTam_TestLan2.Repository;

namespace PhanTranMinhTam_TestLan2.Services
{
    public interface IPlayScheduleServices
    {
        Task<IEnumerable<PlaySchedule>> GetAllPlaySchedulesAsync();
        Task<PlaySchedule> GetPlayScheduleByIdAsync(int id);
        Task AddPlaySchedule(PlayScheduleDTO playScheduleDto);
        Task<bool> UpdatePlayScheduleAsync(int id, PlayScheduleDTO playScheduleDto);
        Task<bool> DeletePlayScheduleAsync(int id);
        Task<IEnumerable<PlaySchedule>> GetAllPlaySchedulePTsAsync(int pageNumber, int pageSize, int? musicId);
    }
    public class PlayScheduleServices : IPlayScheduleServices
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public PlayScheduleServices(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<IEnumerable<PlaySchedule>> GetAllPlaySchedulesAsync()
        {
            return await _repositoryWrapper.MusicPlaySchedule.FindAll().ToListAsync();
        }
        public async Task<PlaySchedule> GetPlayScheduleByIdAsync(int id)
        {
            return await _repositoryWrapper.MusicPlaySchedule.FindByCondition(u => u.ScheduleId == id).FirstOrDefaultAsync();
        }
        public async Task AddPlaySchedule(PlayScheduleDTO playScheduleDto)
        {
            PlaySchedule playSchedule = _mapper.Map<PlaySchedule>(playScheduleDto);

            // Lấy thông tin TimeSlot từ cơ sở dữ liệu
            TimeSlot? timeSlot = await _repositoryWrapper.TimeSlot
                .FindByCondition(ts => ts.TimeSlotId == playSchedule.TimeSlotId)
                .FirstOrDefaultAsync();

            if (timeSlot == null)
            {
                throw new ArgumentException("Thời gian không hợp lệ.");
            }

            // Kiểm tra trùng lặp lịch phát cho tuần hiện tại
            bool isDuplicate = await _repositoryWrapper.MusicPlaySchedule
                .FindByCondition(ps =>
                    ps.MusicId == playSchedule.MusicId &&
                    ps.TimeSlotId == playSchedule.TimeSlotId &&
                    ((ps.StartDate <= playSchedule.StartDate && playSchedule.StartDate <= ps.EndDate) ||
                     (ps.StartDate <= playSchedule.EndDate && playSchedule.EndDate <= ps.EndDate)))
                .AnyAsync();

            if (isDuplicate)
            {
                throw new InvalidOperationException("Lịch phát nhạc đã tồn tại trong cùng thời gian và ngày.");
            }

            // Tạo lịch phát mới cho tuần tiếp theo nếu lịch hiện tại nằm trong khoảng thời gian quy định
            if (playSchedule.StartDate <= DateTime.Now &&
                (!playSchedule.EndDate.HasValue || DateTime.Now <= playSchedule.EndDate.Value))
            {
                // Kiểm tra trùng lặp lịch phát cho tuần tiếp theo
                DateTime nextWeekStartDate = playSchedule.StartDate.AddDays(7);
                DateTime? nextWeekEndDate = playSchedule.EndDate.HasValue
                    ? playSchedule.EndDate.Value.AddDays(7)
                    : (DateTime?)null;

                bool isNextWeekDuplicate = await _repositoryWrapper.MusicPlaySchedule
                    .FindByCondition(ps =>
                        ps.MusicId == playSchedule.MusicId &&
                        ps.TimeSlotId == playSchedule.TimeSlotId &&
                        ((ps.StartDate <= nextWeekStartDate && nextWeekStartDate <= ps.EndDate) ||
                         (ps.StartDate <= nextWeekEndDate && nextWeekEndDate <= ps.EndDate)))
                    .AnyAsync();

                if (isNextWeekDuplicate)
                {
                    throw new InvalidOperationException("Lịch phát nhạc cho tuần tiếp theo đã tồn tại trong cùng thời gian và ngày.");
                }

                // Tạo lịch phát cho tuần tiếp theo
                PlaySchedule nextWeekPlaySchedule = new()
                {
                    MusicId = playSchedule.MusicId,
                    RecurrenceRuleId = playSchedule.RecurrenceRuleId,
                    StartDate = nextWeekStartDate,
                    EndDate = nextWeekEndDate,
                    TimeSlotId = playSchedule.TimeSlotId
                };

                // Lưu lịch phát cho tuần tiếp theo
                _repositoryWrapper.MusicPlaySchedule.Create(nextWeekPlaySchedule);
                await _repositoryWrapper.SaveAsync();
            }

            // Tạo lịch phát mới hiện tại
            _repositoryWrapper.MusicPlaySchedule.Create(playSchedule);
            await _repositoryWrapper.SaveAsync();
        }


        public async Task<bool> UpdatePlayScheduleAsync(int id, PlayScheduleDTO playScheduleDto)
        {
            // Lấy thông tin lịch phát hiện tại từ cơ sở dữ liệu
            PlaySchedule? existingPlaySchedule = await _repositoryWrapper.MusicPlaySchedule
                .FindByCondition(ps => ps.ScheduleId == id)
                .FirstOrDefaultAsync();

            if (existingPlaySchedule == null)
            {
                // Nếu lịch phát không tồn tại, trả về false
                return false;
            }


            _mapper.Map(playScheduleDto, existingPlaySchedule);

            // Kiểm tra tính hợp lệ của TimeSlot
            TimeSlot? timeSlot = await _repositoryWrapper.TimeSlot
                .FindByCondition(ts => ts.TimeSlotId == existingPlaySchedule.TimeSlotId)
                .FirstOrDefaultAsync();

            if (timeSlot == null)
            {
                throw new ArgumentException("Thời gian không hợp lệ.");
            }

            // Kiểm tra tính hợp lệ của Music
            Music? music = await _repositoryWrapper.Music
                .FindByCondition(m => m.MediaId == existingPlaySchedule.MusicId)
                .FirstOrDefaultAsync();

            if (music == null)
            {
                throw new ArgumentException("Nhạc không hợp lệ.");
            }

            // Kiểm tra trùng lặp lịch phát cho tuần hiện tại
            bool isDuplicate = await _repositoryWrapper.MusicPlaySchedule
                .FindByCondition(ps =>
                    ps.MusicId == existingPlaySchedule.MusicId &&
                    ps.TimeSlotId == existingPlaySchedule.TimeSlotId &&
                    ((ps.StartDate <= existingPlaySchedule.StartDate && existingPlaySchedule.StartDate <= ps.EndDate) ||
                     (ps.StartDate <= existingPlaySchedule.EndDate && existingPlaySchedule.EndDate <= ps.EndDate)) &&
                    ps.ScheduleId != id) // Exclude current record
                .AnyAsync();

            if (isDuplicate)
            {
                throw new InvalidOperationException("Lịch phát nhạc đã tồn tại trong cùng thời gian và ngày.");
            }

            // Kiểm tra trùng lặp lịch phát cho tuần tiếp theo
            if (existingPlaySchedule.StartDate <= DateTime.Now &&
                (!existingPlaySchedule.EndDate.HasValue || DateTime.Now <= existingPlaySchedule.EndDate.Value))
            {
                DateTime nextWeekStartDate = existingPlaySchedule.StartDate.AddDays(7);
                DateTime? nextWeekEndDate = existingPlaySchedule.EndDate.HasValue
                    ? existingPlaySchedule.EndDate.Value.AddDays(7)
                    : (DateTime?)null;

                bool isNextWeekDuplicate = await _repositoryWrapper.MusicPlaySchedule
                    .FindByCondition(ps =>
                        ps.MusicId == existingPlaySchedule.MusicId &&
                        ps.TimeSlotId == existingPlaySchedule.TimeSlotId &&
                        ((ps.StartDate <= nextWeekStartDate && nextWeekStartDate <= ps.EndDate) ||
                         (ps.StartDate <= nextWeekEndDate && nextWeekEndDate <= ps.EndDate)) &&
                        ps.ScheduleId != id)
                    .AnyAsync();

                if (isNextWeekDuplicate)
                {
                    throw new InvalidOperationException("Lịch phát nhạc cho tuần tiếp theo đã tồn tại trong cùng thời gian và ngày.");
                }
            }

            // Cập nhật thông tin lịch phát trong cơ sở dữ liệu
            _repositoryWrapper.MusicPlaySchedule.Update(existingPlaySchedule);
            await _repositoryWrapper.SaveAsync();

            return true;
        }


        public async Task<bool> DeletePlayScheduleAsync(int id)
        {

            PlaySchedule? existingMusicPlay = await _repositoryWrapper.MusicPlaySchedule.FindByCondition(u => u.ScheduleId == id).FirstOrDefaultAsync();

            if (existingMusicPlay == null)
            {
                return false;
            }

            _repositoryWrapper.MusicPlaySchedule.Delete(existingMusicPlay);
            await _repositoryWrapper.SaveAsync();

            return true;
        }
        public async Task<IEnumerable<PlaySchedule>> GetAllPlaySchedulePTsAsync(int pageNumber, int pageSize, int? musicId)
        {
            IQueryable<PlaySchedule> query = _repositoryWrapper.MusicPlaySchedule.FindAll();

            // Lọc theo MusicId nếu có
            if (musicId.HasValue)
            {
                query = query.Where(u => u.MusicId == musicId.Value);
            }

            // Thực hiện phân trang
            List<PlaySchedule> pagedResult = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return pagedResult;
        }
    }
}
