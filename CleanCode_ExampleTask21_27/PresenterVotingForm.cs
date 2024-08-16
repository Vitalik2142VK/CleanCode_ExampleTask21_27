using System;
using System.IO;

namespace CleanCode_ExampleTask21_27
{
    class PresenterVotingForm
    {
        private IViewForm _view;
        private ModelVotingForm _model;

        public PresenterVotingForm(IViewForm view, ModelVotingForm model)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public void HandleButtonClick(string passportData)
        {
            try
            {
                string answer = _model.GiveAnswerAccessBulletin(passportData);

                _view.SendAnswer(answer);
            } 
            catch (UnsuitableFormatDataPassportException e)
            {
                _view.SendAnswer(e.Message);
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException || e is ArgumentOutOfRangeException)
                    _view.ShowMessageBox(e.Message);
                else
                    throw;
            }
        }
    }
}
