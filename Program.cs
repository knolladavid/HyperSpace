using System;
using System.Collections.Generic;
using System.Linq;

namespace HyperSpace
{
    internal class Program
    {
        private static void Main(string[] args)
        {
        }
    }

    public class Unit
    {
        public int X { get; set; }

        public int Y { get; set; }

        private ConsoleColor Color { get; set; }

        public string Symbol { get; set; }

        public bool IsSpaceRift { get; set; }

        public static List<string> ObstacleList = new List<string>() { "*", "!", ".", ":", ";", ":", "'", "$", "#", "&" };
        public static Random rng = new Random();

        public Unit(int x, int y)
        {
            this.Color = ConsoleColor.Cyan;
            this.X = x;
            this.Y = y;
            this.Symbol = ObstacleList[rng.Next(0, ObstacleList.Count())];
        }

        public Unit(int x, int y, ConsoleColor color, string Symbol, bool isSpaceRift)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
            this.Symbol = Symbol;
            this.IsSpaceRift = isSpaceRift;
        }

        public void Draw()
        {
            Console.SetCursorPosition(this.X, this.Y);
            Console.ForegroundColor = this.Color;
            Console.Write(this.Symbol);
        }
    }

    public class HyperSpace
    {
        public int Score { get; set; }

        public int Speed { get; set; }

        public List<Unit> ObstacleList { get; set; }

        public Unit SpaceShip { get; set; }

        public bool Smashed { get; set; }

        public Random rng = new Random();

        public HyperSpace(int Score, int Speed, List<Unit> ObstacleList)
        {
            
            this.Score = 0;
            this.Speed = 0;
            this.ObstacleList = new List<Unit>();
            this.SpaceShip = new Unit((Console.WindowWidth / 2) - 1, Console.WindowHeight - 1, ConsoleColor.Red, "@", false);
            
            Console.BufferHeight = Console.WindowHeight;
            Console.WindowHeight = 30;
            Console.BufferWidth = Console.BufferWidth;
            Console.BufferWidth = 60;
        }
        public void PlayGame()
        {
            while (Smashed == false)
            {
                int spaceRiftProb = rng.Next(1, 11);
                if (spaceRiftProb == 10)
                {
                       Unit spaceRift = new Unit(rng.Next(0,Console.WindowWidth - 2), 5, ConsoleColor.Green, "%", true);
                    ObstacleList.Add(spaceRift);
                }
                else
                {
                    Unit Obstacle = new Unit(rng.Next(0,Console.WindowWidth - 2), 5);
                    ObstacleList.Add(Obstacle);
                }
                MoveShip();
                MoveObstacles();
                DrawGame();

                if (Speed < 170)
                {
                    Speed++;
                }
                System.Threading.Thread.Sleep(170 - Speed);
            }
        }
       
        
        
        public void MoveShip()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey();
                
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                //if the key pressed is left arrow and space ship X position is less than 0 decrease ships position by 1 
                if (keyPressed.Key == ConsoleKey.LeftArrow && SpaceShip.X > 0)
                {
                    SpaceShip.X--;
                }
                //if the key pressed is right arrow and the space ship x position is greater than WW, increase position by 1
                if (keyPressed.Key == ConsoleKey.RightArrow && SpaceShip.X < Console.WindowWidth - 2)
                {
                    SpaceShip.X++;
                }
            }
        }
       
        
        public void MoveObstacles()
        {
            List<Unit> newObstacleList = new List<Unit>();
            foreach (Unit stuff in ObstacleList)
              {
                stuff.Y++;

                if ((stuff.IsSpaceRift) && stuff.X == SpaceShip.X && stuff.Y == SpaceShip.Y)
                {
                    Speed -= 50;
                }
                else if (! stuff.IsSpaceRift && stuff.X == SpaceShip.X && stuff.Y == SpaceShip.Y)
                {
                    Smashed = true;
                }
                else if (stuff.Y < Console.WindowHeight)
                {
                    newObstacleList.Add(stuff);
                }
                else
                {
                    Score++;
                }
            }
            ObstacleList = newObstacleList;
        }

        }
       
        
        public void DrawGame()
        {



        }
    }
