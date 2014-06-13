using System.Collections.Generic;
using System.Linq;
using Tretton37.Knowabunga.Core.Extensions;

namespace Tretton37.Knowabunga.Core
{
    public class PairGenerator : IPairGenerator
    {
        private readonly IHackerService _hackerService;

        public PairGenerator(IHackerService hackerService)
        {
            _hackerService = hackerService;
        }

        public IList<IList<Hacker>> Generate()
        {
            var hackers = _hackerService.GetAvailableHackers();
            return hackers
                .Shuffle()
                .Select((h, i) => new {Hacker = h, Index = i})
                .GroupBy(x => x.Index/2)
                .Select(g => (IList<Hacker>) g.Select(x => x.Hacker).ToList())
                .ToList();
        }
    }
}