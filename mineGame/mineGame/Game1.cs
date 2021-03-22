using System.IO;
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
        Rectangle position;

        public GameManager GM;

        public char[,] level;
        public int tileSize = 32;
        public bool pressingDown = false;

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
            GM = new GameManager(this,0);
            GM.loadLevel("level2.txt", out level);

            int windowHeight = level.GetLength(0) * tileSize;
            int windowWidth = tileSize * level.GetLength(1);
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            //0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = 0f;
            MediaPlayer.IsRepeating = true;

            MediaPlayer.Play(backgroundMusic);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            this.backgroundMusic = Content.Load<Song>("Lazy Afternoon - Pushmo World Soundtrack");

            MediaPlayer.Volume = 1f;
            //MediaPlayer.
            //MediaPlayer.
            //  Uncomment the following line will also loop the song
            MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(backgroundMusic);
            //MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            IsMouseVisible = false;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Initialize();
            }

            GM.UpdateGame(gameTime, ref pressingDown);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Deferred,null, SamplerState.PointClamp, DepthStencilState.None,null);
            position = new Rectangle(0, 0, tileSize, tileSize);
            //_spriteBatch.Draw(GM.walls[0].texture, new Rectangle(0,64, tileSize, tileSize), null, Color.Green, 0f, new Vector2(0, 0), SpriteEffects.None, 10f);

            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int b = 0; b < level.GetLength(1); b++)
                {
                    position.X = i * tileSize;
                    position.Y = b * tileSize;

                    switch (level[i, b])
                    {
                        case '#':
                            for (int c = 0; c < GM.walls.Count; c++)
                            {
                                if (GM.walls[c].pos == new Vector2(position.X,position.Y))
                                {
                                    _spriteBatch.Draw(GM.walls[c].texture, new Rectangle((int)GM.walls[c].pos.Y, (int)GM.walls[c].pos.X, tileSize, tileSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                                }
                            }
                            break;
                        case 's':
                            for (int c = 0; c < GM.sands.Count; c++)
                            {
                                if (GM.sands[c].pos == new Vector2(position.X, position.Y))
                                {
                                    _spriteBatch.Draw(GM.sands[c].texture, new Rectangle((int)GM.sands[c].pos.Y, (int)GM.sands[c].pos.X, tileSize, tileSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                                }
                            }
                            break;
                        case 'x':
                            for (int c = 0; c < GM.portals.Count; c++)
                            {
                                if (GM.portals[c].pos == new Vector2(position.X, position.Y))
                                {
                                    _spriteBatch.Draw(GM.portals[c].texture,new Rectangle((int)GM.portals[c].pos.Y, (int)GM.portals[c].pos.X,tileSize,tileSize),null, Color.White,0f,new Vector2(0,0),SpriteEffects.None,0);
                                }
                            }
                            break;
                        case 'p':
                            if (GM.player.faceRight == false)
                                _spriteBatch.Draw(GM.player.texture, new Rectangle((int)GM.player._position.Y, (int)GM.player._position.X, tileSize, tileSize), null, Color.Red, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 1f);
                            else
                                _spriteBatch.Draw(GM.player.texture, new Rectangle((int)GM.player._position.Y, (int)GM.player._position.X, tileSize, tileSize), null, Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None,1f);
                            break;
                        case 'r':
                            for (int c = 0; c < GM.rocks.Count; c++)
                            {
                                //if (GM.rocks[c].pos == new Vector2(position.X, position.Y))
                                //{
                                    //Console.WriteLine("{0} -> {1}", GM.rocks[c].pos, position);
                                    //Console.WriteLine("DrawingRock");
                                    _spriteBatch.Draw(GM.rocks[c].texture, new Rectangle((int)GM.rocks[c].pos.Y, (int)GM.rocks[c].pos.X, tileSize, tileSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
                                //}
                            }
                            break;
                        case 'b':
                            for (int c = 0; c < GM.bombs.Count; c++)
                            {
                                if (GM.bombs[c].pos == new Vector2(position.X, position.Y))
                                {
                                    _spriteBatch.Draw(GM.bombs[c].texture, new Rectangle((int)GM.bombs[c].pos.Y, (int)GM.bombs[c].pos.X, tileSize, tileSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                                }
                            }
                            break;
                        case 'd':
                            for (int c = 0; c < GM.diamonds.Count; c++)
                            {
                                if (GM.diamonds[c].pos == new Vector2(position.X, position.Y))
                                { 
                                    _spriteBatch.Draw(GM.diamonds[c].texture, new Rectangle((int)GM.diamonds[c].pos.Y, (int)GM.diamonds[c].pos.X, tileSize, tileSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                                }
                            }
                            break;
                    }
                }
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
