namespace PromptApi.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PromptApi.Models;

    public interface IPromptService
    {
        Task<IEnumerable<Prompt>> GetAllAsync(string? tag = null);
        Task<Prompt?> GetByIdAsync(int id);
        Task<Prompt> CreateAsync(Prompt input);
        Task UpdateAsync(int id, Prompt input);
        Task DeleteAsync(int id);
    }
}
