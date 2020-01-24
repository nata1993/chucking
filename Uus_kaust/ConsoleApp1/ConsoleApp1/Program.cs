using System;
using System.IO;
using System.Net;
using static System.Console;
using System.Collections.Generic;
using Newtonsoft.Json;

//name, repos, followers, link

namespace ConsoleApp1
{
    class Root
    {
        public string Name { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public string Html_url { get; set; }
    }
    class Program
    {
        static void Main()
        {
            string apiUrl = "https://api.github.com/users/nata1993?client_id=$bba931d5d676e18aa68b&client_secret=$a0411944c8ef739affd2a028f46e93ebf12ba95b";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "GET";
            request.UserAgent = "*";
            //request.Accept = "*/*";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using var responseStream = new StreamReader(webStream);
            var response = responseStream.ReadToEnd();

            Root githubDetails = JsonConvert.DeserializeObject<Root>(response);
            WriteLine(githubDetails.Name);
            WriteLine(githubDetails.Html_url);
            WriteLine(githubDetails.Followers);
            WriteLine(githubDetails.Following);
        }
    }
}
