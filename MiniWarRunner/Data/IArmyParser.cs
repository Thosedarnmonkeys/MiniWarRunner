using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data
{
  public interface IArmyParser
  {
    Army Parse(string armyString);
  }
}
