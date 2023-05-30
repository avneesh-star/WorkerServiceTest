using Npgsql;
using WorkerServiceTest.Data;

namespace WorkerServiceTest.Services
{
    public interface IDemoService
    {
        void DoSomething();
    }

    public class DemoService : IDemoService
    {
        private readonly IAppDbContext _dbContext;

        public DemoService(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DoSomething()
        {

            string query = "INSERT INTO public.demo (id, message) VALUES(@id, @message)";
            NpgsqlParameter[] parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id", Guid.NewGuid().ToString()),
                new NpgsqlParameter("@message", $"Task executes at {DateTime.Now}")
            };
            _dbContext.ExecuteNonQuery(query, parameters);
        }
    }
}
