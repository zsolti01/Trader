﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
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

                cmd.ExecuteNonQuery();

                conn._connection.Close();

                return new { message = "Sikeres hozzáadás!" };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message };
            }
        }

        public object LoginUser(object user)
        {
            conn._connection.Open ();

            string sql = "SELECT * FROM users WHERE UserName = @username AND Password = @password";

            MySqlCommand cmd = new MySqlCommand (sql, conn._connection);

            var logUser = user.GetType().GetProperties();

            cmd.Parameters.AddWithValue("@username", logUser[0].GetValue(user));
            cmd.Parameters.AddWithValue("@password", logUser[1].GetValue(user));

            MySqlDataReader reader = cmd.ExecuteReader();

            object isRegistered = reader.Read() ? new { message = "Regisztált" } : new { message = "Nem regisztrált" };

            conn._connection.Close();

            return isRegistered;
        }

        public DataView UserList()
        {
            conn._connection.Open();

            string sql = "SELECT * FROM users";

            MySqlCommand cmd = new MySqlCommand(sql, conn._connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn._connection);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            conn._connection.Close();

            return dt.DefaultView;
        }
    }
}
