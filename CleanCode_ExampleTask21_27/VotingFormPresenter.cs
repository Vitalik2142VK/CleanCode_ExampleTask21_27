using System;
using System.IO;

namespace CleanCode_ExampleTask21_27
{
    class VotingFormPresenter
    {
        private IViewForm _view;
        private VotingFormModel _model;

        public VotingFormPresenter(IViewForm view, VotingFormModel model)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public void HandleButtonClick(string passportData, string passportDataHash)
        {
            try
            {
                Citizen citizen = _model.GiveAnswerAccessBulletin(passportData, passportDataHash);
                string answer = GetAnswer(citizen);

                _view.SendAnswer(answer);
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException || e is ArgumentOutOfRangeException)
                {
                    _view.ShowMessageBox(e.Message);
                }
                else if (e is UnsuitableFormatDataPassportException || e is InvalidOperationException)
                {
                    if (string.IsNullOrEmpty(e.Message) == false)
                        _view.SendAnswer(e.Message);
                }
                else
                {
                    throw;
                }
            }
        }

        private string GetAnswer(Citizen citizen)
        {
            Passport passport = citizen.Passport;
            string answer = $"По паспорту «{passport.Num}» доступ к бюллетеню на дистанционном электронном голосовании ";

            if (citizen.IsAvailableVote)
                return $"{answer} ПРЕДОСТАВЛЕН";
            else
                return $"{answer} НЕ ПРЕДОСТАВЛЯЛСЯ";
        }
    }
}
