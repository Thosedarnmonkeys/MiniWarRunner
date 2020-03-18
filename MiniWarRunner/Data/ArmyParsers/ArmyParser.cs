using MiniWarRunner.Data.Armies;
using MiniWarRunner.Data.ArmyParsers.BattleScribeHTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data.ArmyParsers
{
  public abstract class ArmyParser 
  {
    #region abstract members
    protected abstract List<GameSystem> SupportedSystems { get; }

    public abstract Army Parse(string armyString);
    #endregion

    #region protected fields
    protected readonly GameSystem gameSystem;
    #endregion

    #region constructors
    protected ArmyParser() { }

    protected ArmyParser(GameSystem gameSystem)
    {
      this.gameSystem = gameSystem;
    }
    #endregion

    #region public static methods
    public static bool IsSystemSupportedInFormat(ListFormat listFormat, GameSystem gameSystem)
    {
      List<GameSystem> supportedSystems;

      switch (listFormat)
      {
        case ListFormat.BattlesScribeHTML:
          supportedSystems = new BattleScribeHTMLArmyParser().SupportedSystems;
          break;
        case ListFormat.BattleScribeRosz:
        case ListFormat.WarscrollBuilder:
        default:
          throw new ArgumentOutOfRangeException(nameof(listFormat));
      }

      return supportedSystems.Contains(gameSystem);
    }

    public static ArmyParser CreateParser(ListFormat listFormat, GameSystem gameSystem)
    {
      switch (listFormat)
      {
        case ListFormat.BattlesScribeHTML:
          return new BattleScribeHTMLArmyParser(gameSystem);
        case ListFormat.BattleScribeRosz:
        case ListFormat.WarscrollBuilder:
        default:
          throw new ArgumentOutOfRangeException(nameof(ListFormat));
      }
    }
    #endregion
  }
}
