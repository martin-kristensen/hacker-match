using System.Collections.Generic;
using Serilog;

namespace Tretton37.Knowabunga.Core
{
    public class LoggingHackerService : IHackerService
    {
        private readonly IHackerService _inner;
        private readonly ILogger _logger;

        public LoggingHackerService(IHackerService inner, ILogger logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public IEnumerable<Hacker> GetHackers()
        {
            var hackers = _inner.GetHackers();
            _logger.Information("GetHackers returned {@hackers}", hackers);
            return hackers;
        }

        public void Add(Hacker hacker)
        {
            _inner.Add(hacker);
            _logger.Information("Add called with {@hacker}", hacker);
        }

        public void Update(Hacker hacker)
        {
            _inner.Update(hacker);
            _logger.Information("Update called with {@hacker}", hacker);
        }

        public Hacker GetHacker(int id)
        {
            var hacker = _inner.GetHacker(id);
            _logger.Information("GetHacker called with {@id} returned {@hacker}", id, hacker);
            return hacker;
        }

        public void ToggleAvailability(int id)
        {
            _inner.ToggleAvailability(id);
            _logger.Information("ToggleAvailability called with {@id}", id);
        }

        public IEnumerable<Hacker> GetAvailableHackers()
        {
            var availableHackers = _inner.GetAvailableHackers();
            _logger.Information("GetAvailableHackers returned {@availableHackers}", availableHackers);
            return availableHackers;
        }
    }
}