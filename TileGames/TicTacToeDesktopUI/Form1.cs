using GameLogicPlugin;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeSimpleGameLogic;

namespace TicTacToeDesktopUI
{
    public partial class Form1 : Form
    {
        public IGameLogic _gameLogic = new GameLogic();
        private Dictionary<int, Button> playerOptions = new Dictionary<int, Button>();
        public Form1()
        {
            InitializeComponent();
            flowLayoutPanel1.BackColor = Color.DarkGray;
            int totalCells = _gameLogic.TileWidth * _gameLogic.TileHeight;
            for (int i = 0; i < totalCells; i++)
            {
                var button = CreateButton(i, "");
                playerOptions.Add(i, button);
                flowLayoutPanel1.Controls.Add(button);
            } 
            _gameLogic = new GameLogic();
            _gameLogic.AddPlayer(new Player { PlayerIndex = 0, PieceKey = "O" });
            _gameLogic.AddPlayer(new Player { PlayerIndex = 1, PieceKey = "X" });
            Size = new Size(20 + _gameLogic.TileWidth * 100, 20 + _gameLogic.TileHeight * 100);
        }
        Button CreateButton(int index, string defaultValue)
        {
            var button = new Button();
            button.FlatStyle = FlatStyle.Flat;
            button.Location = new Point(15, 15);
            button.Margin = new Padding(15);
            button.Name = index.ToString();
            button.Size = new Size(75, 75);
            button.TabIndex = 0;
            button.TabStop = false;
            button.Text = defaultValue;
            button.UseVisualStyleBackColor = true;
            button.BackColor = Color.WhiteSmoke;
            button.Click += ButtonClicked;
            return button;
        }

        private void ButtonClicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;
            if (_gameLogic.IsOver())
            {
                MessageBox.Show("The game is over fool!", "Game State");
                return;
            }

            UpdateButtonState(false);

            Task.Factory.StartNew(name => {
                var move = CreateMove(name as string, _gameLogic.GetCurrentPlayer());
                if (!_gameLogic.IsMoveValid(move))
                    return;
                _gameLogic.Move(move);
                UpdateButtonText(move);
                var boardState = _gameLogic.GetBoardState();
                switch (boardState)
                {
                    case BoardState.WON:break;
                    case BoardState.TIE:break;
                    case BoardState.CONTINUE:break;
                }
            }, button.Name).ContinueWith(task => UpdateButtonState(true));
        }
        private void UpdateButtonState(bool enabled)
        {
            if (playerOptions[0].InvokeRequired)
            {
                Invoke(new OnVoidMethod<bool>(UpdateButtonState), new object[] { enabled });
            }
            else
            {
                foreach (var button in playerOptions.Values) button.Enabled = enabled;
            }
        }
        private IMove<IPlayer> CreateMove(string buttonName, IPlayer player)
        {
            var buttonIndex = int.Parse(buttonName);
            return new Move
            {
                RowIndex = buttonIndex / _gameLogic.TileWidth,
                ColumnIndex = buttonIndex % _gameLogic.TileWidth,
                Player = player
            };
        }
        private void UpdateButtonText(IMove<IPlayer> move)
        {
            var buttonInQuestion = playerOptions[move.RowIndex * _gameLogic.TileWidth + move.ColumnIndex];
            if (buttonInQuestion.InvokeRequired)
            {
                Invoke(new OnVoidMethod<IMove<IPlayer>>(UpdateButtonText), new object[] { move });
                return;
            }
            buttonInQuestion.Text = move.Player.PieceKey;
        }
        private delegate void OnVoidMethod<T>(T param1);
    }

}
