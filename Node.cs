using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Node
    {
        public string parent;
        public SortedDictionary<String, int> anahtar_kelimeler_parent = new SortedDictionary<String, int>();
        public SortedDictionary<String, int> alakali_anahtar_kelimeler_parent = new SortedDictionary<String, int>();
        public List<string> childs = new List<string>();
        public double benzerlik_skoru;
        public List<SortedDictionary<String, int>>anahtar_kelimeler_childs= new List<SortedDictionary<String, int>>();
        public List<SortedDictionary<String, int>> alakali_anahtar_kelimeler_childs = new List<SortedDictionary<String, int>>();
    }
}
