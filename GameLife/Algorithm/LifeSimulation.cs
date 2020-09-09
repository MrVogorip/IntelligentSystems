using GameLife.Service;
using System;

namespace GameLife.Algorithm
{
    public class LifeSimulation
    {
        private readonly LifeService _lifeService;
        public int AmountGeneration { get; set; }
        public int NumberCells { get; }
        public Cell[,] Cells { get; set; }
        public LifeSimulation(int numberCells)
        {
            NumberCells = numberCells;
            Cells = new Cell[NumberCells, NumberCells];
            _lifeService = new LifeService(Cells, NumberCells, AmountGeneration);
            _lifeService.Generate();
        }
        public void NextGeneration()
        {
            for (int i = 0; i < NumberCells; i++)
            {
                for (int j = 0; j < NumberCells; j++)
                {
                    if (!Cells[i, j].Actual)
                    {
                        Cells[i, j].New = _lifeService.GetBorn(i, j);
                    }
                    if (Cells[i, j].Actual)
                    {
                        Cells[i, j].New = _lifeService.GetSurvivor(i, j);
                    }
                }
            }
            for (int i = 0; i < NumberCells; i++)
            {
                for (int j = 0; j < NumberCells; j++)
                {
                    Cells[i, j].Actual = Cells[i, j].New;
                    Cells[i, j].New = false;
                }
            }
            AmountGeneration++;
        }
        public void SetCell(int i, int j)
        {
            Cells[i, j] = new Cell
            {
                Actual = true,
                New = true,
            };
        }
        public int LivingСells
        {
            get
            {
                int count = 0;
                for (int i = 0; i < NumberCells; i++)
                {
                    for (int j = 0; j < NumberCells; j++)
                    {
                        if (Cells[i, j].Actual)
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }
    }
}
