using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EfDapperComparison.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public DapperController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Insert()
        {
            var sql = "INSERT INTO testrecords (CreatedDate) VALUES (@CreatedDate) RETURNING Id;";
            var id = await _dbConnection.ExecuteScalarAsync<int>(sql, new { CreatedDate = DateTime.Now });
            var record = new { Id = id, CreatedDate = DateTime.Now };
            return Ok(record);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var sql = "UPDATE testrecords SET CreatedDate = @CreatedDate WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id, CreatedDate = DateTime.Now });
            var record = new { Id = id, CreatedDate = DateTime.Now };
            return Ok(record);
        }
    }
}
