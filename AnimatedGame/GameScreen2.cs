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
    public partial class GameScreen2 : UserControl
    {
        int rowHeight, rowWidth, ballSpeed = 5;
        SolidBrush drawBrush = new SolidBrush(Color.Thistle);

        List<Area> outAreas = new List<Area>();
        List<Area> gameAreas = new List<Area>();
        List<Ball> obstacles = new List<Ball>();

        public GameScreen2()
        {
            InitializeComponent();

            rowWidth = this.Width / 20;
            rowHeight = this.Height / 13;
            CreateRectangles();
        }

        private void CreateRectangles ()
        {
            Area a1 = new Area(0, 0, rowWidth * 2, this.Height); outAreas.Add(a1);
            Area a2 = new Area(rowWidth * 2, 0, rowWidth * 3, rowHeight * 2); outAreas.Add(a2);
            Area a3 = new Area(rowWidth * 2, rowHeight * 9, rowWidth * 5, rowHeight * 2); outAreas.Add(a3);
            Area a4 = new Area(rowWidth * 5, 0, rowWidth, rowHeight * 8); outAreas.Add(a4);
            Area a5 = new Area(rowWidth * 6, 0, rowWidth * 7, rowHeight * 3); outAreas.Add(a5);
            Area a6 = new Area(rowWidth * 7, rowHeight * 8, rowWidth * 7, rowHeight * 3); outAreas.Add(a6);
            Area a7 = new Area(rowWidth * 13, 0, rowWidth * 5, rowHeight * 2); outAreas.Add(a7);
            Area a8 = new Area(rowWidth * 14, rowHeight * 3, rowWidth, rowHeight * 8); outAreas.Add(a8);
            Area a9 = new Area(rowWidth * 15, rowHeight * 9, rowWidth * 3, rowHeight * 2); outAreas.Add(a9);
            Area a10 = new Area(rowWidth * 18, 0, rowWidth * 2, this.Height); outAreas.Add(a10);

            Area a11 = new Area(rowWidth * 2, rowHeight * 2, rowWidth * 3, rowHeight * 7); gameAreas.Add(a11);
            Area a12 = new Area(rowWidth * 5, rowHeight * 8, rowWidth * 2, rowHeight * 1); gameAreas.Add(a12);
            Area a13 = new Area(rowWidth * 6, rowHeight * 3, rowWidth * 8, rowHeight * 5); gameAreas.Add(a13);
            Area a14 = new Area(rowWidth * 13, rowHeight * 2, rowWidth * 2, rowHeight * 1); gameAreas.Add(a14);
            Area a15 = new Area(rowWidth * 15, rowHeight * 2, rowWidth * 3, rowHeight * 7); gameAreas.Add(a15);

            Ball b1 = new Ball(rowWidth * 6, rowHeight * 3, rowHeight / 2, ballSpeed); obstacles.Add(b1);
            Ball b2 = new Ball(rowWidth * 6, rowHeight * 5, rowHeight / 2, ballSpeed); obstacles.Add(b2);
            Ball b3 = new Ball(rowWidth * 6, rowHeight * 7, rowHeight / 2, ballSpeed); obstacles.Add(b3);
            Ball b4 = new Ball(rowWidth * 13 + rowHeight / 2, rowHeight * 4, rowHeight / 2, ballSpeed); obstacles.Add(b4);
            Ball b5 = new Ball(rowWidth * 13 + rowHeight / 2, rowHeight * 6 , rowHeight / 2, ballSpeed); obstacles.Add(b5);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

        }

        private void GameScreen2_Paint(object sender, PaintEventArgs e)
        {
            foreach (Area a in outAreas)
            {
                drawBrush.Color = Color.Thistle;
                e.Graphics.FillRectangle(drawBrush, a.x, a.y, a.width, a.height);
            }

            drawBrush.Color = Color.LimeGreen;
            e.Graphics.FillRectangle(drawBrush, gameAreas[0].x, gameAreas[0].y, gameAreas[0].width, gameAreas[0].height);
            e.Graphics.FillRectangle(drawBrush, gameAreas[4].x, gameAreas[4].y, gameAreas[4].width, gameAreas[4].height);

            foreach (Ball b in obstacles)
            {
                drawBrush.Color = Color.Blue;
                e.Graphics.FillEllipse(drawBrush, b.x, b.y, b.size, b.size);
            }
        }
    }
}
