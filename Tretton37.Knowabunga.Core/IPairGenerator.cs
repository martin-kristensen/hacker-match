using System.Collections.Generic;

namespace Tretton37.Knowabunga.Core
{
    public interface IPairGenerator
    {
        IList<IList<Hacker>> Generate();
    }
}