using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimatedGame
{
    public partial class GameScreen : UserControl
    {
        int rowWidth, rowHeight;
        List<Box> borderRecs = new List<Box>();
        List<Ball> balls = new List<Ball>();
        SolidBrush drawBrush = new SolidBrush(Color.Thistle);
        Rectangle endZone, startZone;

        bool rightArrowDown, leftArrowDown, upArrowDown, downArrowDown;

        public GameScreen()
        {
            InitializeComponent();

            rowWidth = this.Width / 20;
            rowHeight = this.Height / 13;
            CreateRectangles();
            
        }

        private void CreateRectangles ()
        {
            Box rec1 = new Box(0, 0, rowWidth * 2, this.Height); borderRecs.Add(rec1); 
            Box rec2 = new Box(rowWidth * 2, 0, rowWidth * 2, rowHeight * 4); borderRecs.Add(rec2);
            Box rec3 = new Box(rowWidth * 2, rowHeight * 7, rowWidth * 2, rowHeight * 4); borderRecs.Add(rec3);
            Rectangle rec4 = new Rectangle(rowWidth * 4, 0, rowWidth, rowHeight * 5); borderRecs.Add(rec4);
            Rectangle rec5 = new Rectangle(rowWidth * 4, rowHeight * 6, rowWidth, rowHeight * 5); borderRecs.Add(rec5);
            Rectangle rec6 = new Rectangle(rowWidth * 5, 0, rowWidth * 10, rowHeight * 3); borderRecs.Add(rec6);
            Rectangle rec7 = new Rectangle(rowWidth * 5, rowHeight * 8, rowWidth * 10, rowHeight * 3); borderRecs.Add(rec7);
            Rectangle rec8 = new Rectangle(rowWidth * 15, 0, rowWidth, rowHeight * 5); borderRecs.Add(rec8);
            Rectangle rec9 = new Rectangle(rowWidth * 15, rowHeight * 6, rowWidth, rowHeight * 5); borderRecs.Add(rec9);
            Rectangle rec10 = new Rectangle(rowWidth * 16, 0, rowWidth * 2, rowHeight * 4); borderRecs.Add(rec10);
            Rectangle rec11 = new Rectangle(rowWidth * 16, rowHeight * 7, rowWidth * 2, rowHeight * 4); borderRecs.Add(rec11);
            Rectangle rec12 = new Rectangle(rowWidth * 18, 0, rowWidth * 2, this.Height); borderRecs.Add(rec12);

            endZone = new Rectangle(rowWidth * 16, rowHeight * 4, rowWidth * 2, rowHeight * 3);
            startZone = new Rectangle(rowWidth * 2, rowHeight * 4, rowWidth * 2, rowHeight * 3);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (Rectangle r in borderRecs)
            {
                drawBrush.Color = Color.Thistle;
                e.Graphics.FillRectangle(drawBrush, r);
            }
            drawBrush.Color = Color.LimeGreen;
            e.Graphics.FillRectangle(drawBrush, endZone);
            e.Graphics.FillRectangle(drawBrush, startZone);
        }
    }
}
