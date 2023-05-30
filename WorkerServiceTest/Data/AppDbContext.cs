using Npgsql;
using System.Data;

namespace WorkerServiceTest.Data
{
    public interface IAppDbContext
    {
        int ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters);
    }

    public class AppDbContext : IAppDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly NpgsqlConnection _connection;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }


        public int ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters)
        {
            using var conn = _connection;
            conn.Open();
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddRange(parameters);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            return cmd.ExecuteNonQuery();
        }
    }
}
