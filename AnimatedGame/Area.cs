using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AnimatedGame
{
    class Area
    {
        public int x, y, width, height;

        public Area(int _x, int _y, int _width, int _height)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
        }

        public bool Collision (Ball b)
        {
            Rectangle rec1 = new Rectangle(x, y, width, height);
            Rectangle rec2 = new Rectangle(b.x, b.y, b.size, b.size);

            if (rec1.IntersectsWith(rec2))
            {
                return true;
            }
            return false;
        }
    }
}
