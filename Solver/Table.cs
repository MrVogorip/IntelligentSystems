using System;
using System.Collections.Generic;

namespace Solver
{
    public class Table
    {
        private readonly List<(int, Tuple<int, string>)> _hashtable;
        public Table()
        {
            _hashtable = new List<(int, Tuple<int, string>)>
            {
                {( 1, new Tuple<int, string>(0, "Нет") )},
                {( 2, new Tuple<int, string>(0, "Да") )},
                {( 3, new Tuple<int, string>(2, "Нет") )},
                {( 4, new Tuple<int, string>(2, "Да")) },
                {( 5, new Tuple<int, string>(4, "Да, есть опыт")) },
                {( 6, new Tuple<int, string>(4, "Больше года") )},
                {( 7, new Tuple<int, string>(6, "В помещении") )},
                {( 8, new Tuple<int, string>(6, "На улице, на природе") )},
                {( 9, new Tuple<int, string>(7, "Нет") )},
                {( 10, new Tuple<int, string>(7, "Да")) },
                {( 11, new Tuple<int, string>(8, "Портреты")) },
                {( 12, new Tuple<int, string>(8, "Пейзажи")) },
                {( 13, new Tuple<int, string>(8, "Всё") )},
                {( 14, new Tuple<int, string>(10, "Нет") )},
                {( 15, new Tuple<int, string>(10, "Да")) },
                {( 16, new Tuple<int, string>(1, "Качество фотографий")) },
                {( 17, new Tuple<int, string>(1, "Стоимость фотоаппарата") )},
                {( 18, new Tuple<int, string>(1, "Наличие видео съемки")) },
            };
        }
        public List<(int, Tuple<int, string>)> GetAnswers(int status)
        {
            List<(int, Tuple<int, string>)> Answers = new List<(int, Tuple<int, string>)>();
            for (int i = 0; i < _hashtable.Count; i++)
            {
                if(_hashtable[i].Item2.Item1 == status)
                {
                    Answers.Add(_hashtable[i]);
                }
            }
            return Answers;
        }
    }
}
