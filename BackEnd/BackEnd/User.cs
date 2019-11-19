using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Models;

namespace BackEnd
{
    public class User
    {
        public String Login { get; set; }
        public String Senha { get; set; }
    }
}
