using System;
using System.Timers;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace Time_Spy
{
    public class Unit : Sprite
    {
        public bool selected;
        private static Timer aTimer;
        private Vector2f uPosition;
        private static Vector2f savedPosition;
        private static Vector2f inputPosition;
        private static float v = 5f;

        public Unit(Vector2f iPosition, int team)
        {
            selected = false;
            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5;
            aTimer.Enabled = false;
            
            this.Origin = new Vector2f(this.GetGlobalBounds().Left + this.GetGlobalBounds().Width / 2, this.GetGlobalBounds().Top + this.GetGlobalBounds().Height / 2);
            

            uPosition = iPosition;
            this.Position = uPosition;
            savedPosition = this.Position;
        }
        public void Move(Vector2f inputCoord)
        {
            this.Rotation = this.RotationAngle(inputCoord, this.Position);
            aTimer.Enabled = true;
            inputPosition = inputCoord;
            savedPosition = this.GetPosition();
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (this.Length(inputPosition - this.Position) > 0)
            {
                this.Position = this.Position + (this.Normalize(inputPosition - this.Position) * (Math.Min(v, this.Length(inputPosition - this.Position))));
            }
            else
            {
                aTimer.Enabled = false;
            }     
        }
        public float Length(Vector2f v)
        {
            return (float)(Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2)));
        }
        public Vector2f Normalize(Vector2f v)
        {
            return new Vector2f(v.X / this.Length(v), v.Y / this.Length(v));
        }
        public float RotationAngle(Vector2f iV1, Vector2f iV2)
        {
            float deltaX = (iV2.X - iV1.X);
            float deltaY = (iV2.Y - iV1.X);
            float theta = (float)(Math.Atan2(deltaY, deltaX) * (180/Math.PI))-90;
            return theta;
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
