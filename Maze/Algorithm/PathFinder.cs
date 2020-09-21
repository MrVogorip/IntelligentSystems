using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maze.Algorithm
{
    public class PathFinder
    {
        public static Point CurrentPosition;
        public static int[,] LabyrinthMap;
        public static bool[,] Way;
        public static Stack<Point> DistanceTraveled = new Stack<Point>();
        public static Button[,] Buttons;
        public void FindPath(int i, int j)
        {
            Way[(int)CurrentPosition.X, (int)CurrentPosition.Y] = true;
            Random rand = new Random();
            int countPath = 0;
            Point[] Crossroads = new Point[4];
            Crossroads[0] = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
            Crossroads[1] = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
            Crossroads[2] = new Point(CurrentPosition.X, CurrentPosition.Y - 1);
            Crossroads[3] = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
            for (int k = 0; k < Crossroads.Length; ++k)
            {
                if (Crossroads[k].X > 0 && Crossroads[k].X < LabyrinthMap.GetLength(0) &&
                    Crossroads[k].Y > 0 && Crossroads[k].Y < LabyrinthMap.GetLength(1))
                {
                    if (LabyrinthMap[(int)Crossroads[k].X, (int)Crossroads[k].Y] != MazeBuilder.Wall &&
                        Way[(int)Crossroads[k].X, (int)Crossroads[k].Y] != true)
                    {
                        Crossroads[countPath] = Crossroads[k];
                        countPath++;
                    }
                }
            }
            if (countPath > 0)
            {
                DistanceTraveled.Push(CurrentPosition);
                Buttons[(int)CurrentPosition.X, (int)CurrentPosition.Y].Background = Brushes.Green;
                int number = rand.Next(0, countPath);
                CurrentPosition = Crossroads[number];
            }
            else
            {
                if (DistanceTraveled.Count != 0)
                {
                    CurrentPosition = DistanceTraveled.Pop();
                }
                Buttons[(int)CurrentPosition.X, (int)CurrentPosition.Y].Background = Brushes.White;
                if (Way[i, j] == true)
                {
                    Buttons[(int)CurrentPosition.X, (int)CurrentPosition.Y].Background = Brushes.Green;
                }
            }
        }
        public bool IsFind(int i, int j)
        {
            return Way[i, j] == true;
        }
    }
}
