using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Kelimeler
    {
       public  List<String> kelime_listesi = new List<String>();
        public String tekmetin="";
       public  SortedDictionary<String,
                    int> mp = new SortedDictionary<String,
                                                   int>();

    }
}
