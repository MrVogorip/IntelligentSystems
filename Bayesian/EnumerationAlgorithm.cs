using System.Collections.Generic;
using System.Linq;

namespace Bayesian
{
    class EnumerationAlgorithm
    {
        private Node[] _nodes; // Узлы
        private int _domain = 2; // Количество различных значений, которые может иметь узлы в нашем случае их всего 2 - True и False
        private int[] _сurrentEvidence; // Доказательство в данный момент 
        public EnumerationAlgorithm (Node[] n, int[] e)
        {
            _nodes = n;
            _сurrentEvidence = e;
        }
        // Функция нормализации
        private double[] Normalization(double[] q)
        {
            double sum = q.Sum();
            q[0] /= sum;
            q[1] /= sum;
            return q;
        }
        // Функция, которая получает идентификатор узлы и возвращает его распределение
        public double[] EnumerationAsk(int nodeId)
        {
            double[] Q = new double[2];
            // Узлы байесовской сети отсортированы в топологическом порядке
            var vars = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
            // initialEvidence (-1 = нет доказательств, 0 = true, 1 = false)
            // initialEvidence[0] = -1 => нет доказательств для узла 0
            int[] initialEvidence = (int[])_сurrentEvidence.Clone();
            // Если значение истинности NodeId установлено в initialEvidence, мы немедленно возвращаем его
            if (initialEvidence[nodeId] != -1)
            {
                Q[initialEvidence[nodeId]] = 1;
                return Normalization(Q);
            }
            // Проходимся по возможным значениям возможных значений из домена NodeId
            for (int i = 0; i < _domain; ++i)
            {
                // Здесь истинное значение NodeId равно определенному значению из домена
                initialEvidence[nodeId] = i;
                Q[i] = EumerateAll(vars, initialEvidence);
            }
            // Сумма Q (распределение по области) была равна 1
            return Normalization(Q);
        }
        // Функция, которая перебирает все узлы из списка realVars относительно доказательств из списка realEvidence
        private double EumerateAll(List<int> realVars, int[] realEvidence)
        {
            List<int> vars = new List<int>(realVars);
            int[] evidence = (int[])realEvidence.Clone();
            // Если в realVars не осталось переменных, мы возвращаем 1
            if (vars.Count == 0)
                return 1.0;
            int Y = vars[0];
            vars.RemoveAt(0);
            // Если есть доказательства для извлеченной переменной
            if (evidence[Y] != -1)
            {
                // Доказательства по умолчанию для родителей неизвестны => -1
                int j = 0, e1 = -1, e2 = -1;
                foreach (int parent in _nodes[Y].Parents)
                {
                    if (evidence[parent] != -1)
                    {
                        if (j == 0)
                        {
                            e1 = evidence[parent];
                            j = 1;
                        }
                        else
                        {
                            e2 = evidence[parent];
                        }
                    }
                }
                // Возвращаем вероятность того, что узел Y имеет значение, равное свидетельству Y
                // в отношении известных доказательств его родителей, которые были обработаны ранее
                // умножаем на EnumareteAll с оставшимися переменными и тем же свидетельством
                return _nodes[Y].GetTruthTableProbability(evidence[Y], e1, e2) * EumerateAll(vars, evidence);
            }
            else
            {
                // если у нас нет доказательств Y предполагаем, что возможные значения из домена истинны один за другим
                double sum = 0.0;
                int[] new_evidence = (int[])evidence.Clone();
                for (int i = 0; i < _domain; ++i)
                {
                    new_evidence[Y] = i;
                    int j = 0, e1 = -1, e2 = -1;
                    foreach (int parent in _nodes[Y].Parents)
                    {
                        if (evidence[parent] != -1)
                        {
                            if (j == 0)
                            {
                                e1 = evidence[parent];
                                j = 1;
                            }
                            else
                            {
                                e2 = evidence[parent];
                            }
                        }
                    }
                    // Возвращаем сумму произведений вероятности того, что Y равно каждому значению из домена
                    // в отношении доказательств, присвоенных Y родителям
                    // умножено на Eumerate Все оставшиеся vars и новое свидетельство
                    sum += _nodes[Y].GetTruthTableProbability(i, e1, e2) * EumerateAll(vars, new_evidence);
                }
                return sum;
            }
        }
    }
}
