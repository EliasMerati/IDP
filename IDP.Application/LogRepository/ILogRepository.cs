using IDP.Core.Entities;

namespace IDP.Application.LogRepository
{
    public interface ILogRepository
    {
        void execute(LogInfo log);
    }
}
