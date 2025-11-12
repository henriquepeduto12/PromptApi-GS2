using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromptApi.Models;
using PromptApi.Repository;

namespace PromptApi.Services
{
    public class PromptService : IPromptService
    {
        private readonly IPromptRepository _repo;

        public PromptService(IPromptRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Prompt>> GetAllAsync(string? tag = null)
        {
            var list = await _repo.GetAllAsync();
            if (string.IsNullOrWhiteSpace(tag)) return list;

            return list.Where(p =>
                (p.Tags ?? "")
                .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Any(t => string.Equals(t, tag, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<Prompt?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<Prompt> CreateAsync(Prompt input)
        {
            Validate(input, creation: true);
            input.CreatedAt = DateTime.UtcNow;
            input.UpdatedAt = null;
            input.Id = await _repo.AddAsync(input);
            return input;
        }

        public async Task UpdateAsync(int id, Prompt input)
        {
            if (id <= 0) throw new ArgumentException("Id inválido");
            Validate(input, creation: false);

            var existing = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Prompt não encontrado");

            existing.Title = input.Title;
            existing.Content = input.Content;
            existing.Tags = input.Tags;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido");
            await _repo.DeleteAsync(id);
        }

        private static void Validate(Prompt p, bool creation)
        {
            if (string.IsNullOrWhiteSpace(p.Title)) throw new ArgumentException("Title é obrigatório");
            if (string.IsNullOrWhiteSpace(p.Content)) throw new ArgumentException("Content é obrigatório");
            if (p.Title.Length > 120) throw new ArgumentException("Title muito longo");
        }
    }
}
