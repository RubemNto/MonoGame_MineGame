using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mineGame
{
    public class GameManager
    {
        public Game1 game;
        public List<wall> walls = new List<wall>();
        public List<Sand> sands = new List<Sand>();
        public List<Portal> portals = new List<Portal>();
        public List<Brick> bricks = new List<Brick>();
        public List<Bomb> bombs = new List<Bomb>();
        public List<Diamond> diamonds = new List<Diamond>();
        public List<Rock> rocks = new List<Rock>();
        public List<Ghost> ghosts = new List<Ghost>();

        public string[] levelNames = {"level2.txt","level1.txt","endScreen.txt"};

        public bool changeLevel = false;

        public int levelIndex = 0;
        public int points = 0;
       
        public Player player;
        //Song backgroundMusic;


        public GameManager(Game1 g,int pointsValue) 
        {
            levelIndex = 0;
            game = g;
            points = pointsValue;
        }

        public GameManager(Game1 g, int pointsValue,int LevelIndex)
        {
            levelIndex = LevelIndex;
            game = g;
            points = pointsValue;
        }

        public void addPoints(int value)
        {
            points += value;
            Console.WriteLine("we have {0} points",points);
        }

        public void UpdateGame(Game1 game,GameTime gameTime,ref bool Input)
        {
            if (!changeLevel)
            {
                if (player != null)
                {
                    if (player.lives < 0)
                    {
                        changeLevel = true;
                    }
                    else
                    {
                        player.update(game, gameTime, ref Input);
                    }
                }
                foreach (Rock rock in rocks)
                {
                    rock.update(gameTime, game);
                }

                for (int i = 0; i < bombs.Count; i++)
                {
                    bombs[i].update(gameTime, game);
                }

                for (int i = 0; i < ghosts.Count; i++)
                {
                    ghosts[i].Update(game, gameTime);
                }
            }
        }
        //public List<wall> walls = new List<wall>();


        public void SaveObjects(char[,] level)
        {
            int portalsIndex = 0;
            for (int l = 0; l < level.GetLength(0); l++)
            {
                for (int c = 0; c < level.GetLength(1); c++)
                {
                    /*
                       # -> wall
                       p -> player
                       s -> sand
                       d -> diamond
                       b -> bomb
                       r -> rock
                       v -> vertical enemy
                       h -> horizontal enemy
                       x -> portal
                         -> nothing
                    */
                    if (level[l, c] == 't')
                    {
                        bricks.Add(new Brick(game, new Vector2(l * game.tileSize, c * game.tileSize)));
                    }
                    if (level[l, c] == '#')
                    {
                        walls.Add(new wall(game, new Vector2(l * game.tileSize, c * game.tileSize)));
                    }
                    else if (level[l, c] == 'p') 
                    {
                        player = new Player(game,new Vector2(l * game.tileSize, c * game.tileSize));
                    }
                    else if (level[l, c] == 's')
                    {
                        sands.Add(new Sand(game, new Vector2(l * game.tileSize, c * game.tileSize)));
                    }
                    else if (level[l, c] == 'd')
                    {
                        diamonds.Add(new Diamond(game, new Vector2(l * game.tileSize, c * game.tileSize)));
                    }
                    else if (level[l, c] == 'b')
                    {
                        bombs.Add(new Bomb(game, new Vector2(l * game.tileSize, c * game.tileSize)));
                    }
                    else if (level[l, c] == 'r')
                    {
                        rocks.Add(new Rock(game, new Vector2(l * game.tileSize, c * game.tileSize)));
                        //bricks.Add(new Brick(game, new Vector2(l * game.tileSize, c * game.tileSize)));
                    }
                    else if (level[l, c] == 'g')
                    {
                        ghosts.Add(new Ghost(game, new Vector2(l * game.tileSize, c * game.tileSize)));
                    }
                    else if (level[l, c] >= '1' && level[l,c] <= '9')//check which instance of portal must be added
                    {
                        Console.WriteLine(int.Parse(level[l, c].ToString()));
                        portals.Add(new Portal(game, new Vector2(l * game.tileSize, c * game.tileSize),int.Parse(level[l,c].ToString())-1));
                    }
                }
            }
            organizePortals(ref portals);
        }

        void organizePortals(ref List<Portal> list) 
        {
            Portal[] newList = new Portal[list.Count];            
            foreach(Portal portal in list)
            {
                //Console.WriteLine(portal.index);
                newList[portal.index] = portal;
            }
            list = new List<Portal>(newList);
        }

        public void printLevel(char[,] level)
        {
            if (level.GetLength(0) == 0 || level.GetLength(1) == 0) 
            {
                Console.WriteLine("No level loaded");
            }

            for (int l = 0; l < level.GetLength(0); l++)
            {
                for (int c = 0; c < level.GetLength(1); c++)
                {
                    Console.Write(level[l,c]);
                }
                Console.WriteLine();
            }
        }

        public void loadLevel(string levelAddres,out char[,] level) 
        {
            if (levelAddres != null)
            {
                string[] levelText = File.ReadAllLines(path:$"Content/{levelAddres}");
                int lines = levelText.Length;
                int columns = levelText[0].Length;
                level = new char[lines, columns];
                
                for (int l = 0; l < lines; l++)
                {
                    for (int c = 0; c < columns; c++)
                    {
                        level[l, c] = levelText[l][c];
                    }
                }
            }
            else 
            {
                level = new char[0, 0];
            }
            SaveObjects(level);
            printLevel(level);
        }
    }
}