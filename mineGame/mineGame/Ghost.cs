using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mineGame
{
    class Ghost
    {

        //___________________ ""Artificial Inteligence""_______________________________//  

        /*
                - Prioriza a posição Y (horizontal) e depois a posição X (vertical)

                - A cada Z(milisegundos) o Zombie vai analisar a posição atual do Player, caso este se encontre à sua direita
        o zombie vai andar um gameTile para a direita e vice-versa.

                -Quando o zombie se encontrar na mesma posição Y (horizontal) que o player ele vai comparar a sua posição X com a do Player
        e caso este se encontre acima vai andar um gameTile para cima e vice-versa.


                                                                                                                                         */
        public Texture2D texture;
        public Vector2 pos;

        public Ghost (Game1 g, Vector2 position)
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
