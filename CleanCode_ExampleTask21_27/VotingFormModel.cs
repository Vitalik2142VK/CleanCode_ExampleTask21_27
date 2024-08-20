using System;
using System.Data;

namespace CleanCode_ExampleTask21_27
{
    class VotingFormModel
    {
        private PassportDao _dao;

        public VotingFormModel(PassportDao dao)
        {
            _dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        public Citizen GiveAnswerAccessBulletin(string passportData)
        {
            string passportDataHash = (object)Form1.ComputeSha256Hash(passportData);
            Passport passport = new Passport(passportData, passportDataHash);

            return _dao.FindCitizenByPassport(passport);
        }
    }
}
