using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mineGame
{
    public class Ghost
    {

        //___________________ """Artificial Inteligence"""_______________________________//  

        /*
                - Prioriza a posição Y (horizontal) e depois a posição X (vertical)

                - A cada Z(milisegundos) o Zombie vai analisar a posição atual do Player, caso este se encontre à sua direita
        o zombie vai andar um gameTile para a direita e vice-versa.

                -Quando o zombie se encontrar na mesma posição Y (horizontal) que o player ele vai comparar a sua posição X com a do Player
        e caso este se encontre acima vai andar um gameTile para cima e vice-versa.


                                                                                                                                         */
        public Texture2D texture;
        public Vector2 pos;
        public Vector2 destination;
        public Vector2 originalPos;
        public float ghostSpeed;

        public Rectangle hitbox = new Rectangle();

        public Ghost (Game1 g, Vector2 position)
        {
            texture = g.Content.Load<Texture2D>("ghost");
            pos = position;
            originalPos = position;
            destination = position;
            ghostSpeed = g.tileSize * 0;
        }


        public void LoadContent()
        {
        }

        public void Update(Game1 game ,GameTime gameTime)
        {
            if (!game.GM.player.dead)
            {
                destination = game.GM.player._position;
                moveTo(ref pos, destination, ghostSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                if(hitbox.Intersects(game.GM.player.hitbox))
                {                    
                    game.GM.player.deadPlayer(game);
                    pos = originalPos;
                }

                //if (pos == destination)
                //{
                //    game.GM.player.deadPlayer(game);
                //}

            }
        }

        public void Draw(SpriteBatch sb)
        {
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
