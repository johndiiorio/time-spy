using System;
using System.Diagnostics;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace Time_Spy
{
    public class Building : Sprite
    {
        public bool selected;
        double n = 0.0;
        RectangleShape progBar;

        public Building(Vector2f iPosition, int team)
        {
            selected = false;
            progBar = new RectangleShape(new Vector2f(200,200));
            progBar.Size = new Vector2f(2,this.ProgressBarLength());

            this.Origin = new Vector2f(this.GetLocalBounds().Left + this.GetLocalBounds().Width / 2, this.GetLocalBounds().Top + this.GetLocalBounds().Height / 2);
            this.Position = iPosition;
        }
        public float ElapsedTime()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        public float ProgressBarLength()
        {
            //dummy statement
            return 100;
        }
        public Vector2f GetPosition()
        {
            return this.Position;
        }
        public void Select()
        {
            this.Texture = Program.myP.textureFighterOutline;
        }
        public bool ContainsPoint(Vector2f input)
        {
            float x = this.GetGlobalBounds().Left;
            float y = this.GetGlobalBounds().Top;
            float width = this.GetGlobalBounds().Width;
            float height = this.GetGlobalBounds().Height;
            return (input.X >= x && input.X <= x + width && input.Y >= y && input.Y <= y + height);
            //return (this.GetGlobalBounds()).Contains(input.X, input.Y);
        }
    }
}
