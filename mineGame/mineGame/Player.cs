using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

namespace mineGame
{
    public class Player
    {
        //private Point _position;
        private Game1 game;
        private bool _keysReleased = true;
        bool moved = false;
        private char[] _direction = {
            'L','U','D', 'R'
        };

        private char _dir;

        public Texture2D texture;
        public Vector2 _position;
        public bool faceRight = true;
        SoundEffect moveSound;
        SoundEffect sandDestruction;

        public Player(Game1 g, Vector2 position)
        {
            game = g;
            moveSound = g.Content.Load<SoundEffect>("moveSoundEffect");
            sandDestruction = g.Content.Load<SoundEffect>("sandSoundEffect");

            texture = g.Content.Load<Texture2D>("player");
            _position = position;
        }

        public void checkOrientation(ref bool pressingKeyDown) 
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (!pressingKeyDown)
            {
                Vector2 pastPosition = _position; 
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    Console.WriteLine("Go Up");
                    _dir = _direction[1];
                    _position.Y-=32;
                    pressingKeyDown = true;
                    moved = true;

                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    Console.WriteLine("Go Down");
                    _dir = _direction[2];
                    _position.Y+=32;
                    pressingKeyDown = true;
                    moved = true;

                }
                else if (keyboardState.IsKeyDown(Keys.Left))
                {
                    Console.WriteLine("Go Left");
                    _dir = _direction[0];
                    faceRight = false;
                    _position.X-=32;
                    pressingKeyDown = true;
                    moved = true;

                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    Console.WriteLine("Go Right");
                    _dir = _direction[3];
                    faceRight = true;
                    _position.X+=32;
                    pressingKeyDown = true;
                    moved = true;

                }


                for (int i = 0; i < game.GM.walls.Count; i++) 
                {
                    if (game.GM.walls[i].pos == _position) 
                    {
                        _position = pastPosition;
                        moved = false;
                        break;
                    }
                }

                if (moved)
                {
                    moveSound.Play(0.2f, 1f, 0f);
                    moved = false;
                }

                for (int i = 0; i < game.GM.sands.Count; i++)
                {
                    if (game.GM.sands[i].pos == _position)
                    {
                        game.GM.sands.RemoveAt(i);
                        sandDestruction.Play(0.5f, 1f, 0f);
                    }
                }
            }
            else 
            {
                if (keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyUp(Keys.Down) && keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Right))
                {
                    pressingKeyDown = false;
                }
            }
        }

        //public Player(Game1 game1, int x, int y)
        //{
        //    _position = new Point(x, y);
        //}

        //public void LoadContent()
        //{
        //    KeyboardState kState = Keyboard.GetState();
        //    if (_keysReleased)
        //    {
        //        Vector2 lastPosition = _position;
        //        _keysReleased = false;
        //        if (kState.IsKeyDown(Keys.A) || kState.IsKeyDown(Keys.Left))
        //        {
        //            _position.X--;
        //            _dir = _direction[0];
        //        }
        //        else if (kState.IsKeyDown(Keys.W) || kState.IsKeyDown(Keys.Up))
        //        {
        //            _position.Y--;
        //            _dir = _direction[1];
        //        }
        //        else if (kState.IsKeyDown(Keys.S) || kState.IsKeyDown(Keys.Down))
        //        {
        //            _position.Y++;
        //            _dir = _direction[2];
        //        }
        //        else if (kState.IsKeyDown(Keys.D) || kState.IsKeyDown(Keys.Right))
        //        {
        //            _position.X++;
        //            _dir = _direction[3];
        //        }
        //        else _keysReleased = true;
        //    }
        //}

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch sb)
        {
        }
    }
}
