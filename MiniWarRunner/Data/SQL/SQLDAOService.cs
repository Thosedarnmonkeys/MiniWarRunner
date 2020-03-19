using MiniWarRunner.Data.Armies;
using MiniWarRunner.Data.ArmyParsers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MiniWarRunner.Data.SQL
{
  public class SQLDAOService
  {
    #region private consts
    private const string createTablesSqlResourceKey = "MiniWarRunner.Data.SQL.CreateScript.sql";
    #endregion

    #region private fields
    private readonly string connectionString; 
    #endregion

    #region constructors
    public SQLDAOService(string connectionString)
    {
      this.connectionString = connectionString;
    }
    #endregion

    #region public methods
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

    public async Task CreateTables()
    {
      using (var connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = connection.CreateCommand())
        {
          string createTablesSql = ReadResourceFile(createTablesSqlResourceKey);
          command.CommandText = createTablesSql;

          await command.ExecuteNonQueryAsync();
        }

        await connection.CloseAsync();
      }
    }

    public async Task<List<Player>> GetPlayers()
    {
      var players = new Dictionary<int, Player>();

      using (var connection = new SqlConnection(connectionString))
      {
        await connection.OpenAsync();

        using (SqlCommand command = connection.CreateCommand())
        {
          command.CommandText = "SELECT Players.Id, " + Environment.NewLine +
                                "       Players.Name, " + Environment.NewLine +
                                "       Armies.ListFormat, " + Environment.NewLine +
                                "       Armies.GameSystem, " + Environment.NewLine +
                                "       Armies.ArmyName, " + Environment.NewLine +
                                "       Armies.ArmyDetails " + Environment.NewLine +
                                "FROM Players " + Environment.NewLine +
                                "LEFT JOIN Armies ON Players.Id = Armies.PlayerId;";

          using (SqlDataReader reader = await command.ExecuteReaderAsync())
          {
            while (await reader.ReadAsync())
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

              string listFormatString = reader.GetString(2);
              ListFormat format;
              if (!Enum.TryParse(listFormatString, out format))
                throw new Exception($"Couldn't parse string {listFormatString} to ListFormat");

              string gameSystemString = reader.GetString(3);
              GameSystem gameSystem;
              if (!Enum.TryParse(gameSystemString, out gameSystem))
                throw new Exception($"Couldn't parse string {gameSystemString} to GameSystem");

              if (!ArmyParser.IsSystemSupportedInFormat(format, gameSystem))
                throw new Exception($"Combination of list format {format} and game system {gameSystem} is invalid");

              string armyName = reader.GetString(4);
              string armyString = reader.GetString(5);

              ArmyParser parser = ArmyParser.CreateParser(format, gameSystem);
              Army army = parser.Parse(armyString);

              if (army.Name == null)
                army.Name = armyName;

              players[playerId].Armies.Add(army);
            }
          }
        }

        await connection.CloseAsync();
      }

      return players.Values.ToList();
    }
    #endregion


    #region private methods
    private string ReadResourceFile(string key)
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      using (Stream stream = assembly.GetManifestResourceStream(key))
      {
        using (StreamReader reader = new StreamReader(stream))
        {
          return reader.ReadToEnd();
        }
      }
    }
    #endregion
  }
}
