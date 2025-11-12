namespace PromptApi.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PromptApi.Models;

    public interface IPromptRepository
    {
        Task<IEnumerable<Prompt>> GetAllAsync();
        Task<Prompt?> GetByIdAsync(int id);
        Task<int> AddAsync(Prompt prompt);
        Task UpdateAsync(Prompt prompt);
        Task DeleteAsync(int id);
    }
}
