using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mineGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private Sand _sand;
        private Diamond _diamond;
        private Bomb _bomb;
        private Rock _rock;
        private Brick _brick;

        public char[,] level;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public bool FreeTile(int x, int y)
        {
            
        }
        
        void LoadLevel(string levelFile)
        {
            string[] linhas = File.ReadAllLines();
            int nrLinhas = linhas.Length;
            int nrColunas = linhas[0].Length;

            level = new char[nrColunas, nrLinhas];

            for (int x = 0; x < nrColunas; x++)
            {
                for (int y = 0; y < nrLinhas; y++)
                {
                    switch (linhas[y][x])
                    {
                        case 'P':
                            _player = new Player(this,x,y);
                            level[x, y] = ' ';
                            break;
                        case 'S':
                            _sand = new Sand(this,x,y);
                            level[x, y] = ' ';
                            break;
                        case 'D' :
                            _diamond = new Diamond(this,x,y);
                            level[x, y] = ' ';
                            break;
                        case 'B' :
                            _bomb = new Bomb(this,x,y);
                            level[x, y] = ' ';
                            break;
                        case 'R' :
                            _rock = new Rock(this,x,y);
                            level[x, y] = ' ';
                            break;
                        case 'K' :
                            _brick = new Brick(this,x,y);
                            level[x, y] = ' ';
                            break;
                        default:
                            level[x, y] = linhas[y][x];
                            break;
                    }
                }
            }
            
        }
    }
}
