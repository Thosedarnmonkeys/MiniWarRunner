using MiniWarRunner.Data.Armies;
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
      throw new NotImplementedException();
    }
    #endregion
  }
}
