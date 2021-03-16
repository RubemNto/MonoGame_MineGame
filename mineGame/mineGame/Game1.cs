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
        //private Texture2D[] Textures;
        Rectangle position;
        //private Player _player;

        //private Sand _sand;
        //private Diamond _diamond;
        //private Bomb _bomb;
        //private Rock _rock;
        //private Brick _brick;
        public GameManager GM;

        public char[,] level;
        public int tileSize = 50;
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
            GM = new GameManager(this);
            GM.loadLevel("level1.txt", out level);

            int windowHeight = level.GetLength(1) * tileSize;
            int windowWidth = tileSize * level.GetLength(0);
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

            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.Volume = 1f;
            //MediaPlayer.
            //MediaPlayer.
            //  Uncomment the following line will also loop the song
            MediaPlayer.IsRepeating = true;
            //MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            IsMouseVisible = false;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GM.player != null)
            {
                GM.player.update(gameTime, ref pressingDown);
            }
            //if (Keyboard.GetState().IsKeyDown(Keys.Space) && pressingDown == false && GM.sands.Count != 0)
            //{
            //    GM.sands.RemoveAt(0);
            //    pressingDown = true;
            //}
            //else if (Keyboard.GetState().IsKeyUp(Keys.Space) && pressingDown == true)
            //{
            //    pressingDown = false;
            //}

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Deferred,null, SamplerState.PointClamp, DepthStencilState.None,null);
            position = new Rectangle(0, 0, tileSize, tileSize);

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
                                    //Console.WriteLine("Found Wall");
                                    //if (position.X % 3 == 0 && position.Y % 3 == 0)
                                    //    _spriteBatch.Draw(GM.walls[c].texture, GM.walls[c].pos, Color.Green);
                                    //else
                                    _spriteBatch.Draw(GM.walls[c].texture, new Rectangle((int)GM.walls[c].pos.X, (int)GM.walls[c].pos.Y, tileSize, tileSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);

                                    //_spriteBatch.Draw(GM.walls[c].texture, GM.walls[c].pos, Color.White);
                                    //break;
                                }
                            }
                            break;
                        case 's':
                            for (int c = 0; c < GM.sands.Count; c++)
                            {
                                if (GM.sands[c].pos == new Vector2(position.X, position.Y))
                                {
                                    //Console.WriteLine("Found Wall");
                                    _spriteBatch.Draw(GM.sands[c].texture, new Rectangle((int)GM.sands[c].pos.X, (int)GM.sands[c].pos.Y, tileSize, tileSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);

                                    //_spriteBatch.Draw(GM.sands[c].texture, GM.sands[c].pos, Color.White);
                                    break;
                                }
                            }
                            break;
                        case 'x':
                            for (int c = 0; c < GM.portals.Count; c++)
                            {
                                if (GM.portals[c].pos == new Vector2(position.X, position.Y))
                                {
                                    //Console.WriteLine("Found Wall");
                                    //_spriteBatch.Draw(GM.player.texture, new Rectangle((int)GM.player._position.X, (int)GM.player._position.Y, 32, 32), null, Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 1);
                                    _spriteBatch.Draw(GM.portals[c].texture,new Rectangle((int)GM.portals[c].pos.X, (int)GM.portals[c].pos.Y,tileSize,tileSize),null, Color.White,0f,new Vector2(0,0),SpriteEffects.None,0);
                                    break;
                                }
                            }
                            break;
                        case 'p':
                            if (GM.player.faceRight == false)
                                _spriteBatch.Draw(GM.player.texture, new Rectangle((int)GM.player._position.X, (int)GM.player._position.Y, tileSize, tileSize), null, Color.Red, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 1);
                            else
                                _spriteBatch.Draw(GM.player.texture, new Rectangle((int)GM.player._position.X, (int)GM.player._position.Y, tileSize, tileSize), null, Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 1);
                            
                            break;
                        case 'r':
                            for (int c = 0; c < GM.bricks.Count; c++)
                            {
                                if (GM.bricks[c].pos == new Vector2(position.X, position.Y))
                                {
                                    //Console.WriteLine("Found Wall");
                                    _spriteBatch.Draw(GM.bricks[c].texture, new Rectangle((int)GM.bricks[c].pos.X, (int)GM.bricks[c].pos.Y, tileSize, tileSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);

                                    //_spriteBatch.Draw(GM.bricks[c].texture, GM.bricks[c].pos, Color.White);
                                    break;
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
