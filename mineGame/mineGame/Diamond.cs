using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mineGame
{
    public class Diamond : Movement
    {
        private Point _position;
        private Game1 game;
        
        public Diamond(Game1 game1, int x, int y)
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