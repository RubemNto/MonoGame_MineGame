using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mineGame
{
    public class Border
    {
        public Texture2D texture;
        public Vector2 pos;

        public Border(Game1 g, Vector2 position)
        {
            texture = g.Content.Load<Texture2D>("brick");
            pos = position;
        }

    }
}
