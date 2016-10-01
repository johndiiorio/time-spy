using System;
using SFML;
using SFML.Graphics;
using SFML.Window;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Time_Spy
{
    public class Program
    {
        private int boardWidth;
        private int boardHeight;

        private int boardSpriteW;
        private int boardSpriteH;
        private int[,] board;

        public Texture texturegrass;
        public Texture textureFighter;
        public Texture textureFighterOutline;

        private Sprite[,] spriteBoard;

        public enum MousePressedState { None, Left, Right, Middle };
        public enum EventType { None, MousePressed, MouseMoved, MouseReleased, KeyPressed, KeyReleased, MouseWheel, Closed };
        public MousePressedState mouseButtonState = MousePressedState.None;
        public EventType eventState = EventType.None;

        static VideoMode mode = VideoMode.DesktopMode;
      //  static View view = new View(new Vector2f(mode.Width/2, mode.Height/2), new Vector2f(mode.Width, mode.Height));
        public View view;

        static Unit fighter;
        public RenderWindow app;
        public RenderTexture textApp;

        public static Program myP = new Program();

        public void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }
        public void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            eventState = EventType.MousePressed;
            if (e.Button == Mouse.Button.Left)
            {
                if (fighter.ContainsPoint(new Vector2f(Mouse.GetPosition().X, Mouse.GetPosition().Y)))
                {
                    fighter.Select();
                }
                mouseButtonState = MousePressedState.Left;               
            }
            if (e.Button == Mouse.Button.Right)
            {
                mouseButtonState = MousePressedState.Right;

                Vector2i pixelPos = Mouse.GetPosition();
                //Vector2f worldPos = myP.app.MapPixelToCoords(pixelPos, view);

                fighter.Move(new Vector2f(pixelPos.X, pixelPos.Y));
                //fighter.Move(new Vector2f(worldPos.X, worldPos.Y));
            }
            if (e.Button == Mouse.Button.Middle)
            {
                mouseButtonState = MousePressedState.Middle;
                OnClose(sender, e);
            }
        }
        public void OnKeyPressed(object sender, KeyEventArgs e)
        {
            eventState = EventType.KeyPressed;
            if (e.Code == Keyboard.Key.Up)
            {
                view.Center = new Vector2f(view.Center.X, view.Center.Y - 50);
            }
            else if (e.Code == Keyboard.Key.Down)
            {
                view.Center = new Vector2f(view.Center.X, view.Center.Y + 50);
            }
            else if (e.Code == Keyboard.Key.Left)
            {
                view.Center = new Vector2f(view.Center.X - 50, view.Center.Y);
            }
            else if (e.Code == Keyboard.Key.Right)
            {
                view.Center = new Vector2f(view.Center.X + 50, view.Center.Y);
            }
            else if (e.Code == Keyboard.Key.Escape)
            {
                OnClose(sender, e);
            }
            else if (e.Code == Keyboard.Key.S)
            {

            }
        }
        public static void Main()
        {
            Program myProgram = new Program();           

            View view = new View(new Vector2f(mode.Width/2, mode.Height/2), new Vector2f(mode.Width, mode.Height));

            myProgram.boardWidth = 5000;
            myProgram.boardHeight = 5000;

            myProgram.boardSpriteW = myProgram.boardWidth / 50;
            myProgram.boardSpriteH = myProgram.boardHeight / 50;
            myProgram.board = new int[myProgram.boardWidth, myProgram.boardHeight];

            myProgram.texturegrass = new Texture(@"resources\texture_grass.jpg");
            myProgram.textureFighter = new Texture(@"resources\ship_fighter_jets_blue_3d.png");
            myProgram.textureFighterOutline = new Texture(@"resources\ship_fighter_jets_blue_outline.png");

            myProgram.spriteBoard = new Sprite[myProgram.boardSpriteW, myProgram.boardSpriteH];

            fighter = new Unit(new Vector2f(0, 0), 0)
            {
                Texture = myProgram.textureFighter
            };
         
            Random rnd = new Random();
            for (int i = 0; i < myProgram.spriteBoard.GetLength(0); i++)
            {
                for (int j = 0; j < myProgram.spriteBoard.GetLength(1); j++)
                {
                    int rnum = rnd.Next(0, 1110);
                    Sprite addSprite = new Sprite(myProgram.texturegrass);
                    addSprite.Position = new Vector2f(i * 50, j * 50);

                    addSprite.TextureRect = new IntRect(rnum, rnum, 50, 50);
                    myProgram.spriteBoard[i, j] = addSprite;
                }
            }
            view.Viewport = new FloatRect(0, 0, 1, 1);
            View minimapView = new View(new FloatRect(0, 0, myProgram.boardWidth, myProgram.boardHeight));       
            minimapView.Viewport = new FloatRect(0, 0.75f, 0.25f, 0.25f);        
	  
            //myProgram.app = new RenderWindow(new VideoMode(5000, 5000), "Time Spy", Styles.Fullscreen);
            myProgram.app = new RenderWindow(VideoMode.DesktopMode, "Time Spy", Styles.Fullscreen);
            myProgram.textApp = new RenderTexture(mode.Width, mode.Height);
            myProgram.app.Closed += new EventHandler(myProgram.OnClose);
            myProgram.app.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(myProgram.MouseButtonPressed);
            myProgram.app.KeyPressed += new EventHandler<KeyEventArgs>(myProgram.OnKeyPressed);

            RectangleShape miniback = new RectangleShape(new Vector2f(minimapView.Viewport.Left, minimapView.Viewport.Top));
            miniback.Size = new Vector2f(minimapView.Viewport.Width * myProgram.app.Size.X + 5, minimapView.Viewport.Height * myProgram.app.Size.Y + 5);
            miniback.FillColor = Color.Black;
            miniback.FillColor = Color.Transparent;
            miniback.OutlineThickness = 4f;
            miniback.OutlineColor = Color.Black;


            while (myProgram.app.IsOpen())
            {
                myProgram.app.DispatchEvents();
                myProgram.textApp.Clear();
                myProgram.textApp.Draw(miniback);
                myProgram.textApp.Display();
                
                myProgram.app.Clear(Color.White);

                //Draw everything non-hud object twice
                //Main view
                myProgram.app.SetView(view);
                foreach (Sprite element in myProgram.spriteBoard)
                {
                    myProgram.app.Draw(element);
                }
                myProgram.app.Draw(fighter);       
               
                //Minimap View
                myProgram.app.SetView(minimapView);           
                foreach (Sprite element in myProgram.spriteBoard)
                {
                    myProgram.app.Draw(element);
                }                
                myProgram.app.Draw(fighter);              

                myProgram.app.Display();               
            }
        }
    }
}
