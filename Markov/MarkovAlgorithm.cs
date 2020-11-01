using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace Markov
{
    public class MarkovAlgorithm
    {
        private Random _random;
        private int _size;
        private int _limit;
        private bool _exact;
        public MarkovAlgorithm(int size = 2, int limit = 25, bool exact = false)
        {
            _random = new Random();
            _size = size;
            _limit = limit;
            _exact = exact;
        }
        public Dictionary<string, Dictionary<string, uint>> BuildFrequency(string s)
        {
            Dictionary<string, Dictionary<string, uint>> frequency = new Dictionary<string, Dictionary<string, uint>>();
            string prev = string.Empty;
            foreach (string word in Chunk(s, _size))
            {
                if (frequency.ContainsKey(prev))
                {
                    Dictionary<string, uint> w = frequency[prev];
                    if (w.ContainsKey(word))
                        w[word] += 1;
                    else
                        w.Add(word, 1);
                }
                else
                    frequency.Add(prev, new Dictionary<string, uint>() { { word, 1 } });
                prev = word;
            }
            return frequency;
        }
        public string BuildString(Dictionary<string, Dictionary<string, uint>> frequency)
        {
            List<string> words = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string word in frequency.Keys.Skip(1))
            {
                if (char.IsUpper(word.First()))
                    words.Add(word);
            }
            if (words.Count > 0)
                stringBuilder.Append(words.ElementAt(_random.Next(0, words.Count)));
            string last = stringBuilder.ToString();
            stringBuilder.Append(" ");
            Dictionary<string, uint> tempFrequency = new Dictionary<string, uint>();
            for (uint i = 0; i < _limit; ++i)
            {
                if (frequency.ContainsKey(last))
                    tempFrequency = frequency[last];
                else
                    tempFrequency = frequency[""];
                last = ChooseNext(tempFrequency);
                stringBuilder.Append(last.Split(' ').Last()).Append(" ");
            }
            if (!_exact)
            {
                while (last.Last() != '.')
                {
                    if (frequency.ContainsKey(last))
                        tempFrequency = frequency[last];
                    else
                        tempFrequency = frequency[""];
                    last = ChooseNext(tempFrequency);
                    stringBuilder.Append(last.Split(' ').Last()).Append(" ");
                }
            }
            return stringBuilder.ToString();
        }
        private string[] Chunk(string sentence, int size)
        {
            string[] words = sentence.Split(' ');
            List<string> chunk = new List<string>();
            for (int i = 0; i < words.Length - size; ++i)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(words.Skip(i).Take(size).Aggregate((w, k) => w + " " + k));
                chunk.Add(sb.ToString());
            }
            return chunk.ToArray();
        }
        private string ChooseNext(Dictionary<string, uint> frequency)
        {
            long total = frequency.Sum(t => t.Value);
            while (true)
            {
                int i = _random.Next(0, frequency.Count);
                double c = _random.NextDouble();
                KeyValuePair<string, uint> k = frequency.ElementAt(i);
                if (c < (double)k.Value / total)
                    return k.Key;
            }
        }
    }
}
