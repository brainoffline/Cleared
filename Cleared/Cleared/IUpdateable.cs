using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleared
{
    public interface IUpdateable
    {
        Task Update(bool forced = false, int delay = 0);
    }
}
