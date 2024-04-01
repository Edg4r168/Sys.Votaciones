using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysVotaciones.EN
{
    public class Contest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int State { get; set; }
        public int TypeContestId { get; set; }
        public TypeContest? oTypeContest { get; set; }
    }
}
