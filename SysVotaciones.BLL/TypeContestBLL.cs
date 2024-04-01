using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVotaciones.DAL;
using SysVotaciones.EN;

namespace SysVotaciones.BLL
{
    public class TypeContestBLL
    {
        private readonly TypeContestDAL _typeContestDAL;

        public TypeContestBLL(string conn)
        {
            _typeContestDAL = new TypeContestDAL(conn);
        }

        public List<TypeContest> GetAll(int offset, int amount) => _typeContestDAL.GetTypesContests(offset, amount);

        public int GetTotal() => _typeContestDAL.GetTotalTypesContests();

        public TypeContest? GeById(int id)
        {
            TypeContest? typeContest = _typeContestDAL.GetTypeContest(id);
            return typeContest;
        }

        public int Save(TypeContest typeContest)
        {
            if (typeContest.Name == null) return 0;

            int currentId = _typeContestDAL.SaveTypeContest(typeContest);
            return currentId;
        }

        public int Delete(int id)
        {
            int rowsAffected = _typeContestDAL.DeleteTypeContest(id);
            return rowsAffected;
        }

        public int Update(TypeContest typeContest)
        {
            if (typeContest.Id == default) return 0;

            int rowsAffected = _typeContestDAL.UpdateTypeContest(typeContest);
            return rowsAffected;
        }

        public List<TypeContest> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword)) return [];

            return _typeContestDAL.SearchTypesContests(keyword);
        }
    }
}
