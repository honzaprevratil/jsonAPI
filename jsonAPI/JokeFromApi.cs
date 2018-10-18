using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsonAPI
{
    [DelimitedRecord(";")]
    public class JokeFromApi
    {
        public int id { get; set; }
        public string joke { get; set; }
        public DateTime date { get; set; }
        public string[] categories { get; set; }
    }
}
