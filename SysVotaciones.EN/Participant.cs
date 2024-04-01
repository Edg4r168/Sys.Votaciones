namespace SysVotaciones.EN
{
    public class Participant
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? StudentCode { get; set; }
        public int ContestId { get; set; }
        public Contest? oContest { get; set; }

    }
}
