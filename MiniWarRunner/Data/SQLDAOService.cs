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
  }
}
