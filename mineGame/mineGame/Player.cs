using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Threading;

namespace mineGame
{
    public class Player
    {
        //private Point _position;
        private Game1 game;
        private bool _keysReleased = true;
        bool moved = false;
        private char[] _direction = {
            'L','U','D', 'R'
        };

        private char _dir;

        public Texture2D texture;
        public Vector2 _position;
        private Vector2 _movementDestination;
        public float speed;
        public bool faceRight = true;
        SoundEffect moveSound;
        SoundEffect sandDestructionSound;
        SoundEffect collectionSound;

        public Player(Game1 g, Vector2 position)
        {
            game = g;
            speed = game.tileSize * 8;
            moveSound = g.Content.Load<SoundEffect>("moveSoundEffect");
            sandDestructionSound = g.Content.Load<SoundEffect>("sandSoundEffect");
            collectionSound = g.Content.Load<SoundEffect>("collection");

            texture = g.Content.Load<Texture2D>("player");
            _position = position;
            _movementDestination = _position;
        }


        public void update(GameTime gameTime,ref bool pressingDown) 
        {
            checkOrientation(ref pressingDown);
            moveTo(ref _position,_movementDestination, speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        void moveTo(ref Vector2 originalPos,Vector2 destination,float Speed) 
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

        void checkOrientation(ref bool pressingKeyDown)
        {
            //_movementDestination = _position;
            KeyboardState keyboardState = Keyboard.GetState();
            if (!pressingKeyDown)
            {
                Vector2 pastPosition = _position;
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    //Console.WriteLine("Go Up");
                    _dir = _direction[1];
                    _movementDestination.X -= game.tileSize;
                    pressingKeyDown = true;
                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    //Console.WriteLine("Go Down");
                    _dir = _direction[2];
                    _movementDestination.X += game.tileSize;
                    pressingKeyDown = true;
                }
                else if (keyboardState.IsKeyDown(Keys.Left))
                {
                    //Console.WriteLine("Go Left");
                    _dir = _direction[0];
                    faceRight = false;
                    _movementDestination.Y -= game.tileSize;
                    pressingKeyDown = true;
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    //Console.WriteLine("Go Right");
                    _dir = _direction[3];
                    faceRight = true;
                    _movementDestination.Y += game.tileSize;
                    pressingKeyDown = true;
                }


                if (pressingKeyDown)
                    checkRocks();

                ////check collision with walls
                if (freeSpace() == false && pressingKeyDown == true)
                {
                    _movementDestination = pastPosition;
                    moved = false;
                }               

                //play sound when move
                if (_movementDestination != pastPosition && pressingKeyDown == true) 
                {
                    moveSound.Play(0.5f, 1f, 0f);
                }

                //destroy sand if destination is a sand position
                for (int i = 0; i < game.GM.sands.Count; i++)
                {
                    if (game.GM.sands[i].pos == _movementDestination)
                    {
                        game.GM.sands.RemoveAt(i);
                        sandDestructionSound.Play(0.5f, 1f, 0f);
                    }
                }

                //collect diamond if destination is diamond position
                for (int i = 0; i < game.GM.diamonds.Count; i++)
                {
                    if (game.GM.diamonds[i].pos == _movementDestination)
                    {
                        game.GM.addPoints(5);
                        game.GM.diamonds.RemoveAt(i);
                        collectionSound.Play(0.5f, 1f, 0f);
                    }
                }
            }
            else
            {
                if (keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyUp(Keys.Down) && keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Right))
                {
                    pressingKeyDown = false;
                }
            }
        }

        public bool freeSpace()
        {
            //check collision with walls
            for (int i = 0; i < game.GM.walls.Count; i++)
            {
                if (game.GM.walls[i].pos == _movementDestination)
                {
                    return false;
                }
            }
            for (int i = 0; i < game.GM.rocks.Count; i++)
            {
                    if (game.GM.rocks[i].pos == _movementDestination)
                    {
                        return false;
                    } 
            }
            if (_movementDestination.X > game.windowHeight - 1 || _movementDestination.X < 0)
            {
                return false;
            }
            if (_movementDestination.Y > game.windowWidth - 1 || _movementDestination.Y < 0)
            {
                return false;
            }


            return true;
        }

        public void checkRocks()
        {            
            //get all rocks in list
            for (int i = 0; i < game.GM.rocks.Count; i++)
            {
                if (game.GM.rocks[i].pos == _position + new Vector2(0, game.tileSize) && _dir == 'R') //check rocks at the right
                {                    
                    game.GM.rocks[i].updatePosition(game.GM.rocks[i].pos + new Vector2(0, game.tileSize),game);
                    //_movementDestination = new Vector2(_position.X, _position.Y + 32);
                    
                }
                else if (game.GM.rocks[i].pos == _position - new Vector2(0, game.tileSize) && _dir == 'L') //check rocks at the left
                {                    
                    game.GM.rocks[i].updatePosition(game.GM.rocks[i].pos - new Vector2(0, game.tileSize),game);
                    //_movementDestination = new Vector2(_position.X, _position.Y - 32);
                }
            }
        }

    }
}
