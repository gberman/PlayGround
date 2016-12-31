using System.Drawing;
using System.Windows.Forms;

namespace TicTacToeDesktopUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            flowLayoutPanel1.BackColor = Color.DarkGray;
            for (int i = 0; i < 9; i++)
            {
                flowLayoutPanel1.Controls.Add(CreateButton(i, ""));
            }
        }
        static Button CreateButton(int index, string defaultValue)
        {
            var button = new Button();
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.Location = new System.Drawing.Point(15, 15);
            button.Margin = new System.Windows.Forms.Padding(15);
            button.Name = index.ToString();
            button.Size = new System.Drawing.Size(75, 75);
            button.TabIndex = 0;
            button.TabStop = false;
            button.Text = defaultValue;
            button.UseVisualStyleBackColor = true;
            button.BackColor = Color.WhiteSmoke;
            button.Click += ButtonClicked;
            return button;
        }

        private static void ButtonClicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            button.Text = "X";
        }
    }
}
