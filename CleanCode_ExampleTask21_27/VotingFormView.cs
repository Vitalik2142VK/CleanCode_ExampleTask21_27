using System;
using System.Windows.Forms;

namespace CleanCode_ExampleTask21_27
{
    class VotingFormView
    {
        private VotingFormPresenter _presenter;
        private TextBox _passportTextbox;
        private TextBox _textResult;

        public VotingFormView(TextBox passportTextbox, TextBox textResult)
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

        public void SetPresenter(VotingFormPresenter presenter)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        private void CheckButtonClick(object sender, EventArgs e)
        {
            _presenter.HandleButtonClick(_passportTextbox.Text);
        }
    }
}
