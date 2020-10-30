using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver
{
    public class Results
    {
        private readonly List<Tuple<int, string>> _results;
        public Results()
        {
            _results = new List<Tuple<int, string>>
            {
                new Tuple<int, string>(3, "Вам могут быть доступны профессиональные фотоаппараты."),
                new Tuple<int, string>(5, "Предлагаем вам купить китовые фотоаппараты."),
                new Tuple<int, string>(9, "Вы можете приобрести фотоаппараты с объективом, отличающимся от стандартного."),
                new Tuple<int, string>(11, "Вам подойдут все фотоаппараты фирмы Canon или Nikon с «портретными» объективами."),
                new Tuple<int, string>(12, "Вам подойдут все фотоаппараты фирмы Canon или Nikon с широкоугольными объективами."),
                new Tuple<int, string>(13, "Вам подойдут все фотоаппараты фирмы Canon или Nikon."),
                new Tuple<int, string>(14, "Вам подойдут такие фотоаппараты, как Nikon D90, Canon D500."),
                new Tuple<int, string>(15, "Вам подойдут такие фотоаппараты, как Nikon D70, Canon 5D."),
                new Tuple<int, string>(16, "Вам подойдут фотоаппараты мыльницы с отличным качеством снимков, стоимостью выше 7-8 тыс. руб."),
                new Tuple<int, string>(17, "мы предлагаем вам простые фотоаппараты-мыльницы."),
                new Tuple<int, string>(18, "Вам подойдут такие фотоаппараты, как Nikon Coolpix S3100 Red и проч."),
            };
        }
        public string GetResult(int status)
        {
            return _results.FirstOrDefault(x => x.Item1 == status).Item2;
        }
    }
}
