using System.Collections.Generic;

namespace Bayesian
{
    class Node
    {
        public List<int> Parents { get; set; }
        public string Name { get; set; }
        public double[,,] TruthTable { get; set; }
        public double[] CurrentValues { get; set; }
        public Node(string name)
        {
            Name = name;
            Parents = new List<int>();
            CurrentValues = new double[2];
            TruthTable = new double[10, 10, 10];
        }
        public void SetTruthTableProbabilities(double p, int p1, int p2 = 0, int p3 = 0)
        {
            TruthTable[p1, p2, p3] = p;
        }
        public double GetTruthTableProbability(int p1, int p2 = 0, int p3 = 0)
        {
            if (p2 < 0) 
                p2 = 0;
            if (p3 < 0)
                p3 = 0;
            return TruthTable[p1, p2, p3];
        }
    }
}
