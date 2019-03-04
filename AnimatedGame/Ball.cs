using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatedGame
{
    class Ball
    {
        static int x, y, speed;

        public Ball(int _x, int _y, int _speed)
        {
            x = _x;
            y = _y;
            speed = _speed;
        }

        public void Move ()
        {
            y += speed;
        }
    }
}
