using IDP.Application.Context;
using IDP.Core.Entities;
using MongoDB.Driver;

namespace IDP.Application.LogRepository
{
    public class LogRipository : ILogRepository
    {
        private readonly IMongoDbContext<LogInfo> _database;
        private readonly IMongoCollection<LogInfo> _logs;

        public LogRipository(IMongoDbContext<LogInfo> database)
        {
            _database = database;
            _logs = _database.GetCollection();
        }
        public void execute(LogInfo log)
        {
            _logs.InsertOne(new LogInfo
            {
                Message = log.Message,
            });
        }
    }
}
