using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mineGame
{
    public class Portal
    {
        public Texture2D texture;
        public Vector2 pos;
        int index;

        public Portal(Game1 g, Vector2 position,int portalIndex)
        {
            texture = g.Content.Load<Texture2D>("portal");
            pos = position;
            index = portalIndex;
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