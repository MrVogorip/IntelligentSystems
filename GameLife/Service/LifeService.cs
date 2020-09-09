using GameLife.Algorithm;
using System;

namespace GameLife.Service
{
    public class LifeService
    {
        private Cell[,] _cells;
        private int _numberCells;
        private int _amountGeneration;
        private Random _random;
        public LifeService(Cell[,] cells, int numberCells, int amountGeneration)
        {
            _cells = cells;
            _numberCells = numberCells;
            _amountGeneration = amountGeneration;
            _random = new Random();
        }
        public void Generate()
        {
            _amountGeneration++;
            int count = 0;
            for (int i = 0; i < _numberCells; i++)
            {
                if (count >= _numberCells * _numberCells)
                {
                    break;
                }
                for (int j = 0; j < _numberCells; j++)
                {
                    _cells[i, j] = new Cell
                    {
                        Actual = _random.Next(100) < 50,
                        New = false,
                    };
                    if (_cells[i, j].Actual && count >= _numberCells * _numberCells)
                    {
                        break;
                    }
                }
            }
        }
        public bool GetBorn(int i, int j)
        {
            int count = 0;
            for (int index1 = i - 1; index1 < i - 1 + 3; index1++)
            {
                if (index1 >= _numberCells || index1 < 0)
                {
                    continue;
                }
                for (int index2 = j - 1; index2 < j - 1 + 3; index2++)
                {
                    if (index2 < 0 || index2 >= _numberCells || (index1 == i && index2 == j))
                    {
                        continue;
                    }
                    if (_cells[index1, index2].Actual)
                    {
                        count++;
                    }
                }
            }
            return count == 3;
        }
        public bool GetSurvivor(int i, int j)
        {
            int count = 0;
            for (int index1 = i - 1; index1 < i - 1 + 3; index1++)
            {
                if (index1 >= _numberCells || index1 < 0)
                {
                    continue;
                }
                for (int index2 = j - 1; index2 < j - 1 + 3; index2++)
                {
                    if (index2 < 0 || index2 >= _numberCells || (index1 == i && index2 == j))
                    {
                        continue;
                    }
                    if (_cells[index1, index2].Actual)
                    {
                        count++;
                    }
                }
            }
            return count == 3 || count == 2;
        }
    }
}
