using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace VulnerableApp.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class RegisterController : ControllerBase
  {
    [HttpPost]
    public string Register()
    {
      var username = Request.Form["username"];
      var password = Request.Form["password"];

      using (SqlConnection connection = new SqlConnection("Data Source=1.2.3.4; User_ID=MyUserID; Password=MyPassword"))
      {
        connection.Open();

        string query = $"SELECT * FROM USERS WHERE username='{username}' AND password='{password}'";
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
          reader.Close();
          return $"Username: {username} exists!!!";
        }
        reader.Close();

        string insertQuery = $"INSERT INTO USERS(USERNAME, PASSWORD) VALUES('{username}', '{password}')";
        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
        insertCommand.ExecuteNonQuery();
         
        return $"User created: {username}";
      }
    }
  }
}
