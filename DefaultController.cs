using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using System.Net;
using HtmlAgilityPack;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Collections;


namespace WebApplication2.Conroller
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            Kitap kitap = new Kitap();
            kitap.id = 1;
            kitap.kitap_ad = "";
            return View(new Kelimeler());
        }

        [HttpPost]
        public ActionResult Index(string Name)
        {

            string s2 = Name;





            var webclient = new WebClient();
            var text = webclient.DownloadString(Name);
            HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
            dokuman.LoadHtml(text);
            String str = "";
            Kelimeler kelimeler = new Kelimeler();

            text = webclient.DownloadString(Name);

            dokuman.LoadHtml(text);
            var title = dokuman.DocumentNode.SelectSingleNode("html/head/title");
            if (title != null)
                str += title.InnerText;

            var basliklar = dokuman.DocumentNode.SelectNodes("//h1");
            var basliklar_2 = dokuman.DocumentNode.SelectNodes("//h2");
            if (basliklar != null)
            {
                foreach (var metin in basliklar)
                    str += metin.InnerText + " ";
            }
            if (basliklar_2 != null)
            {
                foreach (var metin in basliklar_2)
                    str += metin.InnerText + " ";
            }

           var  metindeki_paragraflar = dokuman.DocumentNode.SelectNodes("//p");
            if (metindeki_paragraflar != null)
            {
                foreach (var metin in metindeki_paragraflar)
                {
                    str += metin.InnerText + " ";
                    kelimeler.kelime_listesi.Add(metin.InnerText);
                }
            }


            
            
            str = Regex.Replace(str, "[0-9]", ".");
            Boolean hatali = false;
            string kelimeler_2 = "";
            str = str.Replace("\'", " ");
            str = str.Replace("’", " ");
            str = str.Replace("\"", "");
            str = str.Replace(":", "");
            str = str.Replace(",", "");
            str = str.Replace(";", "");
            str = str.Replace(".", "");
            str = str.Replace("-", " ");
            str = str.Replace("–", " ");
            str = str.Replace("[", "");
            str = str.Replace("]", "");
            str = str.Replace("(", "");
            str = str.Replace(")", "");


            string str_duzgun = "";


            foreach (var kelime in str.Split(" "))
            {

              

                if (!kelime.Equals(""))
                    str_duzgun += kelime + " ";
                

            }


            SortedDictionary<String,
                    int> mp = new SortedDictionary<String,
                                                   int>();

            // Splitting to find the word
            String[] arr = str_duzgun.Split(' ');

            // Loop to iterate over the words
            for (int i = 0; i < arr.Length; i++)
            {

                // Condition to check if the 
                // array element is present 
                // the hash-map
                if (mp.ContainsKey(arr[i]))
                {
                    mp[arr[i]] = mp[arr[i]] + 1;
                }
                else
                {
                    mp.Add(arr[i], 1);
                }
            }

            kelimeler.mp = mp;
            // Loop to iterate over the 
            // elements of the map
            foreach (KeyValuePair<String, int> entry in mp)
            {
                Console.WriteLine(entry.Key + " - " +
                                  entry.Value);
            }










            kelimeler.tekmetin = str_duzgun;
            Console.WriteLine(str);

            dynamic mymodel = new ExpandoObject();






            return View(kelimeler);
        }

        public IActionResult Index2()
        {
            var kitaplar = new List<Kitap>()
            {

                new Kitap(){id=1,kitap_ad="selam"},
                new Kitap(){id=2,kitap_ad="selami"}


            };
            return View(new AnahtarKelime());
        }

        [HttpPost]
        public ActionResult Index2(string Name)
        {

            SortedDictionary<String,
                   int> mp = new SortedDictionary<String,
                                                  int>();

            var webGet = new HtmlWeb();
            var document = webGet.Load(Name);
            var title = document.DocumentNode.SelectSingleNode("html/head/title");
            var headers = document.DocumentNode.SelectNodes("//h1");
            var headers_2 = document.DocumentNode.SelectNodes("//h2");
            var webclient = new WebClient();
            var text = webclient.DownloadString(Name);
            HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
            dokuman.LoadHtml(text);
            HtmlNodeCollection basliklar = dokuman.DocumentNode.SelectNodes("//p");

            AnahtarKelime anahtarlar = new AnahtarKelime();
            
            string kelimeler = "";
            if (basliklar != null)
            {
                foreach (var baslik in basliklar)
                    kelimeler += baslik.InnerText + " ";
            }
            if (headers != null)
            {
                foreach (var baslik in headers)
                    kelimeler += baslik.InnerText + " ";

            }


            if (headers_2 != null)
            {
                foreach (var baslik in headers_2)
                    kelimeler += baslik.InnerText + " ";
            }
            if(title!=null)
            kelimeler += title.InnerText + " ";

            //kelimeler = Regex.Replace(kelimeler, @"[^\w\d\s][0-9];,", " ");

            var replacements = new[]{
                 new{Find="don",Replace=""},
                new{Find="how",Replace=""},
                new{Find="or",Replace=""},
                new{Find="us",Replace=""},
                new{Find="what",Replace=""},
                new{Find="our",Replace=""},
                 new{Find="can",Replace=""},
                new{Find="then",Replace=""},
                new{Find="you",Replace=""},
   new{Find="the",Replace=""},
   new{Find="by",Replace=""},
   new{Find="I",Replace=""},
   new{Find="an",Replace=""},
   new{Find="In",Replace=""},
   new{Find="He",Replace=""},
   new{Find="he",Replace=""},
   new{Find="She",Replace=""},
   new{Find="has",Replace=""},
   new{Find="now",Replace=""},
   new{Find="this",Replace=""},
   new{Find="Has",Replace=""},
   new{Find="Now",Replace=""},
   new{Find="no",Replace=""},
   new{Find="she",Replace=""},
   new{Find="it",Replace=""},
   new{Find="him",Replace=""},
   new{Find="his",Replace=""},
   new{Find="her",Replace=""},
   new{Find="from",Replace=""},
   new{Find="which",Replace=""},
   new{Find="after",Replace=""},
   new{Find="is",Replace=""},
   new{Find="was",Replace=""},
   new{Find="were",Replace=""},
   new{Find="did",Replace=""},
   new{Find="when",Replace=""},
   new{Find="who",Replace=""},
   new{Find="but",Replace=""},
   new{Find="and",Replace=""},
   new{Find="to",Replace=""},
   new{Find="with",Replace=""},
   new{Find="of",Replace=""},
   new{Find="that",Replace=""},
   new{Find="s",Replace=""},
   new{Find="not",Replace=""},
   new{Find="on",Replace=""},
   new{Find="at",Replace=""},
   new{Find="as",Replace=""},
   new{Find="t",Replace=""},
   new{Find="would",Replace=""},
   new{Find="The",Replace=""},
   new{Find="for",Replace=""},
   new{Find="be",Replace=""},
   new{Find="its",Replace=""},
   new{Find="no",Replace=""},
   new{Find="been",Replace=""},
   new{Find="should",Replace=""},
    new{Find="in",Replace=""},
    new{Find="ago",Replace=""},
   new{Find="had",Replace=""},
   new{Find="just",Replace=""},
   new{Find="up",Replace=""},
   new{Find="about",Replace=""},
    new{Find="any",Replace=""},
    new{Find="have",Replace=""},

      new{Find="their",Replace=""},
     new{Find="all",Replace=""},
     new{Find="are",Replace=""},
     new{Find="they",Replace=""},
     new{Find="we",Replace=""},
       new{Find="does",Replace=""},
      new{Find="do",Replace=""},
      new{Find="will",Replace=""},


   new { Find = "bir", Replace = "" },
   new { Find = "de", Replace = "" },
   new { Find = "da", Replace = "" },
            new { Find = "ile", Replace = "" },
            new { Find = "nın", Replace = "" },
            new { Find = "bu", Replace = "" },
            new { Find = "Bu", Replace = "" },
            new { Find = "için", Replace = "" },
            new { Find = "nin", Replace = "" },
            new { Find = "ta", Replace = "" },
            new { Find = "te", Replace = "" },
            new { Find = "ün", Replace = "" },
            new { Find = "ın", Replace = "" },
            new { Find = "ye", Replace = "" },
            new { Find = "daha", Replace = "" },
            new { Find = "ancak", Replace = "" },
            new { Find = "Ancak", Replace = "" },
            new { Find = "yi", Replace = "" },
            new { Find = "dan", Replace = "" },
            new { Find = "daki", Replace = "" },
            new { Find = "na", Replace = "" },
            new { Find = "ve", Replace = "" },
            new { Find = "ve", Replace = "" },
            new { Find = "deki", Replace = "" },
            new { Find = "bir", Replace = "" },
            new { Find = "gibi", Replace = "" },
            new { Find = "bazı", Replace = "" },
             new { Find = "ise", Replace = "" },
              new { Find = "çok", Replace = "" },
               new { Find = "ye", Replace = "" },
                new { Find = "ya", Replace = "" },
                 new { Find = "e", Replace = "" },
                  new { Find = "a", Replace = "" },
                   new { Find = "den", Replace = "" },
                    new { Find = "dan", Replace = "" },
   new { Find = "de", Replace = "" },
      new { Find = "da", Replace = "" },
      new { Find = "un", Replace = "" },
      new { Find = "ın", Replace = "" },
      new { Find = "in", Replace = "" },
};
            kelimeler = Regex.Replace(kelimeler, "[0-9]", ".");
            Boolean hatali = false;
            string kelimeler_2 = "";
            kelimeler = kelimeler.Replace("\'", " ");
            kelimeler = kelimeler.Replace("’", " ");
            kelimeler = kelimeler.Replace("\"", "");
            kelimeler = kelimeler.Replace(":", "");
            kelimeler = kelimeler.Replace(",", "");
            kelimeler = kelimeler.Replace(";", "");
            kelimeler = kelimeler.Replace(".", "");
            kelimeler = kelimeler.Replace("-", " ");
            kelimeler = kelimeler.Replace("–", " ");
            kelimeler = kelimeler.Replace("[", "");
            kelimeler = kelimeler.Replace("]", "");
            kelimeler = kelimeler.Replace("(", "");
            kelimeler = kelimeler.Replace(")", "");

            foreach (var kelime in kelimeler.Split(" "))
            {

                foreach (var find in replacements)
                {

                    if (kelime.Equals(find.Find, StringComparison.InvariantCultureIgnoreCase))
                        hatali = true;

                }

                if (hatali == false && !kelime.Equals(""))
                    kelimeler_2 += kelime + " ";

                hatali = false;

            }

            

        
            String[] arr = kelimeler_2.Split(' ');

            
            for (int i = 0; i < arr.Length; i++)
            {

               
                if (mp.ContainsKey(arr[i]))
                {
                    mp[arr[i]] = mp[arr[i]] + 1;
                }
                else
                {
                    mp.Add(arr[i], 1);
                }
            }

            anahtarlar.mp = mp;

           
            int sayi = 0;
            foreach (KeyValuePair<string, int> word in anahtarlar.mp.OrderByDescending(key => key.Value))
            {


                if (sayi == 10)
                    break;

                anahtarlar.anahtar_kelimeler.Add(word.Key, word.Value);
                sayi++;

            }



            return View(anahtarlar);
        }


        public IActionResult Index3()
        {

            return View(new paragraf());
        }

        [HttpPost]
        public ActionResult Index3(string Url1, string Url2)
        {
            string tek_metin_1="";
            var webclient = new WebClient();
            var text = webclient.DownloadString(Url1);
            HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
            dokuman.LoadHtml(text);
            var title = dokuman.DocumentNode.SelectSingleNode("html/head/title");
            HtmlNodeCollection basliklar = dokuman.DocumentNode.SelectNodes("//h1");
            HtmlNodeCollection basliklar_2 = dokuman.DocumentNode.SelectNodes("//h2");
            if(title!=null)
            tek_metin_1 = title.InnerText + " ";
            if (basliklar != null)
            {
                foreach (var metin in basliklar)
                    tek_metin_1 += metin.InnerText + " ";

            }

            if (basliklar_2 != null)
            {
                foreach (var metin in basliklar_2)
                    tek_metin_1 += metin.InnerText + " ";
            }

            HtmlNodeCollection kelimeler = dokuman.DocumentNode.SelectNodes("//p");

            if (kelimeler != null)
            {
                foreach (var metin in kelimeler)
                    tek_metin_1 += metin.InnerText + " ";

            }


            var replacements = new[]{
                  new{Find="don",Replace=""},
                new{Find="how",Replace=""},
                new{Find="or",Replace=""},
                new{Find="us",Replace=""},
                new{Find="what",Replace=""},
                new{Find="our",Replace=""},
                  new{Find="can",Replace=""},
                new{Find="then",Replace=""},
                new{Find="you",Replace=""},
   new{Find="the",Replace=""},
   new{Find="by",Replace=""},
   new{Find="I",Replace=""},
   new{Find="an",Replace=""},
   new{Find="In",Replace=""},
   new{Find="He",Replace=""},
   new{Find="he",Replace=""},
   new{Find="She",Replace=""},
   new{Find="has",Replace=""},
   new{Find="now",Replace=""},
   new{Find="this",Replace=""},
   new{Find="Has",Replace=""},
   new{Find="Now",Replace=""},
   new{Find="no",Replace=""},
   new{Find="she",Replace=""},
   new{Find="it",Replace=""},
   new{Find="him",Replace=""},
   new{Find="his",Replace=""},
   new{Find="her",Replace=""},
   new{Find="from",Replace=""},
   new{Find="which",Replace=""},
   new{Find="after",Replace=""},
   new{Find="is",Replace=""},
   new{Find="was",Replace=""},
   new{Find="were",Replace=""},
   new{Find="did",Replace=""},
   new{Find="when",Replace=""},
   new{Find="who",Replace=""},
   new{Find="but",Replace=""},
   new{Find="and",Replace=""},
   new{Find="to",Replace=""},
   new{Find="with",Replace=""},
   new{Find="of",Replace=""},
   new{Find="that",Replace=""},
   new{Find="s",Replace=""},
   new{Find="not",Replace=""},
   new{Find="on",Replace=""},
   new{Find="at",Replace=""},
   new{Find="as",Replace=""},
   new{Find="t",Replace=""},
   new{Find="would",Replace=""},
   new{Find="The",Replace=""},
   new{Find="for",Replace=""},
   new{Find="be",Replace=""},
   new{Find="its",Replace=""},
   new{Find="no",Replace=""},
   new{Find="been",Replace=""},
   new{Find="should",Replace=""},
    new{Find="in",Replace=""},
    new{Find="ago",Replace=""},
   new{Find="had",Replace=""},
   new{Find="just",Replace=""},
   new{Find="up",Replace=""},
   new{Find="about",Replace=""},
    new{Find="any",Replace=""},
    new{Find="have",Replace=""},

      new{Find="their",Replace=""},
     new{Find="all",Replace=""},
     new{Find="are",Replace=""},
     new{Find="they",Replace=""},
     new{Find="we",Replace=""},
       new{Find="does",Replace=""},
      new{Find="do",Replace=""},
      new{Find="will",Replace=""},


   new { Find = "bir", Replace = "" },
   new { Find = "de", Replace = "" },
   new { Find = "da", Replace = "" },
            new { Find = "ile", Replace = "" },
            new { Find = "nın", Replace = "" },
            new { Find = "bu", Replace = "" },
            new { Find = "Bu", Replace = "" },
            new { Find = "için", Replace = "" },
            new { Find = "nin", Replace = "" },
            new { Find = "ta", Replace = "" },
            new { Find = "te", Replace = "" },
            new { Find = "ün", Replace = "" },
            new { Find = "ın", Replace = "" },
            new { Find = "ye", Replace = "" },
            new { Find = "daha", Replace = "" },
            new { Find = "ancak", Replace = "" },
            new { Find = "Ancak", Replace = "" },
            new { Find = "yi", Replace = "" },
            new { Find = "dan", Replace = "" },
            new { Find = "daki", Replace = "" },
            new { Find = "na", Replace = "" },
            new { Find = "ve", Replace = "" },
            new { Find = "ve", Replace = "" },
            new { Find = "deki", Replace = "" },
            new { Find = "bir", Replace = "" },
            new { Find = "gibi", Replace = "" },
            new { Find = "bazı", Replace = "" },
             new { Find = "ise", Replace = "" },
              new { Find = "çok", Replace = "" },
               new { Find = "ye", Replace = "" },
                new { Find = "ya", Replace = "" },
                 new { Find = "e", Replace = "" },
                  new { Find = "a", Replace = "" },
                   new { Find = "den", Replace = "" },
                    new { Find = "dan", Replace = "" },
   new { Find = "de", Replace = "" },
      new { Find = "da", Replace = "" },
      new { Find = "un", Replace = "" },
      new { Find = "ın", Replace = "" },
      new { Find = "in", Replace = "" },
};

            tek_metin_1 = Regex.Replace(tek_metin_1, "[0-9]", ".");
            Boolean hatali = false;
            string tek_metin_1_duzgun = "";
            tek_metin_1 = tek_metin_1.Replace("\'", " ");
            tek_metin_1 = tek_metin_1.Replace("’", " ");
            tek_metin_1 = tek_metin_1.Replace("\"", "");
            tek_metin_1 = tek_metin_1.Replace(":", "");
            tek_metin_1 = tek_metin_1.Replace(",", "");
            tek_metin_1 = tek_metin_1.Replace(";", "");
            tek_metin_1 = tek_metin_1.Replace(".", "");
            tek_metin_1 = tek_metin_1.Replace("-", " ");
            tek_metin_1 = tek_metin_1.Replace("–", "");
            tek_metin_1 = tek_metin_1.Replace("[", "");
            tek_metin_1 = tek_metin_1.Replace("]", "");
            tek_metin_1 = tek_metin_1.Replace("(", "");
            tek_metin_1 = tek_metin_1.Replace(")", "");

            double tek_metin_1_kelimesayisi = tek_metin_1.Split(" ").Length;

            foreach (var kelime in tek_metin_1.Split(" "))
            {

                foreach (var find in replacements)
                {

                    if (kelime.Equals(find.Find, StringComparison.InvariantCultureIgnoreCase))
                        hatali = true;

                }

                if (!kelime.Equals("")&&!kelime.Equals(" ")&&!hatali)
                    tek_metin_1_duzgun += kelime + " ";

                hatali = false;

            }



            String[] arr = tek_metin_1_duzgun.Split(' ');
            SortedDictionary<String,   int> mp = new SortedDictionary<String,int>();
            SortedDictionary<String, int> anahtar_kelimeler_1 = new SortedDictionary<String, int>();
            // Loop to iterate over the words
            for (int i = 0; i < arr.Length; i++)
            {

                // Condition to check if the 
                // array element is present 
                // the hash-map
                if (mp.ContainsKey(arr[i]))
                {
                    mp[arr[i]] = mp[arr[i]] + 1;
                }
                else
                {
                    mp.Add(arr[i], 1);
                }
            }

         

            // Loop to iterate over the 
            // elements of the map
            int sayi = 0;
            foreach (KeyValuePair<string, int> word in mp.OrderByDescending(key => key.Value))
            {


                if (sayi == 10)
                    break;

                anahtar_kelimeler_1.Add(word.Key, word.Value);
                sayi++;

            }


            string tek_metin_2 = "";
          
             text = webclient.DownloadString(Url2);
   
            dokuman.LoadHtml(text);
           title = dokuman.DocumentNode.SelectSingleNode("html/head/title");
           basliklar = dokuman.DocumentNode.SelectNodes("//h1");
             basliklar_2 = dokuman.DocumentNode.SelectNodes("//h2");
            if(title!=null)
            tek_metin_2 = title.InnerText + " ";
            if (basliklar != null)
            {
                foreach (var metin in basliklar)
                    tek_metin_2 += metin.InnerText + " ";
            }

            if (basliklar_2 != null)
            {
                foreach (var metin in basliklar_2)
                    tek_metin_2 += metin.InnerText + " ";
            }
             kelimeler = dokuman.DocumentNode.SelectNodes("//p");

            if (kelimeler != null)
            {
                foreach (var metin in kelimeler)
                    tek_metin_2 += metin.InnerText + " ";
            }


            tek_metin_2 = Regex.Replace(tek_metin_2, "[0-9]", ".");
            hatali = false;
            string tek_metin_2_duzgun = "";
             tek_metin_2 = tek_metin_2.Replace("\'", " ");
            tek_metin_2 = tek_metin_2.Replace("’", " ");
            tek_metin_2 = tek_metin_2.Replace("\"", "");
            tek_metin_2 = tek_metin_2.Replace(":", "");
            tek_metin_2 = tek_metin_2.Replace(",", "");
            tek_metin_2 = tek_metin_2.Replace(";", "");
            tek_metin_2 = tek_metin_2.Replace(".", "");
            tek_metin_2 = tek_metin_2.Replace("-", " ");
            tek_metin_2 = tek_metin_2.Replace("–", "");
            tek_metin_2 = tek_metin_2.Replace("[", "");
            tek_metin_2 = tek_metin_2.Replace("]", "");
            tek_metin_2 = tek_metin_2.Replace("(", "");
            tek_metin_2 = tek_metin_2.Replace(")", "");

            double tek_metin_2_kelimesayisi = tek_metin_2.Split(" ").Length;

            foreach (var kelime in tek_metin_2.Split(" "))
            {


                foreach (var find in replacements)
                {

                    if (kelime.Equals(find.Find, StringComparison.InvariantCultureIgnoreCase))
                        hatali = true;

                }


                if (!kelime.Equals("") && !kelime.Equals(" ")&&!hatali)
                    tek_metin_2_duzgun += kelime + " ";
                hatali = false;
              

            }

            SortedDictionary<String, int> mp_2 = new SortedDictionary<String, int>();
            SortedDictionary<String, int> anahtar_kelimeler_2 = new SortedDictionary<String, int>();
            arr = tek_metin_2_duzgun.Split(' ');
            for (int i = 0; i < arr.Length; i++)
            {

                // Condition to check if the 
                // array element is present 
                // the hash-map
                if (mp_2.ContainsKey(arr[i]))
                {
                    mp_2[arr[i]] = mp_2[arr[i]] + 1;
                }
                else
                {
                    mp_2.Add(arr[i], 1);
                }
            }

            sayi = 0;

            foreach (KeyValuePair<string, int> word in mp_2.OrderByDescending(key => key.Value))
            {


                if (sayi == 10)
                    break;

                anahtar_kelimeler_2.Add(word.Key, word.Value);
                sayi++;

            }

            // ArrayList<double> oranlar = new ArrayList();
            List<Double> oranlar = new List<Double>();
            foreach (var kontrol in anahtar_kelimeler_1)
            {

                if (mp_2.ContainsKey(kontrol.Key))
                {
                    if (mp_2[kontrol.Key] <= kontrol.Value)
                        oranlar.Add((double)mp_2[kontrol.Key] /(double) kontrol.Value);
                    else
                        oranlar.Add((double)kontrol.Value/(double)mp_2[kontrol.Key]);


                }


            }

            double toplam=0.0;
            double oran_2 = 0.0;
            double benzerlik = 0.0;
            if (tek_metin_1_kelimesayisi > tek_metin_2_kelimesayisi)
                oran_2 = (double)tek_metin_2_kelimesayisi / (double)tek_metin_1_kelimesayisi;

            if (tek_metin_1_kelimesayisi <= tek_metin_2_kelimesayisi)
                oran_2 = (double)tek_metin_1_kelimesayisi / (double)tek_metin_2_kelimesayisi;

            


            for (int i = 0; i < oranlar.Count; i++)
                toplam += (double)oranlar[i];
            toplam = toplam / (double)oranlar.Count;

            if (toplam <= oran_2)
                benzerlik = (toplam * oran_2)*100.0;
            if (toplam > oran_2)
                benzerlik = (oran_2  * toplam)*100.0;

            paragraf metin_sinif = new paragraf();
            metin_sinif.oranlar = oranlar;
            metin_sinif.anahtar_kelimeler_1 = anahtar_kelimeler_1;
            metin_sinif.anahtar_kelimeler_tumurller.Add(anahtar_kelimeler_1);
            metin_sinif.anahtar_kelimeler_tumurller.Add(anahtar_kelimeler_2);
            metin_sinif.metin_1 = tek_metin_2_duzgun;
            metin_sinif.anahtar_oran = benzerlik;

            return View(metin_sinif);
        }


        public IActionResult Index4()
        {

            return View(new paragraf());
        }

        [HttpPost]
        public ActionResult Index4(string anaurl,string Url1)
        {
            int urlsayisi = 0;
            
            List<string> webkumesi = new List<string>();
            paragraf islemler = new paragraf();
            paragraf islem = new paragraf();
            int k = 0;
            foreach (var url in Url1.Split(","))
            {
                islem.agac.Add(new Agac());
                islem.agac[k].root = url;
                webkumesi.Add(url);
                islemler.urller.Add(url);
                urlsayisi++;
                k++;
            }

            k = 0;


            int n = 0;

            foreach (var url1 in webkumesi)
            {
                Boolean hata_var = false;
                n = 0;
                int sinir = 0;
                var client = new WebClient();
                var htmlSource = client.DownloadString(url1);
                var doc = new HtmlDocument();
                doc.LoadHtml(htmlSource);
                List<string> urller = doc.DocumentNode.SelectNodes("//a[@href]").Select(node => node.Attributes["href"].Value).ToList();
                foreach (var url in urller)
                {
                    if (url.StartsWith(islem.agac[k].root) || url.StartsWith(islem.agac[k].root.Substring(0,10)))
                      {
                        try
                        {
                            client.DownloadString(url);


                        }
                        catch(Exception e)
                        {

                            hata_var = true;
                        }
                        if (hata_var == false)
                        {
                            islem.agac[k].node.Add(new Node());
                            islem.agac[k].node[n].parent = url;
                            islemler.urller.Add(url);
                            n++;
                            sinir++;
                        }
                    }
                    hata_var = false;
                    if (sinir == 5)
                        break;

                }

                k++;
            }

            k = 0;
            n = 0;
            

            foreach (var agaclar in islem.agac)
            {

                foreach(var nodes in agaclar.node)
                {
                    Boolean hata_var = false;
                    int sinir = 0;
                    var client = new WebClient();
                    var htmlSource = client.DownloadString(nodes.parent);
                    var doc = new HtmlDocument();
                    doc.LoadHtml(htmlSource);
                    List<string> urller = new List<string>();
                    if (doc.DocumentNode.SelectNodes("//a[@href]")!=null)
                    urller = doc.DocumentNode.SelectNodes("//a[@href]").Select(node => node.Attributes["href"].Value).ToList();
                    foreach (var url in urller)
                    {
                        if (url.StartsWith(islem.agac[k].node[n].parent))
                        {

                            try
                            {
                                client.DownloadString(url);


                            }
                            catch (Exception e)
                            {

                                hata_var = true;
                            }

                            if (hata_var == false)
                            {
                                islem.agac[k].node[n].childs.Add(url);
                                islemler.urller.Add(url);
                                sinir++;
                            }
                            
                          
                        }

                        if (sinir == 3)
                            break;
                        hata_var = false;
                    }
                  n++;
                }

                n = 0;
                k++;
            }



            islemler.agac = islem.agac;


            string tek_metin_1 = "";
            var webclient = new WebClient();
            var text = webclient.DownloadString(anaurl);
            HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
            dokuman.LoadHtml(text);
            string title = "";
            if(dokuman.DocumentNode.SelectSingleNode("html/head/title")!=null)
             title = dokuman.DocumentNode.SelectSingleNode("html/head/title").InnerText;
            HtmlNodeCollection basliklar = dokuman.DocumentNode.SelectNodes("//h1");
            HtmlNodeCollection basliklar_2 = dokuman.DocumentNode.SelectNodes("//h2");
            tek_metin_1 = title + " ";
            foreach (var metin in basliklar)
                tek_metin_1 += metin.InnerText + " ";
            foreach (var metin in basliklar_2)
                tek_metin_1 += metin.InnerText + " ";
            HtmlNodeCollection kelimeler = dokuman.DocumentNode.SelectNodes("//p");
            foreach (var metin in kelimeler)
                tek_metin_1 += metin.InnerText + " ";



            var replacements = new[]{
                  new{Find="don",Replace=""},
                new{Find="how",Replace=""},
                new{Find="or",Replace=""},
                new{Find="us",Replace=""},
                new{Find="what",Replace=""},
                new{Find="our",Replace=""},
                  new{Find="can",Replace=""},
                new{Find="then",Replace=""},
                new{Find="you",Replace=""},
   new{Find="the",Replace=""},
   new{Find="by",Replace=""},
   new{Find="I",Replace=""},
   new{Find="an",Replace=""},
   new{Find="In",Replace=""},
   new{Find="He",Replace=""},
   new{Find="he",Replace=""},
   new{Find="She",Replace=""},
   new{Find="has",Replace=""},
   new{Find="now",Replace=""},
   new{Find="this",Replace=""},
   new{Find="Has",Replace=""},
   new{Find="Now",Replace=""},
   new{Find="no",Replace=""},
   new{Find="she",Replace=""},
   new{Find="it",Replace=""},
   new{Find="him",Replace=""},
   new{Find="his",Replace=""},
   new{Find="her",Replace=""},
   new{Find="from",Replace=""},
   new{Find="which",Replace=""},
   new{Find="after",Replace=""},
   new{Find="is",Replace=""},
   new{Find="was",Replace=""},
   new{Find="were",Replace=""},
   new{Find="did",Replace=""},
   new{Find="when",Replace=""},
   new{Find="who",Replace=""},
   new{Find="but",Replace=""},
   new{Find="and",Replace=""},
   new{Find="to",Replace=""},
   new{Find="with",Replace=""},
   new{Find="of",Replace=""},
   new{Find="that",Replace=""},
   new{Find="s",Replace=""},
   new{Find="not",Replace=""},
   new{Find="on",Replace=""},
   new{Find="at",Replace=""},
   new{Find="as",Replace=""},
   new{Find="t",Replace=""},
   new{Find="would",Replace=""},
   new{Find="The",Replace=""},
   new{Find="for",Replace=""},
   new{Find="be",Replace=""},
   new{Find="its",Replace=""},
   new{Find="no",Replace=""},
   new{Find="been",Replace=""},
   new{Find="should",Replace=""},
    new{Find="in",Replace=""},
    new{Find="ago",Replace=""},
   new{Find="had",Replace=""},
   new{Find="just",Replace=""},
   new{Find="up",Replace=""},
   new{Find="about",Replace=""},
    new{Find="any",Replace=""},
    new{Find="have",Replace=""},

      new{Find="their",Replace=""},
     new{Find="all",Replace=""},
     new{Find="are",Replace=""},
     new{Find="they",Replace=""},
     new{Find="we",Replace=""},
       new{Find="does",Replace=""},
      new{Find="do",Replace=""},
      new{Find="will",Replace=""},


   new { Find = "bir", Replace = "" },
   new { Find = "de", Replace = "" },
   new { Find = "da", Replace = "" },
            new { Find = "ile", Replace = "" },
            new { Find = "nın", Replace = "" },
            new { Find = "bu", Replace = "" },
            new { Find = "Bu", Replace = "" },
            new { Find = "için", Replace = "" },
            new { Find = "nin", Replace = "" },
            new { Find = "ta", Replace = "" },
            new { Find = "te", Replace = "" },
            new { Find = "ün", Replace = "" },
            new { Find = "ın", Replace = "" },
            new { Find = "ye", Replace = "" },
            new { Find = "daha", Replace = "" },
            new { Find = "ancak", Replace = "" },
            new { Find = "Ancak", Replace = "" },
            new { Find = "yi", Replace = "" },
            new { Find = "dan", Replace = "" },
            new { Find = "daki", Replace = "" },
            new { Find = "na", Replace = "" },
            new { Find = "ve", Replace = "" },
            new { Find = "ve", Replace = "" },
            new { Find = "deki", Replace = "" },
            new { Find = "bir", Replace = "" },
            new { Find = "gibi", Replace = "" },
            new { Find = "bazı", Replace = "" },
             new { Find = "ise", Replace = "" },
              new { Find = "çok", Replace = "" },
               new { Find = "ye", Replace = "" },
                new { Find = "ya", Replace = "" },
                 new { Find = "e", Replace = "" },
                  new { Find = "a", Replace = "" },
                   new { Find = "den", Replace = "" },
                    new { Find = "dan", Replace = "" },
   new { Find = "de", Replace = "" },
      new { Find = "da", Replace = "" },
      new { Find = "un", Replace = "" },
      new { Find = "ın", Replace = "" },
      new { Find = "in", Replace = "" },
};

            tek_metin_1 = Regex.Replace(tek_metin_1, "[0-9]", ".");
            Boolean hatali = false;
            string tek_metin_1_duzgun = "";
            tek_metin_1 = tek_metin_1.Replace("\'", " ");
            tek_metin_1 = tek_metin_1.Replace("’", " ");
            tek_metin_1 = tek_metin_1.Replace("\"", "");
            tek_metin_1 = tek_metin_1.Replace(":", "");
            tek_metin_1 = tek_metin_1.Replace(",", "");
            tek_metin_1 = tek_metin_1.Replace(";", "");
            tek_metin_1 = tek_metin_1.Replace(".", "");
            tek_metin_1 = tek_metin_1.Replace("-", " ");
            tek_metin_1 = tek_metin_1.Replace("–", "");
            tek_metin_1 = tek_metin_1.Replace("[", "");
            tek_metin_1 = tek_metin_1.Replace("]", "");
            tek_metin_1 = tek_metin_1.Replace("(", "");
            tek_metin_1 = tek_metin_1.Replace(")", "");

            double anaurl_kelimesayisi = tek_metin_1.Split(" ").Length;

            foreach (var kelime in tek_metin_1.Split(" "))
            {

                foreach (var find in replacements)
                {

                    if (kelime.Equals(find.Find, StringComparison.InvariantCultureIgnoreCase))
                        hatali = true;

                }

                if (!kelime.Equals("") && !kelime.Equals(" ") && !hatali)
                    tek_metin_1_duzgun += kelime + " ";

                hatali = false;

            }



            String[] arr = tek_metin_1_duzgun.Split(' ');
            SortedDictionary<String, int> mp = new SortedDictionary<String, int>();
            SortedDictionary<String, int> anahtar_kelimeler_1 = new SortedDictionary<String, int>();
            // Loop to iterate over the words
            for (int i = 0; i < arr.Length; i++)
            {

                // Condition to check if the 
                // array element is present 
                // the hash-map
                if (mp.ContainsKey(arr[i]))
                {
                    mp[arr[i]] = mp[arr[i]] + 1;
                }
                else
                {
                    mp.Add(arr[i], 1);
                }
            }



            // Loop to iterate over the 
            // elements of the map
            int sayi = 0;
            foreach (KeyValuePair<string, int> word in mp.OrderByDescending(key => key.Value))
            {


                if (sayi == 10)
                    break;

                anahtar_kelimeler_1.Add(word.Key, word.Value);
                sayi++;

            }



            //agac kodu basliyor
            int agac_sayaci = 0;

            foreach(var agac in islemler.agac)
            {


                string tek_metin_asama_1 = "";

                text = webclient.DownloadString(agac.root);
                title = "";
                dokuman.LoadHtml(text);
                if (dokuman.DocumentNode.SelectSingleNode("html/head/title") != null)
                    title = dokuman.DocumentNode.SelectSingleNode("html/head/title").InnerText;
                tek_metin_asama_1 += title;
                basliklar = dokuman.DocumentNode.SelectNodes("//h1");
                basliklar_2 = dokuman.DocumentNode.SelectNodes("//h2");
                if (basliklar != null)
                {
                    foreach (var metin in basliklar)
                        tek_metin_asama_1 += metin.InnerText + " ";
                }
                if (basliklar_2 != null)
                {
                    foreach (var metin in basliklar_2)
                        tek_metin_asama_1 += metin.InnerText + " ";
                }

                kelimeler = dokuman.DocumentNode.SelectNodes("//p");
                if (kelimeler != null)
                {
                    foreach (var metin in kelimeler)
                        tek_metin_asama_1 += metin.InnerText + " ";
                }


                tek_metin_asama_1 = Regex.Replace(tek_metin_asama_1, "[0-9]", ".");
                hatali = false;
                string tek_metin_asama_1_duzgun = "";
                tek_metin_asama_1 = tek_metin_asama_1.Replace("\'", " ");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("’", " ");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("\"", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace(":", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace(",", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace(";", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace(".", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("-", " ");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("–", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("[", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("]", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("(", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace(")", "");

                double tek_metin_asama1_kelimesayisi = tek_metin_asama_1.Split(" ").Length;

                foreach (var kelime in tek_metin_asama_1.Split(" "))
                {


                    foreach (var find in replacements)
                    {

                        if (kelime.Equals(find.Find, StringComparison.InvariantCultureIgnoreCase))
                            hatali = true;

                    }


                    if (!kelime.Equals("") && !kelime.Equals(" ") && !hatali)
                        tek_metin_asama_1_duzgun += kelime + " ";
                    hatali = false;


                }

                SortedDictionary<String, int> mp_asama1 = new SortedDictionary<String, int>();
                SortedDictionary<String, int> anahtar_kelimeler_asama1 = new SortedDictionary<String, int>();
                arr = tek_metin_asama_1_duzgun.Split(' ');
                for (int i = 0; i < arr.Length; i++)
                {

                    // Condition to check if the 
                    // array element is present 
                    // the hash-map
                    if (mp_asama1.ContainsKey(arr[i]))
                    {
                        mp_asama1[arr[i]] = mp_asama1[arr[i]] + 1;
                    }
                    else
                    {
                        mp_asama1.Add(arr[i], 1);
                    }
                }

                sayi = 0;
                foreach (KeyValuePair<string, int> word in mp_asama1.OrderByDescending(key => key.Value))
                {


                    if (sayi == 10)
                        break;

                    anahtar_kelimeler_asama1.Add(word.Key, word.Value);
                    sayi++;

                }

                islemler.agac[agac_sayaci].anahtar_kelimeler = anahtar_kelimeler_asama1;


                //dugum kodu basliyor
                int dugum_sayaci = 0;
               

                foreach(var dugum in agac.node)
                {

                    Boolean daha_once_kontrol_edildi = false;
                    int kontrol_node = 0;
                    int kontrol_agac = 0;
                    int agac_node = 0;
                    for (int kontrol = 0; kontrol < agac_sayaci; kontrol++)
                    {

                        for(int kontrol2 = 0; kontrol2 < islem.agac[kontrol].node.Count(); kontrol2++)
                        {

                            if (islemler.agac[kontrol].node[kontrol2].parent == dugum.parent)
                            {
                                daha_once_kontrol_edildi = true;
                                kontrol_node = kontrol2;
                                kontrol_agac = kontrol;
                                break;
                            }

                          


                        }

                    }

                    if (daha_once_kontrol_edildi == false)
                    {

                        string tek_metin_asama_2 = "";

                        text = webclient.DownloadString(dugum.parent);
                        title = "";
                        dokuman.LoadHtml(text);
                        if (dokuman.DocumentNode.SelectSingleNode("html/head/title") != null)
                            title = dokuman.DocumentNode.SelectSingleNode("html/head/title").InnerText;
                        tek_metin_asama_2 += title;
                        basliklar = dokuman.DocumentNode.SelectNodes("//h1");
                        basliklar_2 = dokuman.DocumentNode.SelectNodes("//h2");
                        if (basliklar != null)
                        {
                            foreach (var metin in basliklar)
                                tek_metin_asama_2 += metin.InnerText + " ";
                        }
                        if (basliklar_2 != null)
                        {
                            foreach (var metin in basliklar_2)
                                tek_metin_asama_2 += metin.InnerText + " ";
                        }

                        kelimeler = dokuman.DocumentNode.SelectNodes("//p");
                        if (kelimeler != null)
                        {
                            foreach (var metin in kelimeler)
                                tek_metin_asama_2 += metin.InnerText + " ";
                        }


                        tek_metin_asama_2 = Regex.Replace(tek_metin_asama_2, "[0-9]", ".");
                        hatali = false;
                        string tek_metin_asama_2_duzgun = "";
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("\'", " ");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("’", " ");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("\"", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace(":", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace(",", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace(";", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace(".", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("-", " ");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("–", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("[", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("]", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("(", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace(")", "");


                        double tek_metin_asama2_kelimesayisi = tek_metin_asama_2.Split(" ").Length;


                        foreach (var kelime in tek_metin_asama_2.Split(" "))
                        {


                            foreach (var find in replacements)
                            {

                                if (kelime.Equals(find.Find, StringComparison.InvariantCultureIgnoreCase))
                                    hatali = true;

                            }


                            if (!kelime.Equals("") && !kelime.Equals(" ") && !hatali)
                                tek_metin_asama_2_duzgun += kelime + " ";
                            hatali = false;


                        }

                        SortedDictionary<String, int> mp_asama2 = new SortedDictionary<String, int>();
                        SortedDictionary<String, int> anahtar_kelimeler_asama2 = new SortedDictionary<String, int>();
                        arr = tek_metin_asama_2_duzgun.Split(" ");

                        for (int i = 0; i < arr.Length; i++)
                        {

                            // Condition to check if the 
                            // array element is present 
                            // the hash-map
                            if (mp_asama2.ContainsKey(arr[i]))
                            {
                                mp_asama2[arr[i]] = mp_asama2[arr[i]] + 1;
                            }
                            else
                            {
                                mp_asama2.Add(arr[i], 1);
                            }
                        }



                        sayi = 0;
                        foreach (KeyValuePair<string, int> word in mp_asama2.OrderByDescending(key => key.Value))
                        {


                            if (sayi == 10)
                                break;

                            anahtar_kelimeler_asama2.Add(word.Key, word.Value);
                            sayi++;

                        }

                        islemler.agac[agac_sayaci].node[dugum_sayaci].anahtar_kelimeler_parent = anahtar_kelimeler_asama2;

                        foreach (var anahtarkelime in anahtar_kelimeler_asama2)
                        {

                            if (mp_asama1.ContainsKey(anahtarkelime.Key))
                            {
                                mp_asama1[anahtarkelime.Key] = mp_asama1[anahtarkelime.Key] + 1;
                            }
                            else
                            {
                                mp_asama1.Add(anahtarkelime.Key, 1);
                            }



                        }





                        //childs kodu baslıyor
                        foreach (var child in dugum.childs)
                        {



                            text = webclient.DownloadString(child);
                            string tek_metin_asama3 = "";
                            title = "";
                            dokuman.LoadHtml(text);
                            if (dokuman.DocumentNode.SelectSingleNode("html/head/title") != null)
                                title = dokuman.DocumentNode.SelectSingleNode("html/head/title").InnerText;
                            tek_metin_asama3 += title;
                            basliklar = dokuman.DocumentNode.SelectNodes("//h1");
                            basliklar_2 = dokuman.DocumentNode.SelectNodes("//h2");
                            if (basliklar != null)
                            {
                                foreach (var metin in basliklar)
                                    tek_metin_asama3 += metin.InnerText + " ";
                            }
                            if (basliklar_2 != null)
                            {
                                foreach (var metin in basliklar_2)
                                    tek_metin_asama3 += metin.InnerText + " ";
                            }

                            kelimeler = dokuman.DocumentNode.SelectNodes("//p");
                            if (kelimeler != null)
                            {
                                foreach (var metin in kelimeler)
                                    tek_metin_asama3 += metin.InnerText + " ";
                            }

                            tek_metin_asama3 = Regex.Replace(tek_metin_asama3, "[0-9]", ".");
                            hatali = false;
                            string tek_metin_asama3_duzgun = "";
                            tek_metin_asama3 = tek_metin_asama3.Replace("\'", " ");
                            tek_metin_asama3 = tek_metin_asama3.Replace("’", " ");
                            tek_metin_asama3 = tek_metin_asama3.Replace("\"", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace(":", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace(",", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace(";", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace(".", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace("-", " ");
                            tek_metin_asama3 = tek_metin_asama3.Replace("–", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace("[", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace("]", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace("(", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace(")", "");


                            double tek_metin_asama3_kelimesayisi = tek_metin_asama3.Split(" ").Length;


                            foreach (var kelime in tek_metin_asama3.Split(" "))
                            {


                                foreach (var find in replacements)
                                {

                                    if (kelime.Equals(find.Find, StringComparison.InvariantCultureIgnoreCase))
                                        hatali = true;

                                }


                                if (!kelime.Equals("") && !kelime.Equals(" ") && !hatali)
                                    tek_metin_asama3_duzgun += kelime + " ";
                                hatali = false;


                            }

                            SortedDictionary<String, int> mp_asama3 = new SortedDictionary<String, int>();
                            SortedDictionary<String, int> anahtar_kelimeler_asama3 = new SortedDictionary<String, int>();
                            arr = tek_metin_asama3_duzgun.Split(' ');

                            for (int i = 0; i < arr.Length; i++)
                            {

                                // Condition to check if the 
                                // array element is present 
                                // the hash-map
                                if (mp_asama3.ContainsKey(arr[i]))
                                {
                                    mp_asama3[arr[i]] = mp_asama3[arr[i]] + 1;
                                }
                                else
                                {
                                    mp_asama3.Add(arr[i], 1);
                                }
                            }

                            sayi = 0;
                            foreach (KeyValuePair<string, int> word in mp_asama3.OrderByDescending(key => key.Value))
                            {


                                if (sayi == 10)
                                    break;

                                anahtar_kelimeler_asama3.Add(word.Key, word.Value);
                                sayi++;

                            }


                            foreach (var anahtarkelime in anahtar_kelimeler_asama3)
                            {

                                if (mp_asama1.ContainsKey(anahtarkelime.Key))
                                {
                                    mp_asama1[anahtarkelime.Key] = mp_asama1[anahtarkelime.Key] + 1;
                                }
                                else
                                {
                                    mp_asama1.Add(anahtarkelime.Key, 1);
                                }



                            }

                            islemler.agac[agac_sayaci].node[dugum_sayaci].anahtar_kelimeler_childs.Add(anahtar_kelimeler_asama3);




                        }//childs kodu bitiyor



                        List<Double> oranlar_node = new List<Double>();
                        foreach (var kontrol in anahtar_kelimeler_1)
                        {

                            if (mp_asama2.ContainsKey(kontrol.Key))
                            {
                                if (mp_asama2[kontrol.Key] <= kontrol.Value)
                                    oranlar_node.Add((double)mp_asama2[kontrol.Key] / (double)kontrol.Value);
                                else
                                    oranlar_node.Add((double)kontrol.Value / (double)mp_asama2[kontrol.Key]);


                            }


                        }

                        double toplam_node = 0.0;
                        double oran_2_node = 0.0;
                        double benzerlik_node = 0.0;
                        if (anaurl_kelimesayisi > tek_metin_asama2_kelimesayisi)
                            oran_2_node = (double)tek_metin_asama2_kelimesayisi / (double)anaurl_kelimesayisi;

                        if (anaurl_kelimesayisi <= tek_metin_asama2_kelimesayisi)
                            oran_2_node = (double)anaurl_kelimesayisi / (double)tek_metin_asama2_kelimesayisi;


                        for (int i = 0; i < oranlar_node.Count; i++)
                            toplam_node += (double)oranlar_node[i];
                        toplam_node = toplam_node / (double)oranlar_node.Count;

                        if (toplam_node <= oran_2_node)
                            benzerlik_node = (toplam_node * oran_2_node) * 100.0;
                        if (toplam_node > oran_2_node)
                            benzerlik_node = (oran_2_node * toplam_node) * 100.0;

                        islemler.agac[agac_sayaci].node[dugum_sayaci].benzerlik_skoru = benzerlik_node;



                    }
                    if (daha_once_kontrol_edildi == true)
                    {

                        islemler.agac[agac_sayaci].node[dugum_sayaci].anahtar_kelimeler_parent = islemler.agac[kontrol_agac].node[kontrol_node].anahtar_kelimeler_parent;
                        islemler.agac[agac_sayaci].node[dugum_sayaci].anahtar_kelimeler_childs = islemler.agac[kontrol_agac].node[kontrol_node].anahtar_kelimeler_childs;
                        islemler.agac[agac_sayaci].node[dugum_sayaci].benzerlik_skoru = islemler.agac[kontrol_agac].node[kontrol_node].benzerlik_skoru;


                        foreach (var anahtarkelime in islemler.agac[agac_sayaci].node[dugum_sayaci].anahtar_kelimeler_parent)
                        {

                            if (mp_asama1.ContainsKey(anahtarkelime.Key))
                            {
                                mp_asama1[anahtarkelime.Key] = mp_asama1[anahtarkelime.Key] + 1;
                            }
                            else
                            {
                                mp_asama1.Add(anahtarkelime.Key, 1);
                            }



                        }

                        foreach (var anahtar in islemler.agac[agac_sayaci].node[dugum_sayaci].anahtar_kelimeler_childs)
                        {

                            foreach (var anahtarkelime in anahtar)

                                if (mp_asama1.ContainsKey(anahtarkelime.Key))
                                {
                                    mp_asama1[anahtarkelime.Key] = mp_asama1[anahtarkelime.Key] + 1;
                                }
                                else
                                {
                                    mp_asama1.Add(anahtarkelime.Key, 1);
                                }



                        }


                    }

                    dugum_sayaci++;
                }
                //dugum kodu bitiyor


                for (int j = 0; j <= islemler.agac[agac_sayaci].node.Count() - 2; j++)
                {
                    for (int i = 0; i <= islemler.agac[agac_sayaci].node.Count() - 2; i++)
                    {
                        if (islemler.agac[agac_sayaci].node[i].benzerlik_skoru < islemler.agac[agac_sayaci].node[i+1].benzerlik_skoru)
                        {


                            Node temp = islemler.agac[agac_sayaci].node[i + 1];
                            islemler.agac[agac_sayaci].node[i + 1] = islemler.agac[agac_sayaci].node[i];
                            islemler.agac[agac_sayaci].node[i] = temp;


                        }
                    }
                }




                // *alt urllerin anahtar kelimeleri* de web site kümesindeki url kelime frekanslarına dahil edildikten sonra web kümesindeki urller ile anaurl
                //arasında benzerlik skoru elde edilir
                List<Double> oranlar = new List<Double>();
                foreach (var kontrol in anahtar_kelimeler_1)
                {

                    if (mp_asama1.ContainsKey(kontrol.Key))
                    {
                        if (mp_asama1[kontrol.Key] <= kontrol.Value)
                            oranlar.Add((double)mp_asama1[kontrol.Key] / (double)kontrol.Value);
                        else
                            oranlar.Add((double)kontrol.Value / (double)mp_asama1[kontrol.Key]);


                    }


                }

                double toplam = 0.0;
                double oran_2 = 0.0;
                double benzerlik = 0.0;
                if (anaurl_kelimesayisi > tek_metin_asama1_kelimesayisi)
                    oran_2 = (double)tek_metin_asama1_kelimesayisi / (double)anaurl_kelimesayisi;

                if (anaurl_kelimesayisi <= tek_metin_asama1_kelimesayisi)
                    oran_2 = (double)anaurl_kelimesayisi / (double)tek_metin_asama1_kelimesayisi;


                for (int i = 0; i < oranlar.Count; i++)
                    toplam += (double)oranlar[i];
                toplam = toplam / (double)oranlar.Count;

                if (toplam <= oran_2)
                    benzerlik = (toplam * oran_2) * 100.0;
                if (toplam > oran_2)
                    benzerlik = (oran_2 * toplam) * 100.0;

                islemler.agac[agac_sayaci].benzerlik_skoru = benzerlik;




                agac_sayaci++;


            }
            //agac kodu bitiyor



            for (int j = 0; j <= islemler.agac.Count() - 2; j++)
            {
                for (int i = 0; i <= islemler.agac.Count() - 2; i++)
                {
                    if (islemler.agac[i].benzerlik_skoru < islemler.agac[i+1].benzerlik_skoru)
                    {
                    

                        Agac temp = islemler.agac[i+1];
                        islemler.agac[i+1] = islemler.agac[i];
                        islemler.agac[i] = temp;


                    }
                }
            }



            return View(islemler);
          
        }

        public IActionResult Index5()
        {
       

            return View(new paragraf());
        }

        [HttpPost]
        public IActionResult Index5(string anaurl,string Url1)
        {
            paragraf islem = new paragraf();
            
            var appWord = new Microsoft.Office.Interop.Word.Application();
            object objLanguage = Microsoft.Office.Interop.Word.WdLanguageID.wdEnglishUS;

            /*
            Microsoft.Office.Interop.Word.SynonymInfo si = appWord.get_SynonymInfo(kelime, ref (objLanguage));
            foreach (var meaning in (si.MeaningList as Array))
            {
                islem.urller.Add(meaning.ToString());
            }
            si = appWord.get_SynonymInfo("anger", ref (objLanguage));
            foreach (var meaning in (si.MeaningList as Array))
            {
                islem.urller.Add(meaning.ToString());
            }
            */

            string tek_metin_1 = "";
            var webclient = new WebClient();
            var text = webclient.DownloadString(anaurl);
            HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
            dokuman.LoadHtml(text);
            var title = "";
            if (dokuman.DocumentNode.SelectSingleNode("html/head/title") != null)
                title = dokuman.DocumentNode.SelectSingleNode("html/head/title").InnerText;
            HtmlNodeCollection basliklar = dokuman.DocumentNode.SelectNodes("//h1");
            HtmlNodeCollection basliklar_2 = dokuman.DocumentNode.SelectNodes("//h2");
            tek_metin_1 = title + " ";
            if(basliklar!=null)
            foreach (var metin in basliklar)
                tek_metin_1 += metin.InnerText + " ";
            if (basliklar_2!= null)
                foreach (var metin in basliklar_2)
                tek_metin_1 += metin.InnerText + " ";
            HtmlNodeCollection kelimeler = dokuman.DocumentNode.SelectNodes("//p");
            if (kelimeler!= null)
                foreach (var metin in kelimeler)
                tek_metin_1 += metin.InnerText + " ";



            var replacements = new[]{
                  new{Find="don",Replace=""},
                new{Find="how",Replace=""},
                new{Find="or",Replace=""},
                new{Find="us",Replace=""},
                new{Find="what",Replace=""},
                new{Find="our",Replace=""},
                  new{Find="can",Replace=""},
                new{Find="then",Replace=""},
                new{Find="you",Replace=""},
   new{Find="the",Replace=""},
   new{Find="by",Replace=""},
   new{Find="I",Replace=""},
   new{Find="an",Replace=""},
   new{Find="In",Replace=""},
   new{Find="He",Replace=""},
   new{Find="he",Replace=""},
   new{Find="She",Replace=""},
   new{Find="has",Replace=""},
   new{Find="now",Replace=""},
   new{Find="this",Replace=""},
   new{Find="Has",Replace=""},
   new{Find="Now",Replace=""},
   new{Find="no",Replace=""},
   new{Find="she",Replace=""},
   new{Find="it",Replace=""},
   new{Find="him",Replace=""},
   new{Find="his",Replace=""},
   new{Find="her",Replace=""},
   new{Find="from",Replace=""},
   new{Find="which",Replace=""},
   new{Find="after",Replace=""},
   new{Find="is",Replace=""},
   new{Find="was",Replace=""},
   new{Find="were",Replace=""},
   new{Find="did",Replace=""},
   new{Find="when",Replace=""},
   new{Find="who",Replace=""},
   new{Find="but",Replace=""},
   new{Find="and",Replace=""},
   new{Find="to",Replace=""},
   new{Find="with",Replace=""},
   new{Find="of",Replace=""},
   new{Find="that",Replace=""},
   new{Find="s",Replace=""},
   new{Find="not",Replace=""},
   new{Find="on",Replace=""},
   new{Find="at",Replace=""},
   new{Find="as",Replace=""},
   new{Find="t",Replace=""},
   new{Find="would",Replace=""},
   new{Find="The",Replace=""},
   new{Find="for",Replace=""},
   new{Find="be",Replace=""},
   new{Find="its",Replace=""},
   new{Find="no",Replace=""},
   new{Find="been",Replace=""},
   new{Find="should",Replace=""},
    new{Find="in",Replace=""},
    new{Find="ago",Replace=""},
   new{Find="had",Replace=""},
   new{Find="just",Replace=""},
   new{Find="up",Replace=""},
   new{Find="about",Replace=""},
    new{Find="any",Replace=""},
    new{Find="have",Replace=""},

      new{Find="their",Replace=""},
     new{Find="all",Replace=""},
     new{Find="are",Replace=""},
     new{Find="they",Replace=""},
     new{Find="we",Replace=""},
       new{Find="does",Replace=""},
      new{Find="do",Replace=""},
      new{Find="will",Replace=""},


   new { Find = "bir", Replace = "" },
   new { Find = "de", Replace = "" },
   new { Find = "da", Replace = "" },
            new { Find = "ile", Replace = "" },
            new { Find = "nın", Replace = "" },
            new { Find = "bu", Replace = "" },
            new { Find = "Bu", Replace = "" },
            new { Find = "için", Replace = "" },
            new { Find = "nin", Replace = "" },
            new { Find = "ta", Replace = "" },
            new { Find = "te", Replace = "" },
            new { Find = "ün", Replace = "" },
            new { Find = "ın", Replace = "" },
            new { Find = "ye", Replace = "" },
            new { Find = "daha", Replace = "" },
            new { Find = "ancak", Replace = "" },
            new { Find = "Ancak", Replace = "" },
            new { Find = "yi", Replace = "" },
            new { Find = "dan", Replace = "" },
            new { Find = "daki", Replace = "" },
            new { Find = "na", Replace = "" },
            new { Find = "ve", Replace = "" },
            new { Find = "ve", Replace = "" },
            new { Find = "deki", Replace = "" },
            new { Find = "bir", Replace = "" },
            new { Find = "gibi", Replace = "" },
            new { Find = "bazı", Replace = "" },
             new { Find = "ise", Replace = "" },
              new { Find = "çok", Replace = "" },
               new { Find = "ye", Replace = "" },
                new { Find = "ya", Replace = "" },
                 new { Find = "e", Replace = "" },
                  new { Find = "a", Replace = "" },
                   new { Find = "den", Replace = "" },
                    new { Find = "dan", Replace = "" },
   new { Find = "de", Replace = "" },
      new { Find = "da", Replace = "" },
      new { Find = "un", Replace = "" },
      new { Find = "ın", Replace = "" },
      new { Find = "in", Replace = "" },
};

            tek_metin_1 = Regex.Replace(tek_metin_1, "[0-9]", ".");
            Boolean hatali = false;
            string tek_metin_1_duzgun = "";
            tek_metin_1 = tek_metin_1.Replace("\'", " ");
            tek_metin_1 = tek_metin_1.Replace("’", " ");
            tek_metin_1 = tek_metin_1.Replace("\"", "");
            tek_metin_1 = tek_metin_1.Replace(":", "");
            tek_metin_1 = tek_metin_1.Replace(",", "");
            tek_metin_1 = tek_metin_1.Replace(";", "");
            tek_metin_1 = tek_metin_1.Replace(".", "");
            tek_metin_1 = tek_metin_1.Replace("-", " ");
            tek_metin_1 = tek_metin_1.Replace("–", "");
            tek_metin_1 = tek_metin_1.Replace("[", "");
            tek_metin_1 = tek_metin_1.Replace("]", "");
            tek_metin_1 = tek_metin_1.Replace("(", "");
            tek_metin_1 = tek_metin_1.Replace(")", "");

            double anaurl_kelimesayisi = tek_metin_1.Split(" ").Length;

            foreach (var kelime in tek_metin_1.Split(" "))
            {

                foreach (var find in replacements)
                {

                    if (kelime.Equals(find.Find, StringComparison.InvariantCultureIgnoreCase))
                        hatali = true;

                }

                if (!kelime.Equals("") && !kelime.Equals(" ") && !hatali)
                    tek_metin_1_duzgun += kelime + " ";

                hatali = false;

            }



            string[] words = tek_metin_1_duzgun.Split(' ');
            
           List<string> arr = new List<string>(words);
            SortedDictionary<String, int> mp = new SortedDictionary<String, int>();
            SortedDictionary<String, int> anahtar_kelimeler_1 = new SortedDictionary<String, int>();
            SortedDictionary<String, int> yedekanahtarlar = new SortedDictionary<String, int>();
            // Loop to iterate over the words
            for (int i = 0; i < arr.Count; i++)
            {

                // Condition to check if the 
                // array element is present 
                // the hash-map
                if (mp.ContainsKey(arr[i]))
                {
                    mp[arr[i]] = mp[arr[i]] + 1;
                }
                else
                {
                    mp.Add(arr[i], 1);
                }
            }



            // Loop to iterate over the 
            // elements of the map
            int sayi = 0;
            foreach (KeyValuePair<string, int> word in mp.OrderByDescending(key => key.Value))
            {


                if (sayi == 10)
                    break;

                anahtar_kelimeler_1.Add(word.Key, word.Value);
                yedekanahtarlar.Add(word.Key, word.Value);
                sayi++;

            }


            
            foreach (var anahtarkelime in yedekanahtarlar)
            {

                Microsoft.Office.Interop.Word.SynonymInfo si = null;

                try
                {
                    si = appWord.get_SynonymInfo(anahtarkelime.Key, ref (objLanguage));
                }
                catch (Exception e)
                {
                    si = null;
                }
                Array esanlamlilar;
                if (si != null)
                    esanlamlilar = si.MeaningList as Array;
                else
                    esanlamlilar = null;
                if (esanlamlilar != null)
                {
                    foreach (var meaning in esanlamlilar)
                    {

                        if (arr.Contains(meaning)&& !anahtar_kelimeler_1.ContainsKey(meaning.ToString()))
                        {
                            anahtar_kelimeler_1.Add(meaning.ToString(), mp[meaning.ToString()]);
                           // islem.yedek_anahtarlar.Add(meaning.ToString(), mp[meaning.ToString()]);
                        }
                        else if(!arr.Contains(meaning)&&!anahtar_kelimeler_1.ContainsKey(meaning.ToString()))
                        {
                          
                            anahtar_kelimeler_1.Add(meaning.ToString(), anahtarkelime.Value);
                            //islem.yedek_anahtarlar.Add(meaning.ToString(), anahtarkelime.Value);
                        }

                    }

                }

            }
            islem.anahtar_kelimeler_1 = anahtar_kelimeler_1;

            int urlsayisi = 0;

            List<string> webkumesi = new List<string>();
            paragraf islemler = new paragraf();
            int k = 0;
            foreach (var url in Url1.Split(","))
            {
                islem.agac.Add(new Agac());
                islem.agac[k].root = url;
                webkumesi.Add(url);
                islemler.urller.Add(url);
                urlsayisi++;
                k++;
            }

            k = 0;


            int n = 0;

            foreach (var url1 in webkumesi)
            {
                Boolean hata_var = false;
                n = 0;
                int sinir = 0;
                var client = new WebClient();
                var htmlSource = client.DownloadString(url1);
                var doc = new HtmlDocument();
                doc.LoadHtml(htmlSource);
                List<string> urller = doc.DocumentNode.SelectNodes("//a[@href]").Select(node => node.Attributes["href"].Value).ToList();
                foreach (var url in urller)
                {
                    if (url.StartsWith(islem.agac[k].root)||url.StartsWith(islem.agac[k].root.Substring(0,10)))
                    {

                        try
                        {
                            client.DownloadString(url);


                        }
                        catch (Exception e)
                        {

                            hata_var = true;
                        }

                        if (hata_var == false)
                        {
                            islem.agac[k].node.Add(new Node());
                            islem.agac[k].node[n].parent = url;
                            islemler.urller.Add(url);
                            n++;
                            sinir++;

                        }
                    }

                    if (sinir == 3)
                        break;
                    hata_var = false;
                }

                k++;
            }

            k = 0;
            n = 0;


            foreach (var agaclar in islem.agac)
            {

                foreach (var nodes in agaclar.node)
                {
                    Boolean hata_var = false;
                    int sinir = 0;
                    var client = new WebClient();
                    var htmlSource = client.DownloadString(nodes.parent);
                    var doc = new HtmlDocument();
                    doc.LoadHtml(htmlSource);
                    List<string> urller = doc.DocumentNode.SelectNodes("//a[@href]").Select(node => node.Attributes["href"].Value).ToList();
                    foreach (var url in urller)
                    {
                        if (url.StartsWith(islem.agac[k].node[n].parent))
                        {
                            try
                            {
                                client.DownloadString(url);


                            }
                            catch (Exception e)
                            {

                                hata_var = true;
                            }

                            if (hata_var == false)
                            {
                                islem.agac[k].node[n].childs.Add(url);
                                islemler.urller.Add(url);
                                sinir++;
                            }

                            
                        }

                        if (sinir == 3)
                            break;
                        hata_var = false;
                    }
                    n++;
                }

                n = 0;
                k++;
            }

            //burası sonradan ekleniyor


            islemler.agac = islem.agac;

            int agac_sayaci = 0;

            foreach (var agac in islemler.agac)
            {


                string tek_metin_asama_1 = "";

                text = webclient.DownloadString(agac.root);
                title = "";
                dokuman.LoadHtml(text);
                if (dokuman.DocumentNode.SelectSingleNode("html/head/title") != null)
                    title = dokuman.DocumentNode.SelectSingleNode("html/head/title").InnerText;
                tek_metin_asama_1 += title;
                basliklar = dokuman.DocumentNode.SelectNodes("//h1");
                basliklar_2 = dokuman.DocumentNode.SelectNodes("//h2");
                if (basliklar != null)
                {
                    foreach (var metin in basliklar)
                        tek_metin_asama_1 += metin.InnerText + " ";
                }
                if (basliklar_2 != null)
                {
                    foreach (var metin in basliklar_2)
                        tek_metin_asama_1 += metin.InnerText + " ";
                }

                kelimeler = dokuman.DocumentNode.SelectNodes("//p");
                if (kelimeler != null)
                {
                    foreach (var metin in kelimeler)
                        tek_metin_asama_1 += metin.InnerText + " ";
                }


                tek_metin_asama_1 = Regex.Replace(tek_metin_asama_1, "[0-9]", ".");
                hatali = false;
                string tek_metin_asama_1_duzgun = "";
                tek_metin_asama_1 = tek_metin_asama_1.Replace("\'", " ");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("’", " ");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("\"", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace(":", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace(",", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace(";", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace(".", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("-", " ");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("–", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("[", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("]", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace("(", "");
                tek_metin_asama_1 = tek_metin_asama_1.Replace(")", "");

                double tek_metin_asama1_kelimesayisi = tek_metin_asama_1.Split(" ").Length;

                foreach (var kelime in tek_metin_asama_1.Split(" "))
                {


                    foreach (var find in replacements)
                    {

                        if (kelime.Equals(find.Find, StringComparison.InvariantCultureIgnoreCase))
                            hatali = true;

                    }


                    if (!kelime.Equals("") && !kelime.Equals(" ") && !hatali)
                        tek_metin_asama_1_duzgun += kelime + " ";
                    hatali = false;


                }

                SortedDictionary<String, int> mp_asama1 = new SortedDictionary<String, int>();
                SortedDictionary<String, int> anahtar_kelimeler_asama1 = new SortedDictionary<String, int>();
                SortedDictionary<String, int> alakali_anahtar_kelimeler_asama1 = new SortedDictionary<String, int>();
                string[] arr_2 = tek_metin_asama_1_duzgun.Split(' ');
                
                    for ( int i = 0; i < arr_2.Length; i++)
                {

                    // Condition to check if the 
                    // array element is present 
                    // the hash-map
                    if (mp_asama1.ContainsKey(arr_2[i]))
                    {
                        mp_asama1[arr_2[i]] = mp_asama1[arr_2[i]] + 1;
                    }
                    else
                    {
                        mp_asama1.Add(arr_2[i], 1);
                    }
                }

                sayi = 0;
                foreach (KeyValuePair<string, int> word in mp_asama1.OrderByDescending(key => key.Value))
                {


                    if (sayi == 10)
                        break;

                    anahtar_kelimeler_asama1.Add(word.Key, word.Value);
                    sayi++;

                }

                

                foreach (var anahtarkelime in anahtar_kelimeler_asama1)
                {

                    Microsoft.Office.Interop.Word.SynonymInfo si = null;

                    try
                    {
                        si = appWord.get_SynonymInfo(anahtarkelime.Key, ref (objLanguage));
                    }
                    catch (Exception e)
                    {
                        si = null;
                    }
                    Array esanlamlilar;
                    if (si != null)
                        esanlamlilar = si.MeaningList as Array;
                    else
                        esanlamlilar = null;
                    if (esanlamlilar != null)
                    {
                        foreach (var meaning in esanlamlilar)
                        {

                            if (arr_2.Contains(meaning) && !anahtar_kelimeler_asama1.ContainsKey(meaning.ToString()) && !alakali_anahtar_kelimeler_asama1.ContainsKey(meaning.ToString()))
                            {

                                alakali_anahtar_kelimeler_asama1.Add(meaning.ToString(), mp_asama1[meaning.ToString()]);
                            }
                           
                        }

                    }

                }



                islemler.agac[agac_sayaci].alakali_anahtar_kelimeler = alakali_anahtar_kelimeler_asama1;
                islemler.agac[agac_sayaci].anahtar_kelimeler = anahtar_kelimeler_asama1;


                //dugum kodu basliyor
                int dugum_sayaci = 0;


                foreach (var dugum in agac.node)
                {

                    Boolean daha_once_kontrol_edildi = false;
                    int kontrol_node = 0;
                    int kontrol_agac = 0;
                    int agac_node = 0;
                    for (int kontrol = 0; kontrol < agac_sayaci; kontrol++)
                    {

                        for (int kontrol2 = 0; kontrol2 < islem.agac[kontrol].node.Count(); kontrol2++)
                        {

                            if (islemler.agac[kontrol].node[kontrol2].parent == dugum.parent)
                            {
                                daha_once_kontrol_edildi = true;
                                kontrol_node = kontrol2;
                                kontrol_agac = kontrol;
                                break;
                            }




                        }

                    }

                    if (daha_once_kontrol_edildi == false)
                    {

                        string tek_metin_asama_2 = "";

                        text = webclient.DownloadString(dugum.parent);
                        title = "";
                        dokuman.LoadHtml(text);
                        if (dokuman.DocumentNode.SelectSingleNode("html/head/title") != null)
                            title = dokuman.DocumentNode.SelectSingleNode("html/head/title").InnerText;
                        tek_metin_asama_2 += title;
                        basliklar = dokuman.DocumentNode.SelectNodes("//h1");
                        basliklar_2 = dokuman.DocumentNode.SelectNodes("//h2");
                        if (basliklar != null)
                        {
                            foreach (var metin in basliklar)
                                tek_metin_asama_2 += metin.InnerText + " ";
                        }
                        if (basliklar_2 != null)
                        {
                            foreach (var metin in basliklar_2)
                                tek_metin_asama_2 += metin.InnerText + " ";
                        }

                        kelimeler = dokuman.DocumentNode.SelectNodes("//p");
                        if (kelimeler != null)
                        {
                            foreach (var metin in kelimeler)
                                tek_metin_asama_2 += metin.InnerText + " ";
                        }


                        tek_metin_asama_2 = Regex.Replace(tek_metin_asama_2, "[0-9]", ".");
                        hatali = false;
                        string tek_metin_asama_2_duzgun = "";
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("\'", " ");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("’", " ");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("\"", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace(":", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace(",", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace(";", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace(".", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("-", " ");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("–", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("[", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("]", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace("(", "");
                        tek_metin_asama_2 = tek_metin_asama_2.Replace(")", "");


                        double tek_metin_asama2_kelimesayisi = tek_metin_asama_2.Split(" ").Length;


                        foreach (var kelime in tek_metin_asama_2.Split(" "))
                        {


                            foreach (var find in replacements)
                            {

                                if (kelime.Equals(find.Find, StringComparison.InvariantCultureIgnoreCase))
                                    hatali = true;

                            }


                            if (!kelime.Equals("") && !kelime.Equals(" ") && !hatali)
                                tek_metin_asama_2_duzgun += kelime + " ";
                            hatali = false;


                        }

                        SortedDictionary<String, int> mp_asama2 = new SortedDictionary<String, int>();
                        SortedDictionary<String, int> anahtar_kelimeler_asama2 = new SortedDictionary<String, int>();
                        SortedDictionary<String, int> alakali_anahtar_kelimeler_asama2 = new SortedDictionary<String, int>();

                        arr_2 = tek_metin_asama_2_duzgun.Split(' ');

                        for (int i = 0; i < arr_2.Length; i++)
                        {

                            // Condition to check if the 
                            // array element is present 
                            // the hash-map
                            if (mp_asama2.ContainsKey(arr_2[i]))
                            {
                                mp_asama2[arr_2[i]] = mp_asama2[arr_2[i]] + 1;
                            }
                            else
                            {
                                mp_asama2.Add(arr_2[i], 1);
                            }
                        }



                        sayi = 0;
                        foreach (KeyValuePair<string, int> word in mp_asama2.OrderByDescending(key => key.Value))
                        {


                            if (sayi == 10)
                                break;

                            anahtar_kelimeler_asama2.Add(word.Key, word.Value);
                            sayi++;

                        }
                        

                        foreach (var anahtarkelime in anahtar_kelimeler_asama2)
                        {

                            Microsoft.Office.Interop.Word.SynonymInfo si = null;

                            try
                            {
                                si = appWord.get_SynonymInfo(anahtarkelime.Key, ref (objLanguage));
                            }
                            catch (Exception e)
                            {
                                si = null;
                            }
                            Array esanlamlilar;
                            if (si != null)
                                esanlamlilar = si.MeaningList as Array;
                            else
                                esanlamlilar = null;
                            if (esanlamlilar != null)
                            {
                                foreach (var meaning in esanlamlilar)
                                {

                                    if (arr_2.Contains(meaning) && !anahtar_kelimeler_asama2.ContainsKey(meaning.ToString())&& !alakali_anahtar_kelimeler_asama2.ContainsKey(meaning.ToString()))
                                    {

                                        alakali_anahtar_kelimeler_asama2.Add(meaning.ToString(), mp_asama2[meaning.ToString()]);
                                     
                                    }
                                 

                                }

                            }

                        }



                        islemler.agac[agac_sayaci].node[dugum_sayaci].alakali_anahtar_kelimeler_parent = alakali_anahtar_kelimeler_asama2;
                        islemler.agac[agac_sayaci].node[dugum_sayaci].anahtar_kelimeler_parent = anahtar_kelimeler_asama2;

                        foreach (var anahtarkelime in anahtar_kelimeler_asama2)
                        {

                            if (mp_asama1.ContainsKey(anahtarkelime.Key))
                            {
                                mp_asama1[anahtarkelime.Key] = mp_asama1[anahtarkelime.Key] + 1;
                            }
                            else
                            {
                                mp_asama1.Add(anahtarkelime.Key, 1);
                            }



                        }

                        foreach (var anahtarkelime in alakali_anahtar_kelimeler_asama2)
                        {

                            if (mp_asama1.ContainsKey(anahtarkelime.Key))
                            {
                                mp_asama1[anahtarkelime.Key] = mp_asama1[anahtarkelime.Key] + 1;
                            }
                            else
                            {
                                mp_asama1.Add(anahtarkelime.Key, 1);
                            }



                        }




                        //childs kodu baslıyor
                        foreach (var child in dugum.childs)
                        {



                            text = webclient.DownloadString(child);
                            string tek_metin_asama3 = "";
                            title = "";
                            dokuman.LoadHtml(text);
                            if (dokuman.DocumentNode.SelectSingleNode("html/head/title") != null)
                                title = dokuman.DocumentNode.SelectSingleNode("html/head/title").InnerText;
                            tek_metin_asama3 += title;
                            basliklar = dokuman.DocumentNode.SelectNodes("//h1");
                            basliklar_2 = dokuman.DocumentNode.SelectNodes("//h2");
                            if (basliklar != null)
                            {
                                foreach (var metin in basliklar)
                                    tek_metin_asama3 += metin.InnerText + " ";
                            }
                            if (basliklar_2 != null)
                            {
                                foreach (var metin in basliklar_2)
                                    tek_metin_asama3 += metin.InnerText + " ";
                            }

                            kelimeler = dokuman.DocumentNode.SelectNodes("//p");
                            if (kelimeler != null)
                            {
                                foreach (var metin in kelimeler)
                                    tek_metin_asama3 += metin.InnerText + " ";
                            }

                            tek_metin_asama3 = Regex.Replace(tek_metin_asama3, "[0-9]", ".");
                            hatali = false;
                            string tek_metin_asama3_duzgun = "";
                            tek_metin_asama3 = tek_metin_asama3.Replace("\'", " ");
                            tek_metin_asama3 = tek_metin_asama3.Replace("’", " ");
                            tek_metin_asama3 = tek_metin_asama3.Replace("\"", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace(":", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace(",", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace(";", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace(".", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace("-", " ");
                            tek_metin_asama3 = tek_metin_asama3.Replace("–", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace("[", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace("]", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace("(", "");
                            tek_metin_asama3 = tek_metin_asama3.Replace(")", "");


                            double tek_metin_asama3_kelimesayisi = tek_metin_asama3.Split(" ").Length;


                            foreach (var kelime in tek_metin_asama3.Split(" "))
                            {


                                foreach (var find in replacements)
                                {

                                    if (kelime.Equals(find.Find, StringComparison.InvariantCultureIgnoreCase))
                                        hatali = true;

                                }


                                if (!kelime.Equals("") && !kelime.Equals(" ") && !hatali)
                                    tek_metin_asama3_duzgun += kelime + " ";
                                hatali = false;


                            }

                            SortedDictionary<String, int> mp_asama3 = new SortedDictionary<String, int>();
                            SortedDictionary<String, int> anahtar_kelimeler_asama3 = new SortedDictionary<String, int>();
                            SortedDictionary<String, int> alakali_anahtar_kelimeler_asama3 = new SortedDictionary<String, int>();
                            arr_2 = tek_metin_asama3_duzgun.Split(' ');

                            for (int i = 0; i < arr_2.Length; i++)
                            {

                                // Condition to check if the 
                                // array element is present 
                                // the hash-map
                                if (mp_asama3.ContainsKey(arr_2[i]))
                                {
                                    mp_asama3[arr_2[i]] = mp_asama3[arr_2[i]] + 1;
                                }
                                else
                                {
                                    mp_asama3.Add(arr_2[i], 1);
                                }
                            }

                            sayi = 0;
                            foreach (KeyValuePair<string, int> word in mp_asama3.OrderByDescending(key => key.Value))
                            {


                                if (sayi == 10)
                                    break;

                                anahtar_kelimeler_asama3.Add(word.Key, word.Value);
                                sayi++;

                            }

                            

                            foreach (var anahtarkelime in anahtar_kelimeler_asama3)
                            {

                                Microsoft.Office.Interop.Word.SynonymInfo si=null;

                                try
                                {
                                   si = appWord.get_SynonymInfo(anahtarkelime.Key, ref (objLanguage));
                                }
                                catch (Exception e)
                                {
                                    si = null;
                                }
                                Array esanlamlilar;
                                if (si != null)
                                  esanlamlilar = si.MeaningList as Array;
                                else
                                     esanlamlilar = null;
                                if (esanlamlilar != null)
                                {
                                    foreach (var meaning in esanlamlilar)
                                    {

                                        if (arr_2.Contains(meaning) && !anahtar_kelimeler_asama3.ContainsKey(meaning.ToString()) && !alakali_anahtar_kelimeler_asama3.ContainsKey(meaning.ToString()))
                                        {

                                            alakali_anahtar_kelimeler_asama3.Add(meaning.ToString(), mp_asama3[meaning.ToString()]);

                                        }


                                    }

                                }

                            }


                            foreach (var anahtarkelime in anahtar_kelimeler_asama3)
                            {

                                if (mp_asama1.ContainsKey(anahtarkelime.Key))
                                {
                                    mp_asama1[anahtarkelime.Key] = mp_asama1[anahtarkelime.Key] + 1;
                                }
                                else
                                {
                                    mp_asama1.Add(anahtarkelime.Key, 1);
                                }



                            }

                            foreach (var anahtarkelime in alakali_anahtar_kelimeler_asama3)
                            {

                                if (mp_asama1.ContainsKey(anahtarkelime.Key))
                                {
                                    mp_asama1[anahtarkelime.Key] = mp_asama1[anahtarkelime.Key] + 1;
                                }
                                else
                                {
                                    mp_asama1.Add(anahtarkelime.Key, 1);
                                }



                            }



                            islemler.agac[agac_sayaci].node[dugum_sayaci].anahtar_kelimeler_childs.Add(anahtar_kelimeler_asama3);
                            islemler.agac[agac_sayaci].node[dugum_sayaci].alakali_anahtar_kelimeler_childs.Add(alakali_anahtar_kelimeler_asama3);
                            



                        }//childs kodu bitiyor



                        List<Double> oranlar_node = new List<Double>();
                        foreach (var kontrol in anahtar_kelimeler_1)
                        {

                            if (mp_asama2.ContainsKey(kontrol.Key))
                            {
                                if (mp_asama2[kontrol.Key] <= kontrol.Value)
                                    oranlar_node.Add((double)mp_asama2[kontrol.Key] / (double)kontrol.Value);
                                else
                                    oranlar_node.Add((double)kontrol.Value / (double)mp_asama2[kontrol.Key]);


                            }


                        }

                        double toplam_node = 0.0;
                        double oran_2_node = 0.0;
                        double benzerlik_node = 0.0;
                        if (anaurl_kelimesayisi > tek_metin_asama2_kelimesayisi)
                            oran_2_node = (double)tek_metin_asama2_kelimesayisi / (double)anaurl_kelimesayisi;

                        if (anaurl_kelimesayisi <= tek_metin_asama2_kelimesayisi)
                            oran_2_node = (double)anaurl_kelimesayisi / (double)tek_metin_asama2_kelimesayisi;


                        for (int i = 0; i < oranlar_node.Count; i++)
                            toplam_node += (double)oranlar_node[i];
                        toplam_node = toplam_node / (double)oranlar_node.Count;

                        if (toplam_node <= oran_2_node)
                            benzerlik_node = (toplam_node * oran_2_node) * 100.0;
                        if (toplam_node > oran_2_node)
                            benzerlik_node = (oran_2_node * toplam_node) * 100.0;

                        islemler.agac[agac_sayaci].node[dugum_sayaci].benzerlik_skoru = benzerlik_node;



                    }
                    if (daha_once_kontrol_edildi == true)
                    {

                        islemler.agac[agac_sayaci].node[dugum_sayaci].anahtar_kelimeler_parent = islemler.agac[kontrol_agac].node[kontrol_node].anahtar_kelimeler_parent;
                        islemler.agac[agac_sayaci].node[dugum_sayaci].anahtar_kelimeler_childs = islemler.agac[kontrol_agac].node[kontrol_node].anahtar_kelimeler_childs;
                        islemler.agac[agac_sayaci].node[dugum_sayaci].benzerlik_skoru = islemler.agac[kontrol_agac].node[kontrol_node].benzerlik_skoru;
                        islemler.agac[agac_sayaci].node[dugum_sayaci].alakali_anahtar_kelimeler_childs= islemler.agac[kontrol_agac].node[kontrol_node].alakali_anahtar_kelimeler_childs;

                        foreach (var anahtarkelime in islemler.agac[agac_sayaci].node[dugum_sayaci].anahtar_kelimeler_parent)
                        {

                            if (mp_asama1.ContainsKey(anahtarkelime.Key))
                            {
                                mp_asama1[anahtarkelime.Key] = mp_asama1[anahtarkelime.Key] + 1;
                            }
                            else
                            {
                                mp_asama1.Add(anahtarkelime.Key, 1);
                            }



                        }

                        foreach (var anahtar in islemler.agac[agac_sayaci].node[dugum_sayaci].anahtar_kelimeler_childs)
                        {

                            foreach (var anahtarkelime in anahtar)

                                if (mp_asama1.ContainsKey(anahtarkelime.Key))
                                {
                                    mp_asama1[anahtarkelime.Key] = mp_asama1[anahtarkelime.Key] + 1;
                                }
                                else
                                {
                                    mp_asama1.Add(anahtarkelime.Key, 1);
                                }



                        }


                    }

                    dugum_sayaci++;
                }
                //dugum kodu bitiyor

                for (int j = 0; j <= islemler.agac[agac_sayaci].node.Count() - 2; j++)
                {
                    for (int i = 0; i <= islemler.agac[agac_sayaci].node.Count() - 2; i++)
                    {
                        if (islemler.agac[agac_sayaci].node[i].benzerlik_skoru < islemler.agac[agac_sayaci].node[i + 1].benzerlik_skoru)
                        {


                            Node temp = islemler.agac[agac_sayaci].node[i + 1];
                            islemler.agac[agac_sayaci].node[i + 1] = islemler.agac[agac_sayaci].node[i];
                            islemler.agac[agac_sayaci].node[i] = temp;


                        }
                    }
                }


                List<Double> oranlar = new List<Double>();
                foreach (var kontrol in anahtar_kelimeler_1)
                {

                    if (mp_asama1.ContainsKey(kontrol.Key))
                    {
                        if (mp_asama1[kontrol.Key] <= kontrol.Value)
                            oranlar.Add((double)mp_asama1[kontrol.Key] / (double)kontrol.Value);
                        else
                            oranlar.Add((double)kontrol.Value / (double)mp_asama1[kontrol.Key]);


                    }


                }

                double toplam = 0.0;
                double oran_2 = 0.0;
                double benzerlik = 0.0;
                if (anaurl_kelimesayisi > tek_metin_asama1_kelimesayisi)
                    oran_2 = (double)tek_metin_asama1_kelimesayisi / (double)anaurl_kelimesayisi;

                if (anaurl_kelimesayisi <= tek_metin_asama1_kelimesayisi)
                    oran_2 = (double)anaurl_kelimesayisi / (double)tek_metin_asama1_kelimesayisi;


                for (int i = 0; i < oranlar.Count; i++)
                    toplam += (double)oranlar[i];
                toplam = toplam / (double)oranlar.Count;

                if (toplam <= oran_2)
                    benzerlik = (toplam * oran_2) * 100.0;
                if (toplam > oran_2)
                    benzerlik = (oran_2 * toplam) * 100.0;

                islemler.agac[agac_sayaci].benzerlik_skoru = benzerlik;




                agac_sayaci++;


            }


            for (int j = 0; j <= islemler.agac.Count() - 2; j++)
            {
                for (int i = 0; i <= islemler.agac.Count() - 2; i++)
                {
                    if (islemler.agac[i].benzerlik_skoru < islemler.agac[i + 1].benzerlik_skoru)
                    {


                        Agac temp = islemler.agac[i + 1];
                        islemler.agac[i + 1] = islemler.agac[i];
                        islemler.agac[i] = temp;


                    }
                }
            }


            return View(islemler);
          
        }


    }
}