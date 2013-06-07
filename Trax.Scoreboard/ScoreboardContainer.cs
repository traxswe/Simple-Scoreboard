using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace Trax.Scoreboard
{
    internal class ScoreboardContainer
    {
        public static Container Container { get; private set; }

        public static void SetupNewContainer()
        {
            var container = new Container();
            container.RegisterSingle<IScoreboardData, ScoreboardData>();
            container.Verify();
            Container = container;
        }
    }
}
