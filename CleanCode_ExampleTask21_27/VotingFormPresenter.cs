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

        public void HandleButtonClick(string passportData)
        {
            try
            {
                Citizen citizen = _model.GiveAnswerAccessBulletin(passportData);
                string answer = GetAnswer(citizen);

                _view.SendAnswer(answer);
            }
            catch (FileNotFoundException e)
            {
                _view.ShowMessageBox(e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _view.ShowMessageBox(e.Message);
            }
            catch (UnsuitableFormatDataPassportException e)
            {
                _view.SendAnswer(e.Message);
            }
            catch (InvalidOperationException e)
            {
                if (string.IsNullOrEmpty(e.Message) == false)
                    _view.SendAnswer(e.Message);
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
