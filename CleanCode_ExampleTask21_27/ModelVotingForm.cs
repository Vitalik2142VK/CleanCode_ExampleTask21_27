using System;
using System.Data;
using System.Linq;

namespace CleanCode_ExampleTask21_27
{
    class ModelVotingForm
    {
        private const int MinCountPassportSymbols = 10;

        private PassportDao _dao;

        public ModelVotingForm(PassportDao dao)
        {
            _dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        public string GiveAnswerAccessBulletin(string passportData)
        {
            passportData = passportData.Trim();

            if (passportData == "")
            {
                string message = "Введите серию и номер паспорта";

                throw new ArgumentOutOfRangeException(message);
            }

            passportData = BringFormatDataPassport(passportData);
            DataTable passportDataTable = _dao.FindPassportDataTableByNum(passportData);

            return GetAnswer(passportDataTable, passportData);
        }

        private string BringFormatDataPassport(string dataPassport)
        {
            string formatDataPassport = dataPassport.Trim().Replace(" ", string.Empty);

            if (formatDataPassport.Length >= MinCountPassportSymbols)
                return formatDataPassport;

            string answer = "Неверный формат серии или номера паспорта";

            throw new UnsuitableFormatDataPassportException(answer);
        }
     
        private string GetAnswer(DataTable passportTable, string dataPassport)
        {
            if (passportTable.Rows.Count > 0)
            {
                string answer = $"По паспорту «{dataPassport}» доступ к бюллетеню на дистанционном электронном голосовании ";

                if (Convert.ToBoolean(passportTable.Rows[0].ItemArray[1]))
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
