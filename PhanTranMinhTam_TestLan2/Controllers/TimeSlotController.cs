using Microsoft.AspNetCore.Mvc;
using PhanTranMinhTam_TestLan2.Models;
using PhanTranMinhTam_TestLan2.Services;

namespace PhanTranMinhTam_TestLan2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotServices _timeslotServices;

        public TimeSlotController(ITimeSlotServices timeslotServices)
        {
            _timeslotServices = timeslotServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTimeSlots()
        {
            IEnumerable<Data.TimeSlot> timeSlots = await _timeslotServices.GetAllTimeSlotsAsync();
            return Ok(timeSlots);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeSlotById(int id)
        {
            Data.TimeSlot timeSlot = await _timeslotServices.GetTimeSlotByIdAsync(id);
            if (timeSlot == null)
            {
                return BadRequest();
            }

            return Ok(timeSlot);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTimeSlot([FromForm] TimeSlotDTO timeSlotDto)
        {

            if (ModelState.IsValid)
            {
                await _timeslotServices.AddTimeSlot(timeSlotDto);
                return Ok();
            }

            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeSlotsAsync(int id, [FromForm] TimeSlotDTO timeSlotDto)
        {

            bool updateResult = await _timeslotServices.UpdateTimeSlotAsync(id, timeSlotDto);

            if (!updateResult)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"TimeSlot with ID {id} was not found or failed to update."
                });
            }

            return Ok(new
            {
                Status = "Success",
                Message = $"TimeSlot with ID {id} has been successfully updated."
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeSlotAsync(int id)
        {

            bool deleteResult = await _timeslotServices.DeleteTimeSlotAsync(id);

            if (!deleteResult)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"TimeSlot with ID {id} was not found or could not be deleted."
                });
            }

            return Ok(new
            {
                Status = "Success",
                Message = $"TimeSlot with ID {id} has been successfully deleted."
            });
        }
    }
}
