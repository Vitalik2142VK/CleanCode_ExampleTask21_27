using System;

namespace CleanCode_ExampleTask21_27
{
    class Passport
    {
        private const int MinCountPassportSymbols = 10;

        public Passport(string num)
        {
            if (num == null)
                throw new ArgumentNullException(nameof(num));

            Num = BringFormatDataPassport(num);
        }

        public string Num { get; private set; }

        private string BringFormatDataPassport(string passportData)
        {
            if (passportData == "")
            {
                string message = "Введите серию и номер паспорта";

                throw new ArgumentOutOfRangeException(message);
            }

            string formatDataPassport = passportData.Trim().Replace(" ", string.Empty);

            if (formatDataPassport.Length < MinCountPassportSymbols)
            {
                string answer = "Неверный формат серии или номера паспорта";

                throw new UnsuitableFormatDataPassportException(answer);
            }

            return formatDataPassport;
        }
    }
}
