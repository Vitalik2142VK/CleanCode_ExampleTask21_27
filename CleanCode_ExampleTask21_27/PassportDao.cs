using Microsoft.Data.Sqlite;
using System.Data;
using System.IO;
using System.Reflection;
using System;

namespace CleanCode_ExampleTask21_27
{
    class PassportDao
    {
        public DataTable FindPassportDataTableByNum(string num)
        {
            string commandText = string.Format("select * from passports where num='{0}' limit 1;", (object)Form1.ComputeSha256Hash(num));
            string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");
            string message = "";

            try
            {
                SqliteConnection connection = new SqliteConnection(connectionString);
                connection.Open();

                SqLiteDataAdapter sqLiteDataAdapter = new SqLiteDataAdapter(new SqliteCommand(commandText, connection));

                DataTable dataTable = new DataTable();
                sqLiteDataAdapter.Fill(dataTable);

                connection.Close();

                return dataTable;
            }
            catch (SqliteException ex)
            {
                if (ex.ErrorCode == 1)
                    message = "Файл db.sqlite не найден. Положите файл в папку вместе с exe.";
            }

            if (message.Length != 0)
                throw new FileNotFoundException(message);
            else
                throw new InvalidOperationException();
        }
    }
}
