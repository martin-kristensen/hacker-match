using System.Collections.Generic;
using System.Linq;

namespace Tretton37.Knowabunga.Core
{
    public class InMemoryHackerService : IHackerService
    {
        private readonly List<Hacker> _hackers;

        public InMemoryHackerService()
        {
            _hackers = new List<Hacker>
            {
                new Hacker {Id = 1, Handle = "disco"},
                new Hacker {Id = 2, Handle = "allram"},
                new Hacker {Id = 2, Handle = "bob terminator"}
            };
        }

        public IEnumerable<Hacker> GetHackers()
        {
            return _hackers;
        }

        public void Add(Hacker hacker)
        {
            hacker.Id = GetHackers().Max(h => h.Id) + 1;
            _hackers.Add(hacker);
        }

        public void Update(Hacker hacker)
        {
            var targetHacker = GetHacker(hacker.Id);
            targetHacker.Handle = hacker.Handle;
        }

        public Hacker GetHacker(int id)
        {
            return _hackers.Single(h => h.Id == id);
        }

        public void ToggleAvailability(int id)
        {
            var targetHacker = GetHacker(id);
            targetHacker.Available = !targetHacker.Available;
        }

        public IEnumerable<Hacker> GetAvailableHackers()
        {
            return GetHackers().Where(h => h.Available);
        }
    }
}
