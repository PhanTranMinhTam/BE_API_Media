using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhanTranMinhTam_TestLan2.Data;
using PhanTranMinhTam_TestLan2.Models;
using PhanTranMinhTam_TestLan2.Repository;

namespace PhanTranMinhTam_TestLan2.Services
{
    public interface IRecurrenceRuleServices
    {
        Task<IEnumerable<RecurrenceRule>> GetAllRecurrenceRulesAsync();
        Task<RecurrenceRule> GetRecurrenceRuleByIdAsync(int id);
        Task AddRecurrenceTule(RecurrenceRuleDTO recurrenceRuleDto);
        Task<bool> UpdateRecurrenceRuleAsync(int id, RecurrenceRuleDTO recurrenceDto);
        Task<bool> DeleteRecurrenceRuleAsync(int id);
    }
    public class RecurrenceRuleServices : IRecurrenceRuleServices
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public RecurrenceRuleServices(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<IEnumerable<RecurrenceRule>> GetAllRecurrenceRulesAsync()
        {
            return await _repositoryWrapper.RecurrenceRule.FindAll().ToListAsync();
        }
        public async Task<RecurrenceRule> GetRecurrenceRuleByIdAsync(int id)
        {
            return await _repositoryWrapper.RecurrenceRule.FindByCondition(u => u.RecurrenceRuleId == id).FirstOrDefaultAsync();
        }
        public async Task AddRecurrenceTule(RecurrenceRuleDTO recurrenceRuleDto)
        {
            RecurrenceRule recurrence = _mapper.Map<RecurrenceRule>(recurrenceRuleDto);

            _repositoryWrapper.RecurrenceRule.Create(recurrence);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<bool> UpdateRecurrenceRuleAsync(int id, RecurrenceRuleDTO recurrenceDto)
        {
            RecurrenceRule? existingRecurrence = await _repositoryWrapper.RecurrenceRule.FindByCondition(u => u.RecurrenceRuleId == id).FirstOrDefaultAsync();
            if (existingRecurrence == null)
            {
                return false;
            }
            _mapper.Map(recurrenceDto, existingRecurrence);

            _repositoryWrapper.RecurrenceRule.Update(existingRecurrence);
            await _repositoryWrapper.SaveAsync();

            return true;
        }
        public async Task<bool> DeleteRecurrenceRuleAsync(int id)
        {

            RecurrenceRule? existingrecurrence = await _repositoryWrapper.RecurrenceRule.FindByCondition(u => u.RecurrenceRuleId == id).FirstOrDefaultAsync();

            if (existingrecurrence == null)
            {
                return false;
            }

            _repositoryWrapper.RecurrenceRule.Delete(existingrecurrence);
            await _repositoryWrapper.SaveAsync();

            return true;
        }
    }
}
