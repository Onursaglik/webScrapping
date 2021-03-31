using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class paragraf
    {
        public double kelimesayisi=0.0;
        public string metin_1="";
        public List<Double> oranlar=new List<Double>();
        public List<string> urller = new List<string>();
        public  SortedDictionary<String, int> anahtar_kelimeler_1 = new SortedDictionary<String, int>();
        public List<SortedDictionary<String, int>> anahtar_kelimeler_tumurller = new List<SortedDictionary<String, int>>();
        public List<SortedDictionary<String, int>> alakalianahtar_kelimeler_tumurller = new List<SortedDictionary<String, int>>();
        public SortedDictionary<String, int> yedek_anahtarlar = new SortedDictionary<String, int>();
        public double anahtar_oran;
        public List<Agac> agac = new List<Agac>();
    }
}
