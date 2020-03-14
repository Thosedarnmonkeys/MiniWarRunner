﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data.Warhammer40K
{
  public class Warhammer40kDetachment
  {
    public string Name { get; set; }
    public int CommandPoints { get; set; }
    public List<Warhammer40KUnit> Units { get; set; }
  }
}
