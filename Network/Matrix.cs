using System;

namespace Network
{
    public class Matrix
    {
        private double[][] _v; // значения матрицы
        public int N { get; set; }
        public int M { get; set; } // количество строк и столбцов
        // создание матрицы заданного размера и заполнение случайными числами из интервала (-0.5, 0.5)
        public Matrix(int n, int m, Random random)
        {
            N = n;
            M = m;
            _v = new double[n][];
            for (int i = 0; i < n; i++)
            {
                _v[i] = new double[m];

                for (int j = 0; j < m; j++)
                    _v[i][j] = random.NextDouble() - 0.5; // заполняем случайными числами
            }
        }
        // обращение по индексу
        public double this[int i, int j]
        {
            get { return _v[i][j]; } // получение значения
            set { _v[i][j] = value; } // изменение значения
        }
    }
}
