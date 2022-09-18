using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace HttpGetPost
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var client = new HttpClient())
            {
                //var endpoint = new Uri("https://jsonplaceholder.typicode.com/posts");
                var endpoint = new Uri("http://localhost:52465/api/products");


                //get
                var result1 = client.GetAsync(endpoint).Result;
                var json = result1.Content.ReadAsStringAsync().Result;

                //post
                var newPost = new Post()
                {
                    Title = "Test Post",
                    Body = "Hello World!",
                    UserId = 20
                };

                var newPostJson = JsonConvert.SerializeObject(newPost);
                var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;



            }
        }
    }
}
