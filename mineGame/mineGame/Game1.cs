﻿using System.IO;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mineGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        Rectangle position;

        public GameManager GM;

        public char[,] level;
        public int tileSize = 32;
        public bool pressingDown = false;

        public int windowHeight;
        public int windowWidth;

        Song backgroundMusic;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            if (GM == null)
            {
                GM = new GameManager(this, 0);
            }
            else
            {
                GM = new GameManager(this, GM.points, GM.levelIndex);
            }
            GM.loadLevel(GM.levelNames[GM.levelIndex], out level);

            windowHeight = level.GetLength(0) * tileSize;
            windowWidth = tileSize * level.GetLength(1);
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        //void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        //{
        //    //0.0f is silent, 1.0f is full volume
        //    MediaPlayer.Volume = 0f;
        //    MediaPlayer.IsRepeating = true;
        //    MediaPlayer.Play(backgroundMusic);
        //}

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            this.backgroundMusic = Content.Load<Song>("backgroundmusic");
            font = Content.Load<SpriteFont>("font");

            MediaPlayer.Volume = 0.5f;

            MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(backgroundMusic);
        }

        protected override void Update(GameTime gameTime)
        {
            IsMouseVisible = true;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (GM.levelIndex == GM.levelNames.Length - 1)
                {       
                    GM.levelIndex = 0;
                    GM.points = 0;
                    GM.changeLevel = true;
                    Initialize();
                }
            }

            GM.UpdateGame(this, gameTime, ref pressingDown);
            if (GM.changeLevel && GM.player.lives >= 0)
            {
                GM.levelIndex++;
                GM.changeLevel = false;
                Console.Clear();
                Initialize();
            }
            else if (GM.changeLevel && GM.player.lives < 0)
            {
                GM.levelIndex = GM.levelNames.Length - 1;
                GM.changeLevel = false;
                Console.Clear();
                Initialize();
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, DepthStencilState.None, null);

            if (GM.levelIndex == GM.levelNames.Length - 1)
            {
                _spriteBatch.DrawString(font, "GAME OVER", new Vector2(tileSize * 12, tileSize * 3), Color.White);
                _spriteBatch.DrawString(font, "Points: " + GM.points.ToString(), new Vector2(tileSize * 12, tileSize), Color.White);
            }
            else 
            {
                _spriteBatch.DrawString(font, "Lives: " + (GM.player.lives+1).ToString(), new Vector2(tileSize * 5, tileSize), Color.White);
                _spriteBatch.DrawString(font, "Bombs: " + GM.player.numBombs.ToString(), new Vector2(tileSize * 15, tileSize), Color.White);
                _spriteBatch.DrawString(font, "Points: " + GM.points.ToString(), new Vector2(tileSize * 25, tileSize), Color.White);
            }
            position = new Rectangle(0, 0, tileSize, tileSize);

            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int b = 0; b < level.GetLength(1); b++)
                {
                    position.X = i * tileSize;
                    position.Y = b * tileSize;

                    switch (level[i, b])
                    {
                        case 't':
                            for (int c = 0; c < GM.bricks.Count; c++)
                            {
                                if (GM.bricks[c].pos == new Vector2(position.X, position.Y))
                                {
                                    _spriteBatch.Draw(GM.bricks[c].texture, new Rectangle((int)GM.bricks[c].pos.Y, (int)GM.bricks[c].pos.X, tileSize, tileSize), null, Color.DarkRed, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                                }
                            }
                            break;
                        case '#':
                            for (int c = 0; c < GM.walls.Count; c++)
                            {
                                if (GM.walls[c].pos == new Vector2(position.X, position.Y))
                                {
                                    _spriteBatch.Draw(GM.walls[c].texture, new Rectangle((int)GM.walls[c].pos.Y, (int)GM.walls[c].pos.X, tileSize, tileSize), null, Color.LightGray, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                                }
                            }
                            break;
                        case 's':
                            for (int c = 0; c < GM.sands.Count; c++)
                            {
                                if (GM.sands[c].pos == new Vector2(position.X, position.Y))
                                {
                                    _spriteBatch.Draw(GM.sands[c].texture, new Rectangle((int)GM.sands[c].pos.Y, (int)GM.sands[c].pos.X, tileSize, tileSize), null, Color.SandyBrown, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                                }
                            }
                            break;
                        case 'p':
                            if (GM.player.faceRight == false)
                            {
                                GM.player.hitbox = new Rectangle((int)GM.player._position.Y, (int)GM.player._position.X, tileSize / 8, tileSize / 8);
                                _spriteBatch.Draw(GM.player.texture, new Rectangle((int)GM.player._position.Y, (int)GM.player._position.X, tileSize, tileSize), null, Color.Violet, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 1f);
                            }
                            else
                            {
                                GM.player.hitbox = new Rectangle((int)GM.player._position.Y, (int)GM.player._position.X, tileSize / 8, tileSize / 8);
                                _spriteBatch.Draw(GM.player.texture, new Rectangle((int)GM.player._position.Y, (int)GM.player._position.X, tileSize, tileSize), null, Color.Violet, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
                            }
                            break;
                        case 'r':
                            for (int c = 0; c < GM.rocks.Count; c++)
                            {
                                _spriteBatch.Draw(GM.rocks[c].texture, new Rectangle((int)GM.rocks[c].pos.Y, (int)GM.rocks[c].pos.X, tileSize, tileSize), null, Color.Brown, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
                            }
                            break;
                        case 'b':
                            for (int c = 0; c < GM.bombs.Count; c++)
                            {
                                if (GM.bombs[c].collectable || GM.bombs[c].drawScale == 1)
                                {
                                    _spriteBatch.Draw(GM.bombs[c].texture,
                                        new Rectangle((int)GM.bombs[c].pos.Y, (int)GM.bombs[c].pos.X, tileSize,
                                            tileSize), null, Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                                }
                                else
                                {
                                    _spriteBatch.Draw(GM.bombs[c].texture, new Rectangle((int)GM.bombs[c].pos.Y - tileSize / GM.bombs[c].drawScale, (int)GM.bombs[c].pos.X - tileSize / GM.bombs[c].drawScale, tileSize * GM.bombs[c].drawScale,
                                        tileSize * GM.bombs[c].drawScale), null, Color.Orange, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                                }
                            }
                            break;
                        case 'd':
                            for (int c = 0; c < GM.diamonds.Count; c++)
                            {
                                if (GM.diamonds[c].pos == new Vector2(position.X, position.Y))
                                {
                                    _spriteBatch.Draw(GM.diamonds[c].texture, new Rectangle((int)GM.diamonds[c].pos.Y, (int)GM.diamonds[c].pos.X, tileSize, tileSize), null, Color.Green, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                                }
                            }
                            break;
                        case 'g':
                            for (int c = 0; c < GM.ghosts.Count; c++)
                            {
                                GM.ghosts[c].hitbox = new Rectangle((int)GM.ghosts[c].pos.Y, (int)GM.ghosts[c].pos.X, tileSize, tileSize);
                                _spriteBatch.Draw(GM.ghosts[c].texture, new Rectangle((int)GM.ghosts[c].pos.Y, (int)GM.ghosts[c].pos.X, tileSize, tileSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                            }
                            break;
                    }
                }
            }

            foreach (Portal portal in GM.portals)
            {
                if (portal.index % 2 == 0)
                    _spriteBatch.Draw(portal.texture, new Rectangle((int)portal.pos.Y, (int)portal.pos.X, tileSize, tileSize), null, Color.Blue, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                else
                    _spriteBatch.Draw(portal.texture, new Rectangle((int)portal.pos.Y, (int)portal.pos.X, tileSize, tileSize), null, Color.Orange, 0f, new Vector2(0, 0), SpriteEffects.None, 0);

            }
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public bool FreeTile(int x, int y)
        {
            return true;
        }


    }
}
