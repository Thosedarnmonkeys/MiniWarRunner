using MiniWarRunner.Data.Warhammer40K;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data
{
  public class SQLDAOService
  {
    private string connectionString;

    public SQLDAOService(string connectionString)
    {
      this.connectionString = connectionString;
    }

    public async Task<bool> TestConnect()
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        try
        {
          await connection.OpenAsync();
          return true;
        }
        catch (Exception)
        {
          return false;
        }
      }
    }

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
