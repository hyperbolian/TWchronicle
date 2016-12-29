using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TWC
{
    public partial class Mainmenu : Form
    {
        public Mainmenu()
        {
            InitializeComponent();
        }

        private void start_Click(object sender, EventArgs e)
        {
            game game = new game();//產生Form2的物件，才可以使用它所提供的Method
            this.Visible = false;//將Form1隱藏。由於在Form1的程式碼內使用this，所以this為Form1的物件本身
            game.Visible = true;//顯示第二個視窗
            game.FormClosed += new FormClosedEventHandler(game_FormClosed);
        }

        void game_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = true;
        }
    }
}
