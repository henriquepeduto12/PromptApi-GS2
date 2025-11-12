using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using PromptApi.Models;
namespace PromptApi.Repository
{
    public class PromptRepository : IPromptRepository
    {
        private readonly string _cs;

        public PromptRepository(IConfiguration cfg)
        {
            _cs = cfg.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<Prompt>> GetAllAsync()
        {
            const string sql = @"SELECT Id, Title, Content, Tags, CreatedAt, UpdatedAt
                                 FROM Prompts ORDER BY Id DESC;";
            await using var conn = new MySqlConnection(_cs);
            return await conn.QueryAsync<Prompt>(sql);
        }

        public async Task<Prompt?> GetByIdAsync(int id)
        {
            const string sql = @"SELECT Id, Title, Content, Tags, CreatedAt, UpdatedAt
                                 FROM Prompts WHERE Id=@id;";
            await using var conn = new MySqlConnection(_cs);
            return await conn.QueryFirstOrDefaultAsync<Prompt>(sql, new { id });
        }

        public async Task<int> AddAsync(Prompt prompt)
        {
            const string sql = @"
INSERT INTO Prompts (Title, Content, Tags, CreatedAt, UpdatedAt)
VALUES (@Title, @Content, @Tags, @CreatedAt, @UpdatedAt);
SELECT LAST_INSERT_ID();";
            await using var conn = new MySqlConnection(_cs);
            return await conn.ExecuteScalarAsync<int>(sql, prompt);
        }

        public async Task UpdateAsync(Prompt prompt)
        {
            const string sql = @"
UPDATE Prompts
   SET Title=@Title, Content=@Content, Tags=@Tags, UpdatedAt=@UpdatedAt
 WHERE Id=@Id;";
            await using var conn = new MySqlConnection(_cs);
            await conn.ExecuteAsync(sql, prompt);
        }

        public async Task DeleteAsync(int id)
        {
            const string sql = @"DELETE FROM Prompts WHERE Id=@id;";
            await using var conn = new MySqlConnection(_cs);
            await conn.ExecuteAsync(sql, new { id });
        }
    }
}
