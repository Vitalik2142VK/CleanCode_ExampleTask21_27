using System;
using System.Windows.Forms;

namespace CleanCode_ExampleTask21_27
{
    class ViewVotingForm
    {
        private PresenterVotingForm _presenter;
        private TextBox _passportTextbox;
        private TextBox _textResult;

        public ViewVotingForm(TextBox passportTextbox, TextBox textResult)
        {
            _passportTextbox = passportTextbox ?? throw new ArgumentNullException(nameof(passportTextbox));
            _textResult = textResult ?? throw new ArgumentNullException(nameof(textResult));
        }

        public void SendAnswer(string answer)
        {
            _textResult.Text = answer;
        }

        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message);
        }

        public void SetPresenter(PresenterVotingForm presenter)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        private void CheckButtonClick(object sender, EventArgs e)
        {
            string passportData = _passportTextbox.Text;

            _presenter.HandleButtonClick(passportData);
        }
    }
}
