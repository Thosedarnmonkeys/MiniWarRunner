using MiniWarRunner.Data.Armies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data.ArmyParsers.BattleScribeHTML.GameSystemParsers
{
  public abstract class SystemParser
  {
    public abstract Army Parse(string armyString);
  }
}
