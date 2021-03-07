using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mineGame
{
    public class Player
    {
        private Point _position;
        private Game1 game;
        
        public Player(Game1 game1, int x, int y)
        {
            _position = new Point(x, y);
        }

        public void LoadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch sb)
        {
        }
    }
}