using System;
using System.Collections.Specialized;

namespace Genetic.Algorithm
{
    // Класс описывающий генотип
    public class Genotype : IComparable<Genotype>
	{
		public static readonly int _nMax = 1024;
		static BitVector32.Section[] _s;
		static Random _random;
		public BitVector32 ByteGens;

		public static void Init()
		{
			_s = new BitVector32.Section[4];
			_s[0] = BitVector32.CreateSection((1 << 10) - 1);
			_s[1] = BitVector32.CreateSection((1 << 10) - 1, _s[0]);
			_s[2] = BitVector32.CreateSection((1 << 10) - 1, _s[1]);
			_s[3] = BitVector32.CreateSection((1 << 10) - 1, _s[2]);
			_random = new Random();
		}

		public Genotype()
		{
			ByteGens = new BitVector32();
		}

		public Genotype(Genotype g)
		{
			ByteGens = g.ByteGens;
		}

		public Genotype(int x1, int x2, int x3, int x4)
		{
			ByteGens[_s[0]] = x1;
			ByteGens[_s[1]] = x2;
			ByteGens[_s[2]] = x3;
			ByteGens[_s[3]] = x4;
		}

		public Genotype Clone()
		{
			return new Genotype(this);
		}

		// Значение функции приспособленности особи с данным генотипом
		public double FitFunction
		{
			get { return Evolution.GetFitnessFunction(this[0], this[1], this[2], this[3]); }
		}

		// Длина битовой строки
		public static int Length
		{
			get { return 4 * 10; }
		}

		// Метод генерации генотипа
		public void GenerateRandomGenotype()
		{
			for (int i = 0; i < 4; i++)
				ByteGens[_s[i]] = _random.Next(_nMax);
		}

		// Метод получения потомка в результате кроссинговера
		public void MakeCross(int place, Genotype m, Genotype f)
		{
			for (int i = 0; i < place; i++)
				ByteGens[1 << i] = m.ByteGens[1 << i];
			for (int i = place; i < Length; i++)
				ByteGens[1 << i] = f.ByteGens[1 << i];
		}

		// Метод, моделирующий мутацию гена
		public void MakeMutation()
		{
			ByteGens[1 << _random.Next(Length)] = !ByteGens[1 << _random.Next(Length)];
		}

		public int CompareTo(Genotype other)
		{
			if (FitFunction > other.FitFunction)
				return 1;
			if (FitFunction < other.FitFunction)
				return -1;
			return 0;
		}

		public int this[int idx]
		{
			get { return ByteGens[_s[idx]]; }
			set { ByteGens[_s[idx]] = value; }
		}

		public override string ToString()
		{
			return string.Format($"x1 = {this[0]}, x2 = {this[1]}, x3 = {this[2]}, x4 = {this[3]}");
		}
	}
}
