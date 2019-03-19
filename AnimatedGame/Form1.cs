using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimatedGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            MainMenu mm = new MainMenu();
            this.Controls.Add(mm);
        }

        static public void ChangeScreen (UserControl uc)
        {
            Form f = uc.FindForm();
            f.Controls.Remove(uc);
            uc.Dispose();

            MainMenu mm = new MainMenu();
            f.Controls.Add(mm);
        }
    }
}
