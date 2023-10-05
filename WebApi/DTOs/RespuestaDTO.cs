using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class RespuestaDTO
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object result { get; set; }
    }
}