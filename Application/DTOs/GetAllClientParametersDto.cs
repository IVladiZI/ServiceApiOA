using Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetAllClientParametersDto : RequestParameter
    {
        public string Name { get; set; }
        public string DocumentNumber { get; set; }
    }
}
