namespace Network
{
    public class Vector
    {
        private double[] _v; // значения вектора
        public int N { get; set; } // длина вектора
        // конструктор из длины
        public Vector(int n)
        {
            N = n; // копируем длину
            _v = new double[n]; // создаём массив
        }
        // создание вектора из вещественных значений
        public Vector(params double[] values)
        {
            N = values.Length;
            _v = new double[N];
            for (int i = 0; i < N; i++)
                _v[i] = values[i];
        }
        // обращение по индексу
        public double this[int i]
        {
            get { return _v[i]; } // получение значение
            set { _v[i] = value; } // изменение значения
        }
    }
}
