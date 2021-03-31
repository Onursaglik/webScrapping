using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class AnahtarKelime
    {
        public string title="";
        public string headers = "";
        public SortedDictionary<String,
                    int> mp = new SortedDictionary<String,
                                                   int>();
        public SortedDictionary<String,
                    int> anahtar_kelimeler = new SortedDictionary<String,
                                                   int>();

    }
}
