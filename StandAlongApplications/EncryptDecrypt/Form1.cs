using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptDecrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //encrypt
        private void button1_Click(object sender, EventArgs e)
        {
            ScrambleAction(new Scramble(BusinessLogic.Encrypt));
        }

        //decrypt
        private void button2_Click(object sender, EventArgs e)
        {
            ScrambleAction(new Scramble(BusinessLogic.Decrypt));
        }
        private void ScrambleAction(Scramble direction)
        {
            SetEnabledProperties(false, textBox1, textBox2, textBox3, textBox4);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    UpdateOutputField(
                        direction(textBox2.Text, textBox1.Text, textBox4.Text));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            ).ContinueWith(t =>
            {
                SetEnabledProperties(true, textBox1, textBox2, textBox3, textBox4);
            });
        }
        private void UpdateOutputField(string value)
        {
            if (!textBox3.InvokeRequired)
            {
                textBox3.Text = value;
                return;
            }

            this.Invoke(new ShowOutput(UpdateOutputField), new object[]{ value });
        }
        private void SetEnabledProperties(bool enabled, params Control[] controls)
        {
            foreach (var control in controls) SetEnabledProperty(enabled, control);
        }
        private void SetEnabledProperty(bool enabled, Control control)
        {
            if (!control.InvokeRequired)
            {
                control.Enabled = enabled;
                return;
            }
            this.Invoke(new ChangeEnabledProperty(SetEnabledProperty), new object[]{ enabled, control });
        }
        private delegate void ChangeEnabledProperty(bool enabled, Control control);
        private delegate void ShowOutput(string value);
        private delegate string Scramble(string cipherText, string key, string initVector, int keySize = 256);

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox3.Text);
        }
    }
}
