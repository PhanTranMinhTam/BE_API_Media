using Microsoft.AspNetCore.Mvc;
using PhanTranMinhTam_TestLan2.Models;
using PhanTranMinhTam_TestLan2.Services;

namespace PhanTranMinhTam_TestLan2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayScheduleController : ControllerBase
    {
        private readonly IPlayScheduleServices _playScheduleServices;

        public PlayScheduleController(IPlayScheduleServices playScheduleServices)
        {
            _playScheduleServices = playScheduleServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMusics()
        {
            IEnumerable<Data.PlaySchedule> playSchedules = await _playScheduleServices.GetAllPlaySchedulesAsync();
            return Ok(playSchedules);
        }
        [HttpGet("{Schedule}")]
        public async Task<IActionResult> GetAllPlaySchedules([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? musicId = null)
        {
            IEnumerable<Data.PlaySchedule> result = await _playScheduleServices.GetAllPlaySchedulePTsAsync(pageNumber, pageSize, musicId);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayScheduleById(int id)
        {
            Data.PlaySchedule playSchedule = await _playScheduleServices.GetPlayScheduleByIdAsync(id);
            if (playSchedule == null)
            {
                return BadRequest();
            }

            return Ok(playSchedule);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlaySchedule([FromForm] PlayScheduleDTO playScheduleDto)
        {

            if (ModelState.IsValid)
            {
                await _playScheduleServices.AddPlaySchedule(playScheduleDto);
                return Ok();
            }

            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaySchedulesAsync(int id, [FromForm] PlayScheduleDTO playScheduleDto)
        {

            bool updateResult = await _playScheduleServices.UpdatePlayScheduleAsync(id, playScheduleDto);

            if (!updateResult)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"PlaySchedule with ID {id} was not found or failed to update."
                });
            }
            return Ok(new
            {
                Status = "Success",
                Message = $"PlaySchedule with ID {id} has been successfully updated."
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusicAsync(int id)
        {

            bool deleteResult = await _playScheduleServices.DeletePlayScheduleAsync(id);

            if (!deleteResult)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"PlaySchedule with ID {id} was not found or could not be deleted."
                });
            }
            return Ok(new
            {
                Status = "Success",
                Message = $"PlaySchedule with ID {id} has been successfully deleted."
            });
        }
    }
}
