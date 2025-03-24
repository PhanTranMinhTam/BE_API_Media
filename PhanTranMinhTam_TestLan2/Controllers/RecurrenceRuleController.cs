using Microsoft.AspNetCore.Mvc;
using PhanTranMinhTam_TestLan2.Models;
using PhanTranMinhTam_TestLan2.Services;

namespace PhanTranMinhTam_TestLan2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecurrenceRuleController : ControllerBase
    {
        private readonly IRecurrenceRuleServices _recurrenRuleServices;

        public RecurrenceRuleController(IRecurrenceRuleServices recurrenRuleServices)
        {
            _recurrenRuleServices = recurrenRuleServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRecurrencesRules()
        {
            IEnumerable<Data.RecurrenceRule> recurrences = await _recurrenRuleServices.GetAllRecurrenceRulesAsync();
            return Ok(recurrences);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecurrenceRulesById(int id)
        {
            Data.RecurrenceRule recurrence = await _recurrenRuleServices.GetRecurrenceRuleByIdAsync(id);
            if (recurrence == null)
            {
                return BadRequest();
            }

            return Ok(recurrence);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecurrence([FromForm] RecurrenceRuleDTO recurrenceDto)
        {

            if (ModelState.IsValid)
            {
                await _recurrenRuleServices.AddRecurrenceTule(recurrenceDto);
                return Ok();
            }

            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecurrencesAsync(int id, [FromForm] RecurrenceRuleDTO recurrenceDto)
        {

            bool updateResult = await _recurrenRuleServices.UpdateRecurrenceRuleAsync(id, recurrenceDto);

            if (!updateResult)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"RecurrenceRule with ID {id} was not found or failed to update."
                });
            }

            return Ok(new
            {
                Status = "Success",
                Message = $"RecurrenceRule with ID {id} has been successfully updated."
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecurrendceAsync(int id)
        {

            bool deleteResult = await _recurrenRuleServices.DeleteRecurrenceRuleAsync(id);

            if (!deleteResult)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"RecurrenceRule with ID {id} was not found or could not be deleted."
                });
            }

            return Ok(new
            {
                Status = "Success",
                Message = $"RecurrenceRule with ID {id} has been successfully deleted."
            });
        }
    }
}
