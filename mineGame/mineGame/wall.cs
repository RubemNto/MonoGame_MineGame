using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mineGame
{
    public class wall
    {
        public Texture2D texture;
        public Vector2 pos;

        public wall(Game1 g, Vector2 position) 
        {
            texture = g.Content.Load<Texture2D>("wall");
            pos = position;
        }

    }
}
