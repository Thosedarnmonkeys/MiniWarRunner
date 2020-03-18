using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data.Armies.Warhammer40K
{
  public class Warhammer40KArmy : Army
  {
    public int CommandPoints { get; set; }

    public string Faction { get; set; }

    public List<Warhammer40kDetachment> Detatchments { get; set; } = new List<Warhammer40kDetachment>();

    public override List<IUnit> Units => Detatchments.SelectMany(x => x.Units.Cast<IUnit>()).ToList();

    public override GameSystem System => GameSystem.Warhammer40K;

  }
}
