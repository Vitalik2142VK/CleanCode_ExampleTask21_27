using Microsoft.Data.Sqlite;
using System.Data;
using System.IO;
using System.Reflection;
using System;

namespace CleanCode_ExampleTask21_27
{
    class PassportDao
    {
        public Citizen FindCitizenByPassportNumHash(Passport passport, string numHash)
        {
            string commandText = string.Format("select * from passports where num='{0}' limit 1;", numHash);
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

                if (dataTable.Rows.Count == 0)
                {
                    message = $"Паспорт «{passport.Num}» в списке участников дистанционного голосования НЕ НАЙДЕН";

                    throw new InvalidOperationException(message);
                }

                return new Citizen(passport, Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]));
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
