using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mineGame
{
    public class Bomb : Movement
    {
        public Texture2D texture;
        public Vector2 pos;
        public Vector2 destination;
        public float bombSpeed;
        public bool _checkBottom;
        public bool exploding = false; //???
        public bool collectable = true;
        public float aboutToExplode = 3;
        public float animationTime = 0.5f;
        public int drawScale = 1;


        public Bomb(Game1 g, Vector2 position)
        {
            texture = g.Content.Load<Texture2D>("bombOff");
            pos = position;
            destination = position;
            bombSpeed = g.tileSize * 15;
            _checkBottom = true;
            collectable = true;
        }

        public void update(GameTime gameTime, Game1 game)
        {
            // Se collectable for true, o jogador ao posicionar junto à bomba destruir bomba da lista do GM
            // aumentar nº de bombas +1  
            // Se player instanciar uma nova bomba, diminuir nº de bombas -1, instanciar e collectable = false
            // Contagem da bomba

            // //surroundings(game);
            // if (moveTo(game, ref pos, destination, bombSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds))
            // {
            //     //checkBottom(game);
            // }

            if (!collectable)
            {
                countDown(ref aboutToExplode, gameTime, game);
            }
        }

        // public void updatePosition(Vector2 Destination, Game1 game)
        // {
        //     if (freeTile(game, Destination)) destination = Destination;
        // }

        public void removeAt(Game1 game, Vector2 Destination)
        {
            if(game.GM.player._position == Destination) game.GM.player.deadPlayer(game);
            
            foreach (Rock rock in game.GM.rocks)
            {
                if (rock.pos == Destination)
                {
                    game.GM.rocks.Remove(rock);
                    return;
                }
            }

            foreach (Sand sand in game.GM.sands)
            {
                if (sand.pos == Destination)
                {
                    game.GM.sands.Remove(sand);
                    return;
                }
            }

            foreach (wall Wall in game.GM.walls)
            {
                if (Wall.pos == Destination)
                {
                    game.GM.walls.Remove(Wall);
                    return;
                }
            }

            // foreach (Bomb bomb in game.GM.bombs)
            // {
            //     if (bomb.pos == Destination)
            //     {
            //         bomb.explosion(game);
            //         return;
            //     }
            // }

            foreach (Diamond diamond in game.GM.diamonds)
            {
                if (diamond.pos == Destination)
                {
                    game.GM.diamonds.Remove(diamond);
                    return;
                }
            }
            
            
        }

        bool moveTo(Game1 g, ref Vector2 originalPos, Vector2 destination, float Speed)
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
                else if (originalPos.Y < destination.Y && diference.Y < 0)
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

        public bool wait(ref float timer, GameTime gameTime)
        {
            if (timer > 0)
            {
                timer -= (float) gameTime.ElapsedGameTime.TotalSeconds;
                return false;
            }

            return true;
        }
        
        public void countDown(ref float timer, GameTime gameTime, Game1 game)
        {
            if (timer > 0)
            {
                timer -= (float) gameTime.ElapsedGameTime.TotalSeconds;
                if (wait(ref animationTime, gameTime) && drawScale == 1)
                {
                    animationTime = 0.5f;
                    drawScale = 2;
                }
                else if(wait(ref animationTime, gameTime) && drawScale == 2)
                {
                    animationTime = 0.5f;
                    drawScale = 1;
                }
            }
            else
            {
                explosion(game);
                game.GM.bombs.Remove(this);
                // for (int i = 0; i < game.GM.bombs.Count; i++)
                // {
                //     if (game.GM.bombs[i].Equals(this))
                //     {
                //         Console.WriteLine(this);
                //         game.GM.bombs.Remove(this);
                //     }
                // }
            }
        }

        public void explosion(Game1 game)
        {
            // canto superior esquerdo
            removeAt(game, pos + Vector2.One*game.tileSize);
            // esquerda
            removeAt(game, pos + new Vector2(game.tileSize,0));
            // canto inferior esquerdo
            removeAt(game, pos + new Vector2(game.tileSize,-game.tileSize));
            // canto superior esquerdo
            removeAt(game, pos - Vector2.One*game.tileSize);
            // esquerda
            removeAt(game, pos - new Vector2(game.tileSize,0));
            // canto inferior esquerdo
            removeAt(game, pos - new Vector2(game.tileSize,-game.tileSize));
            removeAt(game, pos - new Vector2(0,game.tileSize));
            removeAt(game, pos - new Vector2(0,-game.tileSize));
            removeAt(game,pos);
            
        }
    }
}