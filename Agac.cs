using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Agac
    {
        public string root;
        public SortedDictionary<String, int> anahtar_kelimeler = new SortedDictionary<String, int>();
        public SortedDictionary<String, int> alakali_anahtar_kelimeler = new SortedDictionary<String, int>();
        public double benzerlik_skoru;
        public List<Node> node = new List<Node>();
    }
}
