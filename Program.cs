using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JokesClient
{
    class Program
    {
        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                return 0;
            }
        }
        class Joke
        {
            public int id { get; set; }
            public string type { get; set; }
            public string setup { get; set; }
            public string punchline { get; set; }
        }
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var userHasQuitApp = false;

            while (userHasQuitApp == false)
            {
                Console.WriteLine("Please select a number from the following choices:");
                Console.WriteLine("(1) - Receive a random programming joke.");
                Console.WriteLine("(2) - Receive ten random jokes.");
                Console.WriteLine("(3) - Quit the application.");

                var choice = PromptForInteger("Choice:");

                if (choice == 1)
                {
                    var responseAsStream = await client.GetStreamAsync("https://official-joke-api.appspot.com/jokes/programming/random");

                    var jokes = await JsonSerializer.DeserializeAsync<List<Joke>>(responseAsStream);

                    foreach (var joke in jokes)
                    {
                        Console.WriteLine($"This is a {joke.type} joke. The joke goes as follows: {joke.setup} {joke.punchline}");
                    }
                }



            }


        }
    }
}