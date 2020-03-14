using MiniWarRunner.Data.Warhammer40K;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data
{
  public class DbService
  {
    public async Task<List<Player>> GetPlayers()
    {
      List<Player> players = new List<Player>()
      {
        new Player()
        {
          Name = "Hobbit",
          Armies = new List<Army>()
          {
            new Warhammer40KArmy(),
            new Warhammer40KArmy()
          }
        },
        new Player()
        {
          Name = "Lex",
          Armies = new List<Army>()
          {
            new Warhammer40KArmy(),
            new Warhammer40KArmy(),
            new Warhammer40KArmy()
          }
        },

      };

      return players;
    }
  }
}
