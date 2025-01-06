﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace TriviaPLay
{
    [Serializable]
    public class Questions
    {
        [JsonProperty("category")]
        public string category { get; set; }

        [JsonProperty("question")]
        public string question {  get; set; }

        [JsonProperty("difficulty")]
        public string difficulty { get; set; }

        [JsonProperty("correct_answer")]
        public string CorrectAnswer { get; set; }

        [JsonProperty("incorrect_answer")]
        public string IncorrectAnswers { get; set; }

    }
}
