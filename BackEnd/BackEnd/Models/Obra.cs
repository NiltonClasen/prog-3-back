using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Obra
    {
        public int id { get; set; }
        public string autor { get; set; }
        public string titulo { get; set; }
        public string ano { get; set; }
        public string edicao { get; set; }
        public string local { get; set; }
        public string editora { get; set; }
        public string paginas { get; set; }
        public string isbn { get; set; }
        public string issn { get; set; }
    }
}
