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
        int rowWidth, rowHeight, playerSpeed = 3, ballSpeed = 5;

        List<Area> outAreas = new List<Area>();
        List<Area> gameAreas = new List<Area>();
        List<Ball> obstacles = new List<Ball>();

        Ball player;

        SolidBrush drawBrush = new SolidBrush(Color.Thistle);


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
            Area rec1 = new Area(0, 0, rowWidth * 2, this.Height); outAreas.Add(rec1); 
            Area rec2 = new Area(rowWidth * 2, 0, rowWidth * 2, rowHeight * 4); outAreas.Add(rec2);
            Area rec3 = new Area(rowWidth * 2, rowHeight * 7, rowWidth * 2, rowHeight * 4); outAreas.Add(rec3);
            Area rec4 = new Area(rowWidth * 4, 0, rowWidth, rowHeight * 5); outAreas.Add(rec4);
            Area rec5 = new Area(rowWidth * 4, rowHeight * 6, rowWidth, rowHeight * 5); outAreas.Add(rec5);
            Area rec6 = new Area(rowWidth * 5, 0, rowWidth * 10, rowHeight * 2); outAreas.Add(rec6);
            Area rec7 = new Area(rowWidth * 5, rowHeight * 9, rowWidth * 10, rowHeight * 2); outAreas.Add(rec7);
            Area rec8 = new Area(rowWidth * 15, 0, rowWidth, rowHeight * 5); outAreas.Add(rec8);
            Area rec9 = new Area(rowWidth * 15, rowHeight * 6, rowWidth, rowHeight * 5); outAreas.Add(rec9);
            Area rec10 = new Area(rowWidth * 16, 0, rowWidth * 2, rowHeight * 4); outAreas.Add(rec10);
            Area rec11 = new Area(rowWidth * 16, rowHeight * 7, rowWidth * 2, rowHeight * 4); outAreas.Add(rec11);
            Area rec12 = new Area(rowWidth * 18, 0, rowWidth * 2, this.Height); outAreas.Add(rec12);

            Area rec13 = new Area(rowWidth * 2, rowHeight * 4, rowWidth * 2, rowHeight * 3); gameAreas.Add(rec13);
            Area rec14 = new Area(rowWidth * 4, rowHeight * 5, rowWidth, rowHeight); gameAreas.Add(rec14);
            Area rec15 = new Area(rowWidth * 5, rowHeight * 2, rowWidth * 10, rowHeight * 7); gameAreas.Add(rec15);
            Area rec16 = new Area(rowWidth * 15, rowHeight * 5, rowWidth, rowHeight); gameAreas.Add(rec16);
            Area rec17 = new Area(rowWidth * 16, rowHeight * 4, rowWidth * 2, rowHeight * 3); gameAreas.Add(rec17);

            Ball ball1 = new Ball(rowWidth * 5, rowHeight * 2, rowWidth/2, ballSpeed); obstacles.Add(ball1);
            Ball ball2 = new Ball(rowWidth * 7, rowHeight * 2, rowWidth/2, ballSpeed); obstacles.Add(ball2);
            Ball ball3 = new Ball(rowWidth * 9, rowHeight * 2, rowWidth/2, ballSpeed); obstacles.Add(ball3);
            Ball ball4 = new Ball(rowWidth * 11, rowHeight * 2, rowWidth/2, ballSpeed); obstacles.Add(ball4);
            Ball ball5 = new Ball(rowWidth * 13, rowHeight * 2, rowWidth/2, ballSpeed); obstacles.Add(ball5);
            Ball ball6 = new Ball(rowWidth * 6, rowHeight * 8 + rowWidth / 2, rowWidth/2, -ballSpeed); obstacles.Add(ball6);
            Ball ball7 = new Ball(rowWidth * 8, rowHeight * 8 + rowWidth / 2, rowWidth/2, -ballSpeed); obstacles.Add(ball7);
            Ball ball8 = new Ball(rowWidth * 10, rowHeight * 8 + rowWidth / 2, rowWidth/2, -ballSpeed); obstacles.Add(ball8);
            Ball ball9 = new Ball(rowWidth * 12, rowHeight * 8 + rowWidth / 2, rowWidth/2, -ballSpeed); obstacles.Add(ball9);
            Ball ball10 = new Ball(rowWidth * 14, rowHeight * 8 + rowWidth / 2, rowWidth/2, -ballSpeed); obstacles.Add(ball10);

            player = new Ball(rowWidth * 3, rowHeight * 5, rowWidth / 2, playerSpeed);
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
            if (rightArrowDown) { player.x += playerSpeed; }
            if (leftArrowDown) { player.x -= playerSpeed; }
            if (upArrowDown) { player.y -= playerSpeed; }
            if (downArrowDown) { player.y += playerSpeed; }

            foreach (Ball b in obstacles)
            {
                b.Move();
                if (b.y <= rowHeight * 2) { b.speed = -b.speed; }
                else if (b.y >= rowHeight * 8 + rowWidth / 2) { b.speed = -b.speed; }
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (Area r in outAreas)
            {
                drawBrush.Color = Color.Thistle;
                e.Graphics.FillRectangle(drawBrush, r.x, r.y, r.width, r.height);
            }

            drawBrush.Color = Color.LimeGreen;
            e.Graphics.FillRectangle(drawBrush, gameAreas[0].x, gameAreas[0].y, gameAreas[0].width, gameAreas[0].height);
            e.Graphics.FillRectangle(drawBrush, gameAreas[4].x, gameAreas[4].y, gameAreas[4].width, gameAreas[4].height);

            foreach (Ball b in obstacles)
            {
                drawBrush.Color = Color.Blue;
                e.Graphics.FillEllipse(drawBrush, b.x, b.y, b.size, b.size);
            }

            drawBrush.Color = Color.MediumPurple;
            e.Graphics.FillRectangle(drawBrush, player.x, player.y, player.size, player.size);
        }
    }
}
