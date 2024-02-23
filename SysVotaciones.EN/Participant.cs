using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysVotaciones.EN
{
    internal class Participant
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? StudentCode { get; set; }
        public Category? oCategory { get; set; }

    }
}
