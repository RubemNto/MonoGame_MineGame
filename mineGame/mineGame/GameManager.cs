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

        private int points = 0;

        public Player player;
        //Song backgroundMusic;


        public GameManager(Game1 g,int pointsValue) 
        {
            game = g;
            points = pointsValue;
        }

        public void addPoints(int value)
        {
            points += value;
            Console.WriteLine("we have {0} points",points);
        }

        public void UpdateGame(GameTime gameTime,ref bool Input)
        {
            player.update(gameTime,ref Input);
            foreach (Rock rock in rocks)
            {
                rock.update(gameTime);
            }
        }
        //public List<wall> walls = new List<wall>();


        public void SaveObjects(char[,] level)
        {
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
                    else if (level[l, c] == 'v')
                    {
                    }
                    else if (level[l, c] == 'h')
                    {
                    }
                    else if (level[l, c] == 'x')
                    {
                        portals.Add(new Portal(game, new Vector2(l * game.tileSize, c * game.tileSize)));
                    }
                    else if (level[l, c] == ' ')
                    {
                    }
                }
            }
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