using MiniWarRunner.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data
{
  public abstract class Army
  {
    #region virtual properties
    public virtual string Name { get; set; }
    #endregion

    #region abstract properties
    public abstract List<IUnit> Units { get; }
    public abstract GameSystem System { get; } 
    #endregion

  }
}
