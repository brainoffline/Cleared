using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleared.Model
{

    public class GameHighScore
    {
        public string PackName { get; set; }
        public string SetName { get; set; }
        public int GameIndex { get; set; }
        public TimeSpan TimeTaken { get; set; }
    }

}
