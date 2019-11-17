using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Obra
    {
        public int id { get; set; }
        [StringLength(100)]
        public string autor { get; set; }
        [StringLength(100)]
        public string titulo { get; set; }
        [StringLength(1000)]
        public string ano { get; set; }
        [StringLength(100)]
        public string edicao { get; set; }
        [StringLength(100)]
        public string local { get; set; }
        [StringLength(100)]
        public string editora { get; set; }
        [StringLength(1000)]
        public string paginas { get; set; }
        [StringLength(100)]
        public string isbn { get; set; }
        [StringLength(100)]
        public string issn { get; set; }
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int cd_instituicao { get; set; }
    }
}
