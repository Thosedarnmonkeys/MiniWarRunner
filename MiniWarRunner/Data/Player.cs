using MiniWarRunner.Data.Armies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data
{
    public class Player
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Army> Armies { get; set; } = new List<Army>();
  }
}
