using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver
{
    public class Questions
    {
        private readonly List<Tuple<int,string>> _questions;
        public Questions()
        {
            _questions = new List<Tuple<int, string>>
            {
                new Tuple<int, string>(0, "Рассчитываете ли вы потратить большую сумму денег на покупку фотика?"),
                new Tuple<int, string>(1, "Что для вас важнее?"),
                new Tuple<int, string>(2, "Умеете ли вы обращаться с профессиональным фотиком?"),
                new Tuple<int, string>(4, "Какой у вас опыт с фотиком?"),
                new Tuple<int, string>(6, "Где вы предпочитаете фоткать?"),
                new Tuple<int, string>(7, "В студии?"),
                new Tuple<int, string>(8, "Что вам нравиться фоткать?"),
                new Tuple<int, string>(10, "Вы будите пользоваться аппаратурой?")
            };
        }
        public Tuple<int, string> GetQuestion(int status)
        {
            return _questions.FirstOrDefault(x => x.Item1 == status);
        }
    }
}
