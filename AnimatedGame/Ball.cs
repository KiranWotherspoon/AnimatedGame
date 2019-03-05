using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Move ()
        {
            y += speed;
        }
    }
}
