using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mineGame
{
    public class Brick
    {
        public Texture2D texture;
        public Vector2 pos;

        public Brick(Game1 g, Vector2 position)
        {
            texture = g.Content.Load<Texture2D>("brick");
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