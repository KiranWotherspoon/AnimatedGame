using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AnimatedGame
{
    class Ball
    {
        public int x, y, size, speed;

        public Ball(int _x, int _y, int _size, int _speed)
        {
            x = _x;
            y = _y;
            size = _size;
            speed = _speed;
        }

        public bool Collision (Area a)
        {
            Rectangle rec1 = new Rectangle(x, y, size, size);
            Rectangle rec2 = new Rectangle(a.x, a.y, a.width, a.height);

            if (rec1.IntersectsWith(rec2)) { return true; }
            return false;
        }

        public bool Collision (Ball b)
        {
            Rectangle rec1 = new Rectangle(x, y, size, size);
            Rectangle rec2 = new Rectangle(b.x + 2, b.y + 2, b.size - 4, b.size - 4);

            if (rec1.IntersectsWith(rec2))
            {
                return true;
            }
            return false;
        }

        public void Move (string direction)
        {
            if (direction == "left") { x -= speed; }
            else if (direction == "right") { x += speed; }
            else if (direction == "up") { y -= speed; }
            else if (direction == "down") { y += speed; }
        }
    }
}
