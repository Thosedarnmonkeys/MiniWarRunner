using MiniWarRunner.Data.Warhammer40K;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data.SQL
{
  public class SQLDAOService
  {
    private readonly string connectionString;

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
      var players = new Dictionary<int, Player>();

      using (var connection = new SqlConnection(connectionString))
      {
        using (SqlCommand command = connection.CreateCommand())
        {
          command.CommandText = "SELECT Players.Id, " + Environment.NewLine +
                                "       Players.Name, " + Environment.NewLine +
                                "       Armies.GameSystem, " + Environment.NewLine +
                                "       Armies.ListFormat, " + Environment.NewLine +
                                "       Armies.ArmyName, " + Environment.NewLine +
                                "       Armies.ArmyDetails " + Environment.NewLine +
                                "FROM Players " + Environment.NewLine +
                                "LEFT JOIN Armies ON Players.Id = Armies.PlayerId;";

          using (SqlDataReader reader = await command.ExecuteReaderAsync())
          {
            while(await reader.ReadAsync())
            {
              int playerId = reader.GetInt32(0);

              if (!players.ContainsKey(playerId))
              {
                string playerName = reader.GetString(1);

                var player = new Player();
                player.Id = playerId;
                player.Name = playerName;

                players[playerId] = player;
              }


            }
          }
        }
      }

      return players.Values.ToList();


      //List<Player> players = new List<Player>()
      //{
      //  new Player()
      //  {
      //    Name = "Hobbit",
      //    Armies = new List<Army>()
      //    {
      //      new Warhammer40KArmy(),
      //      new Warhammer40KArmy()
      //    }
      //  },
      //  new Player()
      //  {
      //    Name = "Lex",
      //    Armies = new List<Army>()
      //    {
      //      new Warhammer40KArmy(),
      //      new Warhammer40KArmy(),
      //      new Warhammer40KArmy()
      //    }
      //  },

      //};

      //return players;
    }

  }
}
