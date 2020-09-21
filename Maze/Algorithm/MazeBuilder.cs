using System;
using System.Collections.Generic;
using System.Windows;

namespace Maze.Algorithm
{
    public class MazeBuilder
    {
        public const int Traveled = 3;
        public const int Wall = 2;
        public const int FreeWay = 1;
        public int[,] InitializeLabyrinth(int value, ref int countСrossroads)
        {
            int[,] LabyrinthMap = new int[value, value];
            for (int j = 0; j < value; ++j)
            {
                for (int i = 0; i < value; ++i)
                {
                    if ((i % 2 != 0 && j % 2 != 0) && (j < value - 1 && i < value - 1))
                    {
                        LabyrinthMap[j, i] = FreeWay;
                        countСrossroads++;
                    }
                    else
                    {
                        LabyrinthMap[j, i] = Wall;
                    }
                }
            }
            return LabyrinthMap;
        }
        public void BuildLabyrinth(int mazeSize , ref int[,] LabyrinthMap)
        {
            Stack<Point> distanceTraveled = new Stack<Point>();
            Random rand = new Random();
            Point currentPosition = new Point(1, 1);
            bool[] way = new bool[mazeSize];
            int countWay = 0;
            bool isExist;
            do
            {
                isExist = false;
                if (LabyrinthMap[(int)currentPosition.X, (int)currentPosition.Y] == FreeWay)
                {
                    LabyrinthMap[(int)currentPosition.X, (int)currentPosition.Y] = Traveled;
                    way[countWay] = true;
                    countWay++;
                }
                int countPath = 0;
                Point[] crossroads = new Point[4];
                crossroads[0] = new Point(currentPosition.X - 2, currentPosition.Y);
                crossroads[1] = new Point(currentPosition.X + 2, currentPosition.Y);
                crossroads[2] = new Point(currentPosition.X, currentPosition.Y - 2);
                crossroads[3] = new Point(currentPosition.X, currentPosition.Y + 2);
                for (int i = 0; i < crossroads.Length; ++i)
                {
                    if (crossroads[i].X > 0 && crossroads[i].X < LabyrinthMap.GetLength(0) &&
                        crossroads[i].Y > 0 && crossroads[i].Y < LabyrinthMap.GetLength(1))
                    {
                        if (LabyrinthMap[(int)crossroads[i].X, (int)crossroads[i].Y] != Wall &&
                            LabyrinthMap[(int)crossroads[i].X, (int)crossroads[i].Y] != Traveled)
                        {
                            crossroads[countPath] = crossroads[i];
                            countPath++;
                        }
                    }
                }
                if (countPath != 0)
                {
                    distanceTraveled.Push(currentPosition);
                    int number = rand.Next(0, countPath);
                    int x = (int)((Math.Abs(crossroads[number].X - currentPosition.X) == 0) ? currentPosition.X : Math.Abs(crossroads[number].X - currentPosition.X));
                    int y = (int)((Math.Abs(crossroads[number].Y - currentPosition.Y) == 0) ? currentPosition.Y : Math.Abs(crossroads[number].Y - currentPosition.Y));
                    if (Math.Abs(crossroads[number].Y - currentPosition.Y) == 0)
                    {
                        x = (int)((crossroads[number].X - currentPosition.X > 0) ? crossroads[number].X : currentPosition.X) - 1;
                    }
                    else
                    {
                        y = (int)((crossroads[number].Y - currentPosition.Y > 0) ? crossroads[number].Y : currentPosition.Y) - 1;
                    }
                    LabyrinthMap[x, y] = Traveled;
                    currentPosition = crossroads[number];
                }
                else
                {
                    if (distanceTraveled.Count != 0)
                    {
                        currentPosition = distanceTraveled.Pop();
                    }
                }
                for (int i = 0; i < way.Length; i++)
                {
                    if (way[i] == false)
                        isExist = true;
                }
            } while (isExist);
        }
    }
}
