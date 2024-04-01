using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVotaciones.DAL;
using SysVotaciones.EN;

namespace SysVotaciones.BLL
{
    public class CareerBLL
    {
        private readonly CareerDAL _careerDAL;

        public CareerBLL(string conn)
        {
            _careerDAL = new CareerDAL(conn);
        }

        public List<Career> GetAll(int offset, int amount) => _careerDAL.GetCareers(offset, amount);

        public int GetTotal() => _careerDAL.GetTotalCareers();

        public Career? GeById(int id)
        {
            Career? career = _careerDAL.GetCareer(id);
            return career;
        }

        public int Save(Career career)
        {
            if (career.Name == null) return 0;

            int currentId = _careerDAL.SaveCareer(career);
            return currentId;
        }

        public int Delete(int id)
        {
            int rowsAffected = _careerDAL.DeleteCareer(id);
            return rowsAffected;
        }

        public int Update(Career career)
        {
            if (career.Id == default) return 0;

            int rowsAffected = _careerDAL.UpdateCareer(career);
            return rowsAffected;
        }

        public List<Career> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword)) return [];

            return _careerDAL.SearchCareers(keyword);
        }
    }
}
