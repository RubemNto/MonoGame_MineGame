using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mineGame
{
    public class Diamond : Movement
    {
        public Texture2D texture;
        public Vector2 pos;

        public Diamond(Game1 g, Vector2 position)
        {
            texture = g.Content.Load<Texture2D>("diamond");
            pos = position;
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