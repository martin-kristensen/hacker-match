using System.Collections.Generic;

namespace Tretton37.Knowabunga.Core
{
    public interface IHackerService
    {
        IEnumerable<Hacker> GetHackers();
        void Add(Hacker hacker);
        void Update(Hacker hacker);
        Hacker GetHacker(int id);
        void ToggleAvailability(int id);
        IEnumerable<Hacker> GetAvailableHackers();
    }
}