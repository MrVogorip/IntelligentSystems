using System;

namespace Network
{
    public class NetworkAlgorithm
    {
        struct LayerT
        {
            public Vector x; // вход слоя
            public Vector z; // активированный выход слоя
            public Vector df; // производная функции активации слоя
        }
        private Matrix[] _weights; // матрицы весов слоя
        private LayerT[] _l; // значения на каждом слое
        private Vector[] _deltas; // дельты ошибки на каждом слое
        private int _layersN; // число слоёв
        public NetworkAlgorithm(int[] sizes)
        {
            Random random = new Random(DateTime.Now.Millisecond); // создаём генератор случайных чисел
            _layersN = sizes.Length - 1; // запоминаем число слоёв
            _weights = new Matrix[_layersN]; // создаём массив матриц весовых коэффициентов
            _l = new LayerT[_layersN]; // создаём массив значений на каждом слое
            _deltas = new Vector[_layersN]; // создаём массив для дельт
            for (int k = 1; k < sizes.Length; k++)
            {
                _weights[k - 1] = new Matrix(sizes[k], sizes[k - 1], random); // создаём матрицу весовых коэффициентов
                _l[k - 1].x = new Vector(sizes[k - 1]); // создаём вектор для входа слоя
                _l[k - 1].z = new Vector(sizes[k]); // создаём вектор для выхода слоя
                _l[k - 1].df = new Vector(sizes[k]); // создаём вектор для производной слоя
                _deltas[k - 1] = new Vector(sizes[k]); // создаём вектор для дельт
            }
        }
        // прямое распространение
        public Vector Forward(Vector input)
        {
            for (int k = 0; k < _layersN; k++)
            {
                if (k == 0)
                {
                    for (int i = 0; i < input.N; i++)
                        _l[k].x[i] = input[i];
                }
                else
                {
                    for (int i = 0; i < _l[k - 1].z.N; i++)
                        _l[k].x[i] = _l[k - 1].z[i];
                }
                for (int i = 0; i < _weights[k].N; i++)
                {
                    double y = 0;
                    for (int j = 0; j < _weights[k].M; j++)
                        y += _weights[k][i, j] * _l[k].x[j];
                    // активация с помощью сигмоидальной функции
                    _l[k].z[i] = 1 / (1 + Math.Exp(-y));
                    _l[k].df[i] = _l[k].z[i] * (1 - _l[k].z[i]);
                }
            }
            return _l[_layersN - 1].z; // возвращаем результат
        }
        // обновление весовых коэффициентов, alpha - скорость обучения
        private void UpdateWeights(double alpha)
        {
            for (int k = 0; k < _layersN; k++)
            {
                for (int i = 0; i < _weights[k].N; i++)
                {
                    for (int j = 0; j < _weights[k].M; j++)
                    {
                        _weights[k][i, j] -= alpha * _deltas[k][i] * _l[k].x[j];
                    }
                }
            }
        }
        // обратное распространение
        private void Backward(Vector output, ref double error)
        {
            int last = _layersN - 1;
            error = 0; // обнуляем ошибку
            for (int i = 0; i < output.N; i++)
            {
                double e = _l[last].z[i] - output[i]; // находим разность значений векторов
                _deltas[last][i] = e * _l[last].df[i]; // запоминаем дельту
                error += e * e / 2; // прибавляем к ошибке половину квадрата значения
            }
            // вычисляем каждую предудущю дельту на основе текущей с помощью умножения на транспонированную матрицу
            for (int k = last; k > 0; k--)
            {
                for (int i = 0; i < _weights[k].M; i++)
                {
                    _deltas[k - 1][i] = 0;
                    for (int j = 0; j < _weights[k].N; j++)
                        _deltas[k - 1][i] += _weights[k][j, i] * _deltas[k][j];
                    _deltas[k - 1][i] *= _l[k - 1].df[i]; // умножаем получаемое значение на производную предыдущего слоя
                }
            }
        }
        public void Train(Vector[] X, Vector[] Y, double alpha, double eps, int epochs)
        {
            int epoch = 1; // номер эпохи
            double error; // ошибка эпохи
            do
            {
                error = 0; // обнуляем ошибку
                // проходимся по всем элементам обучающего множества
                for (int i = 0; i < X.Length; i++)
                {
                    Forward(X[i]); // прямое распространение сигнала
                    Backward(Y[i], ref error); // обратное распространение ошибки
                    UpdateWeights(alpha); // обновление весовых коэффициентов
                }
                epoch++; // увеличиваем номер эпохи
            } while (epoch <= epochs && error > eps);
        }
    }
}
