using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Trader
{
    internal class DatabaseStatements
    {
        Connect conn = new Connect();

        public object AddNewUser(object user)
        {
            try
            {
                conn._connection.Open();

                string sql = "INSERT INTO `users`(`UserName`, `FullName`, `PASSWORD`, `Salt`, `Email`) VALUES (@username,@fullname,@password,@salt,@email)";

                MySqlCommand cmd = new MySqlCommand(sql, conn._connection);

                var newUser = user.GetType().GetProperties();

                cmd.Parameters.AddWithValue("@username", newUser[0].GetValue(user));
                cmd.Parameters.AddWithValue("@fullname", newUser[1].GetValue(user));
                cmd.Parameters.AddWithValue("@password", newUser[2].GetValue(user));
                cmd.Parameters.AddWithValue("@salt", newUser[3].GetValue(user));
                cmd.Parameters.AddWithValue("@email", newUser[4].GetValue(user));

                conn._connection.Close();

                return new { message = "Sikeres hozzáadás!" };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message };
            }
        }
    }
}
