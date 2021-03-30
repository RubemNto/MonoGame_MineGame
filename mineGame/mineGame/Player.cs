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
        private bool dead = false;
        bool moved = false;
        private char[] _direction = {
            'L','U','D', 'R'
        };

        private char _dir;

        public Texture2D texture;
        public Vector2 _position;
        private Vector2 initialPos;
        public Vector2 _movementDestination;
        public float speed;
        public float deadTimer = 2;

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
            initialPos = position;
            _movementDestination = _position;
        }


        public void update(Game1 game, GameTime gameTime,ref bool pressingDown) 
        {
            if(dead == false) { 
                checkOrientation(ref pressingDown);
                moveTo(ref _position,_movementDestination, speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else
            {
                resetPos(game, gameTime, ref deadTimer); 
                
            }
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
                {
                    checkRocks();
                    checkBomb();
                }
                

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
            for (int i = 0; i < game.GM.bombs.Count; i++)
            {
                if (game.GM.bombs[i].pos == _movementDestination)
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

        public bool NextTo(Vector2 Destination)
        {
            Vector2[] positions = {
                new Vector2(_movementDestination.X,_movementDestination.Y+game.tileSize),
                new Vector2(_movementDestination.X,_movementDestination.Y-game.tileSize),
                };
            //Console.WriteLine(positions[0]);
            //Console.WriteLine(positions[1]);
            //Console.WriteLine(Destination);

            //Thread.Sleep(2000);
            //Console.Clear();

            foreach (Rock rock in game.GM.rocks)
            {
                if (rock.pos == Destination)
                {
                    if ((Destination - new Vector2(Destination.X, Destination.Y + game.tileSize) == positions[0]) || (Destination + new Vector2(Destination.X, Destination.Y + game.tileSize) == positions[1]))
                    {
                        return true;
                    }
                }
            }
            foreach (Sand sand in game.GM.sands)
            {
                if (sand.pos == Destination)
                {
                    if ((Destination - new Vector2(Destination.X, Destination.Y + game.tileSize) == positions[0]) || (Destination + new Vector2(Destination.X, Destination.Y + game.tileSize) == positions[1]))
                    {
                        return true;
                    }
                }
            }
            foreach (wall Wall in game.GM.walls)
            {
                if (Wall.pos == Destination)
                {
                    if ((Destination - new Vector2(Destination.X, Destination.Y + game.tileSize) == positions[0]) || (Destination + new Vector2(Destination.X, Destination.Y + game.tileSize) == positions[1]))
                    {
                        return true;
                    }
                }
            }
            foreach (Bomb bomb in game.GM.bombs)
            {
                if (bomb.pos == Destination)
                {
                    if ((Destination - new Vector2(Destination.X, Destination.Y + game.tileSize) == positions[0]) || (Destination + new Vector2(Destination.X, Destination.Y + game.tileSize) == positions[1]))
                    {
                        return true;
                    }
                }
            }
            foreach (Diamond diamond in game.GM.diamonds)
            {
                if (diamond.pos == Destination)
                {
                    if ((Destination - new Vector2(Destination.X, Destination.Y + game.tileSize) == positions[0]) || (Destination + new Vector2(Destination.X, Destination.Y + game.tileSize) == positions[1]))
                    {
                        return true;
                    }
                }
            }
            return false;
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

        public void checkBomb()
        {
            //get all bombs in list
            for (int i = 0; i < game.GM.bombs.Count; i++)
            {
                if (game.GM.bombs[i].pos == _position + new Vector2(0, game.tileSize) && _dir == 'R') //check rocks at the right
                {

                    game.GM.bombs[i].updatePosition(game.GM.bombs[i].pos + new Vector2(0, 64), game);
                    //_movementDestination = new Vector2(_position.X, _position.Y + 32);

                }
                else if (game.GM.bombs[i].pos == _position - new Vector2(0, game.tileSize) && _dir == 'L') //check rocks at the left
                {

                    game.GM.bombs[i].updatePosition(game.GM.bombs[i].pos - new Vector2(0, 64), game);
                    //_movementDestination = new Vector2(_position.X, _position.Y - 32);
                }
                else if (game.GM.bombs[i].pos == _position + new Vector2(game.tileSize, 0) && _dir == 'D')
                {
                    game.GM.bombs[i].updatePosition(game.GM.bombs[i].pos + new Vector2(32, 0), game);
                }
                else if (game.GM.bombs[i].pos == _position - new Vector2(game.tileSize, 0) && _dir == 'U')
                {
                    game.GM.bombs[i].updatePosition(game.GM.bombs[i].pos - new Vector2(32, 0), game);
                }
            }
        }

        public void deadPlayer(Game1 g)
        {
            Texture2D tempTexture = g.Content.Load<Texture2D>("playerDead");

            // código para -1 vida, verificar se vidas são maiores ou = a zero

            //Apresentar o "Game Over" caso vidas = 0

            //Function to kill the player



            dead = true;
            texture = tempTexture;            
           

        }
        
        public void resetPos(Game g, GameTime gameTime, ref float _deadTimer)
        {
            float time = 2;

            if (_deadTimer <= 0)
            {
                texture = g.Content.Load<Texture2D>("player");
                _movementDestination = initialPos;
                _position = initialPos;
                _deadTimer = time;
                dead = false;
            }
            else
            {
                _deadTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            


        }

    }
}
