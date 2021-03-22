using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace mineGame
{
    public class Rock : Movement
    {
        public Texture2D texture;
        public Vector2 pos;
        public Vector2 destination;
        public float rockSpeed;
        
        public Rock(Game1 g, Vector2 position)
        {
            texture = g.Content.Load<Texture2D>("rock");
            pos = position;
            destination = position;
            rockSpeed = g.tileSize * 8;
        }

        public void update(GameTime gameTime)
        {
            moveTo(ref pos, destination, rockSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void updatePosition(Vector2 Destination)
        {
            destination = Destination;
        }

        void moveTo(ref Vector2 originalPos, Vector2 destination, float Speed)
        {
            if (originalPos != destination)
            {
                //Console.WriteLine($"moving from " + originalPos + " to " + destination);
                Vector2 diference = destination - originalPos;
                originalPos += Vector2.Normalize(diference) * Speed;
                if (originalPos.X < destination.X && diference.X < 0)
                {
                    originalPos = destination;
                    //Console.WriteLine("moving {0} from {1} to {2}", "player", originalPos, destination);
                }
                else if (originalPos.X > destination.X && diference.X > 0)
                {
                    originalPos = destination;
                    //Console.WriteLine("moving {0} from {1} to {2}","player",originalPos,destination);
                }

                if (originalPos.Y < destination.Y && diference.Y < 0)
                {
                    originalPos = destination;
                    //Console.WriteLine("moving {0} from {1} to {2}","player",originalPos,destination);
                }
                else if (originalPos.Y > destination.Y && diference.Y > 0)
                {
                    originalPos = destination;
                    //Console.WriteLine("moving {0} from {1} to {2}","player",originalPos,destination);
                }
            }

        }

    }
}