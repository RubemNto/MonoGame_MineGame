using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace mineGame
{
    public class Diamond : Movement
    {
        public Texture2D texture;
        public Vector2 pos;
        public Vector2 bottomPos;
        public float fallSpeed;
        Game1 game;

        public Diamond(Game1 g, Vector2 position)
        {
            fallSpeed = g.tileSize * 0.05f;
            game = g;
            texture = g.Content.Load<Texture2D>("diamond");
            pos = position;
            bottomPos = pos;
        }

        public void update(GameTime gameTime) 
        {
            checkBottom();
            moveTo(ref pos, bottomPos, fallSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
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

        //check if there is freetile bellow
        public void checkBottom() 
        {
            bottomPos = pos - new Vector2(0, game.tileSize);

            //check if all objects exist bellow
            for (int i = 0; i < game.GM.walls.Count; i++)
            {
                if (bottomPos == game.GM.walls[i].pos)
                {
                    Console.WriteLine(pos);
                    Console.WriteLine(bottomPos);
                    Console.WriteLine();
                    //bottomPos = pos;
                }
            }

            for (int i = 0; i < game.GM.sands.Count; i++)
            {
                if (bottomPos == game.GM.sands[i].pos)
                {
                    Console.WriteLine("found sands");
                    //bottomPos = pos;
                }
            }
        }
    }
}