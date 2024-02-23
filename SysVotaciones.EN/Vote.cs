namespace SysVotaciones.EN
{
    internal class Vote
    {
        public int Id { get; set; }
        public Student? oStudent { get; set; }
        public Participant? oParticipant { get; set; }

    }
}
