using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CleanCode_ExampleTask21_27
{
    class ModelVotingForm
    {
        private const int MinCountPassportSymbols = 10;

        public event Action<string> MessagePrepared;
        public event Action<string> AnswerPrepared;

        public void HandlePassportData(string passportData)
        {
            passportData = passportData.Trim();

            if (passportData == "")
            {
                string message = "Введите серию и номер паспорта";

                MessagePrepared?.Invoke(message);

                return;
            }

            if (TryBringFormatDataPassport(out string formatDataPassport, passportData))
            {
                string answer = "Неверный формат серии или номера паспорта";

                AnswerPrepared?.Invoke(answer);
            }
            else
            {
                ExecuteSqlQuery(formatDataPassport);
            }
        }

        private bool TryBringFormatDataPassport(out string formatDataPassport, string dataPassport)
        {
            formatDataPassport = dataPassport.Trim().Replace(" ", string.Empty);

            if (formatDataPassport.Length < MinCountPassportSymbols)
                return false;
            else
                return true;
        }

        private void ExecuteSqlQuery(string passportData)
        {
            string commandText = string.Format("select * from passports where num='{0}' limit 1;", (object)Form1.ComputeSha256Hash(passportData));
            string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");

            try
            {
                SqliteConnection connection = new SqliteConnection(connectionString);
                connection.Open();

                SqLiteDataAdapter sqLiteDataAdapter = new SqLiteDataAdapter(new SqliteCommand(commandText, connection));

                DataTable dataTable = new DataTable();
                sqLiteDataAdapter.Fill(dataTable);

                string answer = GiveAnswer(dataTable, passportData);

                AnswerPrepared?.Invoke(answer);

                connection.Close();
            }
            catch (SqliteException ex)
            {
                if (ex.ErrorCode != 1)
                    return;

                string message = "Файл db.sqlite не найден. Положите файл в папку вместе с exe.";

                MessagePrepared?.Invoke(message);
            }
        }

        private string GiveAnswer(DataTable dataTable, string dataPassport)
        {
            if (dataTable.Rows.Count > 0)
            {
                string answer = $"По паспорту «{dataPassport}» доступ к бюллетеню на дистанционном электронном голосовании ";

                if (Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]))
                    return $"{answer} ПРЕДОСТАВЛЕН";
                else
                    return $"{answer} НЕ ПРЕДОСТАВЛЯЛСЯ";
            }
            else
            {
                return $"Паспорт «{dataPassport}» в списке участников дистанционного голосования НЕ НАЙДЕН";
            }
        }
    }
}
