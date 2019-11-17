using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Instituicao
    {
        public int id { get; set; }
        [StringLength(100)]
        public string nome { get; set; }
        [StringLength(100)]
        public string entidade { get; set; }
    }
}
