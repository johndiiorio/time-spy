using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace Time_Spy
{
    class Algebra
    {
        public float GetSlope(Vector2f iP1, Vector2f iP2)
        {
            return (iP2.Y - iP1.Y) / (iP2.X - iP1.X);
        }
        public float GetDistance(Vector2f iP1, Vector2f iP2)
        {
            return (float)(Math.Sqrt(Math.Pow(iP2.X - iP1.X, 2) + Math.Pow(iP2.Y - iP1.Y, 2)));
        }
        public float DotProduct(Vector2f iP1, Vector2f iP2)
        {
            return iP1.X * iP2.X + iP1.Y * iP2.Y;
        }
    }
}
