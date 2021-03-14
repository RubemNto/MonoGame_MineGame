using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
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


        public GameManager(Game1 g) 
        {
            game = g;
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
                    }
                    else if (level[l, c] == 's')
                    {
                        sands.Add(new Sand(game, new Vector2(l * game.tileSize, c * game.tileSize)));
                    }
                    else if (level[l, c] == 'd')
                    {
                    }
                    else if (level[l, c] == 'b')
                    {
                    }
                    else if (level[l, c] == 'r')
                    {
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
                int columns = levelText[0].Length;
                int lines = levelText.Length;
                level = new char[lines, columns];
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
                for (int l = 0; l < lines; l++)
                {
                    for (int c = 0; c < columns; c++)
                    {
                        level[l, c] = levelText[c][l];
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