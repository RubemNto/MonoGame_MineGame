using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mineGame
{
    public class Sand
    {
        public Texture2D texture;
        public Vector2 pos;

        public Sand(Game1 g, Vector2 position)
        {
            texture = g.Content.Load<Texture2D>("sand");
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