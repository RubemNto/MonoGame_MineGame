using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mineGame
{
    public class Portal
    {
        private Point _position;
        private Game1 game;
        
        public Portal(Game1 game1, int x, int y)
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