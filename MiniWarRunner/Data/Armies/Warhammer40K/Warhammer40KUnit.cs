using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data.Armies.Warhammer40K
{
  public class Warhammer40KUnit : IUnit
  {
    public string Name { get; set; }
    public int Points { get; set; }
    public Warhammer40KUnitSlot Slot { get; set; }
  }
}
