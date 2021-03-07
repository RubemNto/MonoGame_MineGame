using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mineGame
{
    public class Player
    {
        private Point _position;
        private Game1 game;
        private bool _keysReleased = true;
        private char[] _direction = {
            'L','U','D', 'R'
        };

        private char _dir;
        
        public Player(Game1 game1, int x, int y)
        {
            _position = new Point(x, y);
        }

        public void LoadContent()
        {
            KeyboardState kState = Keyboard.GetState();
            if (_keysReleased)
            {
                Point lastPosition = _position;
                _keysReleased = false;
                if (kState.IsKeyDown(Keys.A))
                {
                    _position.X--;
                    _dir = _direction[0];
                }
                else if (kState.IsKeyDown(Keys.W))
                {
                    _position.Y--;
                    _dir = _direction[1];
                }
                else if (kState.IsKeyDown(Keys.S))
                {
                    _position.Y++;
                    _dir = _direction[2];
                }
                else if (kState.IsKeyDown(Keys.D))
                {
                    _position.X++;
                    _dir = _direction[3];
                }
                else _keysReleased = true;
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch sb)
        {
        }
    }
}
