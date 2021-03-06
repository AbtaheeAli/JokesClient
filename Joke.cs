using System;
using System.Text.Json.Serialization;


namespace JokesClient
{
    class Joke
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("setup")]
        public string SetUp { get; set; }

        [JsonPropertyName("punchline")]
        public string PunchLine { get; set; }
    }
}