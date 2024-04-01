using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVotaciones.DAL;
using SysVotaciones.EN;

namespace SysVotaciones.BLL;

public class ParticipantBLL
{
    private readonly ParticipantDAL _participantDAL;

    public ParticipantBLL(string con)
    {
        _participantDAL = new ParticipantDAL(con);
    }

    public List<Participant> GetAll(int offset, int amount) => _participantDAL.GetParticipants(offset, amount);

    public int GetTotal() => _participantDAL.GetTotalParticipants();

    public Participant? GeById(int id)
    {
        Participant? participant = _participantDAL.GetParticipant(id);
        return participant;
    }

    public int Save(Participant participant)
    {
        var participantToEvalueate = new
        {
            Name = participant.Name,
            LastName = participant.LastName,
            studentCode = participant.StudentCode,
            ContestId = participant.ContestId,
        };

        bool isValid = Helper.AllPropertiesHaveValue(participantToEvalueate);
        
        if (!isValid) return 0;

        int currentId = _participantDAL.SaveParticipant(participant);

        return currentId;
    }

    public int Delete(int id)
    {
        int rowsAffected = _participantDAL.DeleteParticipant(id);
        return rowsAffected;
    }

    public int Update(Participant participant)
    {
        if (participant.Id == default) return 0;

        int rowsAffected = _participantDAL.UpdateParticipant(participant);
        return rowsAffected;
    }

    public List<Participant> Search(string keyword)
    {
        if (string.IsNullOrEmpty(keyword)) return [];

        return _participantDAL.SearchParticipants(keyword);
    }
}

