using MiniWarRunner.Data.Armies;
using MiniWarRunner.Data.ArmyParsers.BattleScribeHTML.GameSystemParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data.ArmyParsers.BattleScribeHTML
{
  public class BattleScribeHTMLArmyParser : ArmyParser
  {
    #region protected properties
    protected override List<GameSystem> SupportedSystems => new List<GameSystem>(){ GameSystem.Warhammer40K, GameSystem.WarhammerAoS};
    #endregion

    #region constructors
    public BattleScribeHTMLArmyParser(GameSystem gameSystem) : base(gameSystem) { }
    public BattleScribeHTMLArmyParser() : base() { }
    #endregion

    #region public methods
    public override Army Parse(string armyString)
    {
      SystemParser parser = GetParserForGameSystem(gameSystem);
      return parser.Parse(armyString);
    }
    #endregion

    #region private methods
    private SystemParser GetParserForGameSystem(GameSystem gameSystem)
    {
      switch (gameSystem)
      {
        case GameSystem.Warhammer40K:
          return new Warhammer40KParser();
        case GameSystem.WarhammerAoS:
        default:
          throw new ArgumentOutOfRangeException(nameof(gameSystem));
      }
    }
    #endregion
  }
}
