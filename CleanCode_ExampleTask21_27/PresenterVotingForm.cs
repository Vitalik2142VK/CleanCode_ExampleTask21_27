using System;

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

        public void Enable()
        {
            _model.MessagePrepared += OnShowMessage;
            _model.AnswerPrepared += OnSendAnswer;
        }

        public void Disable()
        {
            _model.MessagePrepared -= OnShowMessage;
            _model.AnswerPrepared -= OnSendAnswer;
        }

        public void HandleButtonClick(string passportData)
        {
            _model.HandlePassportData(passportData);
        }

        private void OnShowMessage(string message)
        {
            _view.ShowMessageBox(message);
        }

        private void OnSendAnswer(string answer)
        {
            _view.SendAnswer(answer);
        }
    }
}
