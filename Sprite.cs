using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PingPong2
{
    class Sprite
    {
        public Bitmap CurrentSprite;
        public bool Show;
        public int _x, _y, _width, _height;
        public Sprite(string filename, int x, int y, int w, int h) 
        {
            CurrentSprite = new Bitmap(filename);
            _x = x;
            _y = y;
            _width = w;
            _height = h;
            Show = true;
        }

        public Sprite(string filename, int x, int y)
        {
            CurrentSprite = new Bitmap(filename);
            _x = x;
            _y = y;
            _width = CurrentSprite.Width;
            _height = CurrentSprite.Height;
            Show = true;
        }

        public bool SpriteCollision(Sprite s) 
        {
            Sprite temp = this;
            Rectangle A = new Rectangle(s._x, s._y, s._width, s._height);
            Rectangle B = new Rectangle(temp._x, temp._y, temp._width, temp._height);
            if (A.IntersectsWith(B))
                return true;
            else return false;
        }
    }
}
