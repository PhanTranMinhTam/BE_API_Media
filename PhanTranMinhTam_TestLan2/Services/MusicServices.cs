using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhanTranMinhTam_TestLan2.Data;
using PhanTranMinhTam_TestLan2.Models;
using PhanTranMinhTam_TestLan2.Repository;

namespace PhanTranMinhTam_TestLan2.Services
{
    public interface IMusicServices
    {
        Task<IEnumerable<Music>> GetAllMusicsAsync();
        Task<Music> GetMusicByIdAsync(int id);
        Task AddMusic(MusicDTO musicDto);
        Task<bool> UpdateMusicAsync(int id, MusicDTO musicDto);
        Task<bool> DeleteMusicAsync(int id);

    }
    public class MusicServices : IMusicServices
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public MusicServices(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<IEnumerable<Music>> GetAllMusicsAsync()
        {
            return await _repositoryWrapper.Music.FindAll().ToListAsync();
        }
        public async Task<Music> GetMusicByIdAsync(int id)
        {
            return await _repositoryWrapper.Music.FindByCondition(u => u.MediaId == id).FirstOrDefaultAsync();
        }
        public async Task AddMusic(MusicDTO musicDto)
        {
            TimeSpan duration;
            if (TimeSpan.TryParse(musicDto.Duration, out duration))
            {
            }
            else
            {
                throw new ArgumentException("Invalid duration format.");
            }
            Music music = _mapper.Map<Music>(musicDto);
            music.Duration = duration;

            if (musicDto.FilePath != null && musicDto.FilePath.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + musicDto.FilePath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                using (FileStream fileStream = new(filePath, FileMode.Create))
                {
                    await musicDto.FilePath.CopyToAsync(fileStream);
                }

                music.FilePath = uniqueFileName;
            }
            _repositoryWrapper.Music.Create(music);
            await _repositoryWrapper.SaveAsync();
        }
        public async Task<bool> UpdateMusicAsync(int id, MusicDTO musicDto)
        {
            Music? existingMusic = await _repositoryWrapper.Music.FindByCondition(u => u.MediaId == id).FirstOrDefaultAsync();
            if (existingMusic == null)
            {
                return false;
            }
            _mapper.Map(musicDto, existingMusic);

            if (musicDto.FilePath != null && musicDto.FilePath.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + musicDto.FilePath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (!string.IsNullOrEmpty(existingMusic.FilePath))
                {
                    string oldFilePath = Path.Combine(uploadsFolder, existingMusic.FilePath);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                using (FileStream fileStream = new(filePath, FileMode.Create))
                {
                    await musicDto.FilePath.CopyToAsync(fileStream);
                }

                existingMusic.FilePath = uniqueFileName;
            }

            _repositoryWrapper.Music.Update(existingMusic);
            await _repositoryWrapper.SaveAsync();

            return true;
        }
        public async Task<bool> DeleteMusicAsync(int id)
        {

            Music? existingMusic = await _repositoryWrapper.Music.FindByCondition(u => u.MediaId == id).FirstOrDefaultAsync();

            if (existingMusic == null)
            {
                return false;
            }

            _repositoryWrapper.Music.Delete(existingMusic);
            await _repositoryWrapper.SaveAsync();

            return true;
        }
    }
}
