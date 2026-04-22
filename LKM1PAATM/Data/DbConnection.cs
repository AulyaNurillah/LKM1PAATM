using Npgsql;

namespace LKM1PAATM.Data
{
    public class AppDbConnection
    {
        private readonly string _connectionString;

        public AppDbConnection(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
