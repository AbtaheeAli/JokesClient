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
                Console.WriteLine("<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>");
                Console.WriteLine("Please select a number from the following choices:");
                Console.WriteLine("(1) - Receive a random programming joke.");
                Console.WriteLine("(2) - Receive ten random programming jokes.");
                Console.WriteLine("(3) - Quit the application.");
                Console.WriteLine("<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>");

                var choice = PromptForInteger("Choice:");

                if (choice == 1)
                {
                    var responseAsStream = await client.GetStreamAsync("https://official-joke-api.appspot.com/jokes/programming/random");

                    var jokes = await JsonSerializer.DeserializeAsync<List<Joke>>(responseAsStream);

                    foreach (var joke in jokes)
                    {
                        Console.WriteLine($"The joke goes as follows: {joke.setup}");
                        Console.WriteLine($"Press ANY key to see the punchline");
                        Console.ReadKey();
                        Console.WriteLine($"{joke.punchline}");
                        Console.WriteLine("Ha! Was that not hilarious? Press ANY key to return back to the main menu to view more jokes!");
                        Console.ReadKey();
                    }
                }

                if (choice == 2)
                {
                    var responseAsStream = await client.GetStreamAsync("https://official-joke-api.appspot.com/jokes/programming/ten");

                    var jokes = await JsonSerializer.DeserializeAsync<List<Joke>>(responseAsStream);

                    foreach (var joke in jokes)
                    {
                        Console.WriteLine($"The joke goes as follows: {joke.setup} {joke.punchline}");
                    }
                }

                if (choice == 3)
                {
                    userHasQuitApp = true;
                }
            }
        }
    }
}