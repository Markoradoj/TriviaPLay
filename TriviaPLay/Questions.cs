using System;
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
        public string Category { get; set; }


    }
}
