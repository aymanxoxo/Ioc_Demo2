using DataEntities;
using Interfaces;
using System;

namespace Services
{
    public class DatabaseLogService : ILogService
    {
        private readonly IRepository<Log> _logRepository;
        public DatabaseLogService(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        public void Dispose()
        {
            _logRepository.Dispose();
        }

        public void Log(string url, string exception)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrEmpty(exception)) throw new ArgumentNullException(nameof(exception));
            _logRepository.Add(new Log {Url = url, LogMessage = exception });
            _logRepository.SaveChanges();
        }
    }
}
