using System;
using static System.Console;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace elering
{
    class Root
    {
        public class Ee
        {
            public int Timestamp { get; set; }
            public double Price { get; set; }
        }
        public class Lt
        {
            public int Timestamp { get; set; }
            public double Price { get; set; }
        }
        public class Lv
        {
            public int Timestamp { get; set; }
            public double Price { get; set; }
        }
        public class Fi
        {
            public int Timestamp { get; set; }
            public double Price { get; set; }
        }

        public class Data
        {
            public List<Ee> Ee { get; set; }
        }

        public class Status
        {
            public bool Success { get; set; }
        }
        public class RootObject
        {
            public Data Data { get; set; }
        }
    }
    class Program
    {
        static void Main()
        {
            OutputEncoding = System.Text.Encoding.UTF8;
            string apiUrl = "https://dashboard.elering.ee/api/nps/price";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "GET";
            //request.UserAgent = "*";
            //request.Accept = "*/*";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using var responseStream = new StreamReader(webStream);
            var response = responseStream.ReadToEnd();

            Root.RootObject data = JsonConvert.DeserializeObject<Root.RootObject>(response);

            int ts;
            int count = 0;
            WriteLine("Prices today: \n");
            for (int i = 0; i < data.Data.Ee.Count; i++)
            {
                ts = data.Data.Ee[i].Timestamp;
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ts).ToLocalTime();
                string formattedDate = dt.ToString("dd.MM.yyyy HH.mm");

                WriteLine(formattedDate + " - " + data.Data.Ee[i].Price + " €/MWh");
                count++;
            }

            if (data.Data.Ee[count-1].Price <= 19.50)
            {
                WriteLine("\nIt is good time to turn on high power electrical applications");
            }
        }
    }
}
