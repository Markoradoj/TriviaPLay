using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace TriviaPLay
{
    [Serializable]
    public class Question
    {

        [JsonProperty("question")]
        public string question { get; set; }

        [JsonProperty("difficulty")]
        public string difficulty { get; set; }

        [JsonProperty("correct_answer")]
        public string CorrectAnswer { get; set; }

        [JsonProperty("incorrect_answer")]
        public string IncorrectAnswers { get; set; }

    }

    public class CategoryResponse
    {
        [JsonProperty("trivia_categories")]
        public List<Category> TriviaCategories { get; set; }
    }

    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}