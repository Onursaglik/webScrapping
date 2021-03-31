using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Kitap
    {

        public int id { get; set;}
        public string kitap_ad { get; set; }

        public String[] fun()
        {
            String[] kelimeler = { "merhaba", "hallo", "ohaaa" };
            return kelimeler;


        }

    }
}
