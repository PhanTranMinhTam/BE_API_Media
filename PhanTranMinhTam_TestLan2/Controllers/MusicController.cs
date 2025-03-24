using Microsoft.AspNetCore.Mvc;
using PhanTranMinhTam_TestLan2.Models;
using PhanTranMinhTam_TestLan2.Services;

namespace PhanTranMinhTam_TestLan2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MusicController : ControllerBase
    {
        private readonly IMusicServices _musicServices;

        public MusicController(IMusicServices musicServices)
        {
            _musicServices = musicServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMusics()
        {
            IEnumerable<Data.Music> musics = await _musicServices.GetAllMusicsAsync();
            return Ok(musics);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMusicById(int id)
        {
            Data.Music gift = await _musicServices.GetMusicByIdAsync(id);
            if (gift == null)
            {
                return BadRequest();
            }

            return Ok(gift);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMussic([FromForm] MusicDTO musicDto)
        {

            if (ModelState.IsValid)
            {
                await _musicServices.AddMusic(musicDto);
                return Ok();
            }

            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMusicsAsync(int id, [FromForm] MusicDTO musicDto)
        {

            bool updateResult = await _musicServices.UpdateMusicAsync(id, musicDto);

            if (!updateResult)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"Music with ID {id} was not found or failed to update."
                });
            }

            return Ok(new
            {
                Status = "Success",
                Message = $"Music with ID {id} has been successfully updated."
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusicAsync(int id)
        {

            bool deleteResult = await _musicServices.DeleteMusicAsync(id);

            if (!deleteResult)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"Music with ID {id} was not found or could not be deleted."
                });
            }

            return Ok(new
            {
                Status = "Success",
                Message = $"Music with ID {id} has been successfully deleted."
            });
        }
    }
}
