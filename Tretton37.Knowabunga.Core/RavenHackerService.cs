using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;

namespace Tretton37.Knowabunga.Core
{
    public class RavenHackerService : IHackerService
    {
        private readonly IDocumentSession _session;

        public RavenHackerService(IDocumentSession session)
        {
            _session = session;
        }

        public IEnumerable<Hacker> GetHackers()
        {
            return _session.Query<Hacker>().Customize(c => c.WaitForNonStaleResultsAsOfNow()).OrderBy(h => h.Handle).ToArray();
        }

        public void Add(Hacker hacker)
        {
            _session.Store(hacker);
            _session.SaveChanges();
        }

        public void Update(Hacker hacker)
        {
            var targetHacker = GetHacker(hacker.Id);
            targetHacker.Handle = hacker.Handle;
            _session.SaveChanges();
        }

        public Hacker GetHacker(int id)
        {
            return _session.Load<Hacker>(id);
        }

        public void ToggleAvailability(int id)
        {
            var hacker = GetHacker(id);
            hacker.Available = !hacker.Available;
            _session.SaveChanges();
        }

        public IEnumerable<Hacker> GetAvailableHackers()
        {
            return _session.Query<Hacker>().Where(h => h.Available).OrderBy(h => h.Handle).ToArray();
        }
    }
}