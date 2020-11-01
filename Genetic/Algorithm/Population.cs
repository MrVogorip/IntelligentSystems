using System;

namespace Genetic.Algorithm
{
    public class Population
	{
		static Random _random;
		private Genotype[] _nextGenotype;
		private int[] _arrayForRoulett;
		public Genotype[] Members { get; set; }
		public int Num { get; set; }

		public static void Init()
		{
			_random = new Random();
		}

		public Population(int num)
		{
			Members = new Genotype[num];
			Num = num;
		}

		public void GenerateRandomPopulation()
		{
			for (int i = 0; i < Num; i++)
			{
				Members[i] = new Genotype();
				Members[i].GenerateRandomGenotype();
			}
		}

		// Метод, реализующий одноточечный кроссинговер
		public static void DoCrossingover(Genotype m, Genotype f, out Genotype s, out Genotype d)
		{
			int place = _random.Next(1, Genotype.Length);
			s = new Genotype();
			d = new Genotype();
			s.MakeCross(place, m, f);
			d.MakeCross(place, f, m);
		}

		// Метод смены поколения
		public void ChangeGeneration()
		{
			Array.Sort(Members);
			_arrayForRoulett = new int[1000];
			GenerateArrayForRoulette();

			_nextGenotype = new Genotype[Num];
			for (int i = 0; i < Members.Length; i++)
				AddChild(i);
			Members = _nextGenotype;

			Array.Sort(Members);
			AddParents();
			DoMutation();
			Array.Sort(Members);
		}

		// Мутация (вероятность мутации 1% для каждого генотипа)
		public void DoMutation()
		{
			for (int i = 0; i < Num; i++)
			{
				if (_random.Next(100) == 50)
					Members[i].MakeMutation();
			}
		}

		// Добавляем в популяцию лучших из предыдущего поколения
		public void AddParents()
		{
			int num = Members.Length / 100 * 10;
			int i = num - 1;
			int j = Members.Length - 1;
			while (i >= 0)
			{
				if (_nextGenotype[i].FitFunction < Members[j].FitFunction)
				{
					_nextGenotype[i] = Members[j].Clone();
					j--;
				}
				i--;
			}
		}

		// Создание массива для запуска рулетки
		public void GenerateArrayForRoulette()
		{
			double sum = 0;
			for (int i = 0; i < Members.Length; i++)
				sum += Members[i].FitFunction;

			int idex = 0;
			for (int i = 0; i < Members.Length; i++)
			{
				int temp = (int)Math.Floor(Members[i].FitFunction * 1.0 / sum * 1000);
				for (int j = 0; j < temp; j++)
				{
					_arrayForRoulett[idex] = i;
					idex++;
				}
			}
			while (idex < 1000)
			{
				_arrayForRoulett[idex] = _random.Next(Members.Length);
				idex++;
			}
		}

		// Добавление потомка
		public void AddChild(int idx)
		{
			Genotype m = Members[_arrayForRoulett[_random.Next(1000)]];
			Genotype f = Members[_arrayForRoulett[_random.Next(1000)]];
            DoCrossingover(m, f, out Genotype s, out Genotype d);
            if (_random.Next(2) == 1)
				_nextGenotype[idx] = s;
			else
				_nextGenotype[idx] = d;
		}
	}
}
