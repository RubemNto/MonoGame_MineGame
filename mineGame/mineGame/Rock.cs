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
        public bool _checkBottom;

        public Rock(Game1 g, Vector2 position)
        {
            texture = g.Content.Load<Texture2D>("rock");
            pos = position;
            destination = position;
            rockSpeed = g.tileSize * 10;
            _checkBottom = true;
        }

        public void update(GameTime gameTime, Game1 game)
        {
            //surroundings(game);
            if (moveTo(game , ref pos, destination, rockSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds))
            {
                checkBottom(game);
            }
        }

        public void updatePosition(Vector2 Destination,Game1 game)
        {
            if(freeTile(game,Destination)) destination = Destination;
        }
        public bool freeTile(Game1 game,Vector2 Destination)
        {
            foreach (Rock rock in game.GM.rocks)
            {
                if (rock.pos == Destination) return false;
            }
            foreach (Sand sand in game.GM.sands)
            {
                if (sand.pos == Destination) return false;
            }
            foreach (wall Wall in game.GM.walls)
            {
                if (Wall.pos == Destination) return false;
            }
            foreach (Bomb bomb in game.GM.bombs)
            {
                if (bomb.pos == Destination) return false;
            }
            foreach (Diamond diamond in game.GM.diamonds)
            {
                if (diamond.pos == Destination) return false;
            }
            return true;
        }

        private void checkBottom(Game1 game)
        {
            Vector2 bottomPos = pos + new Vector2(game.tileSize, 0);
            foreach (Rock rock in game.GM.rocks)
            {
                if (rock.pos == bottomPos)
                {
                    //stop there
                    destination = pos;
                    return;
                }
            }
            foreach (Sand sand in game.GM.sands)
            {
                if (sand.pos == bottomPos)
                {
                    //stop there
                    destination = pos;
                    return;
                }
            }
            foreach (wall wall in game.GM.walls)
            {
                if (wall.pos == bottomPos)
                {
                    //stop there
                    destination = pos;
                    return;
                }
            }
            foreach (Bomb bomb in game.GM.bombs)
            {
                if (bomb.pos == bottomPos)
                {
                    //stop there
                    destination = pos;
                    return;
                }
            }
            foreach (Diamond diamond in game.GM.diamonds)
            {
                if (diamond.pos == bottomPos)
                {
                    //stop there
                    destination = pos;
                    return;
                }
            }
            destination = bottomPos;
        }

        bool moveTo(Game1 g ,ref Vector2 originalPos, Vector2 destination, float Speed)
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
                return false;
            }
            else 
            {
                return true;
            }
        }

    }
}