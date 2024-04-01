using SysVotaciones.DAL;
using SysVotaciones.EN;

namespace SysVotaciones.BLL
{
    public class YearBLL
    {
        private readonly YearDAL _yearDAL;

        public YearBLL (string conn)
        {
            _yearDAL = new YearDAL(conn);
        }

        /*public async Task<List<Year>> GetAllAsync() => await _yearDAL.GetYearsAsync();
        
        public async Task<Year?> GeById(int id)
        {
            Year year;
            try
            {
                year = await _yearDAL.GetYearAsync(id);
                return year;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<int> SaveAsync(Year year)
        {
            if (year.CareerYear == null) return 0;
            
            int rowsAffected = await _yearDAL.SaveYearAsync(year);
            return rowsAffected;
        }*/

        public List<Year> GetAll(int offset, int amount) =>  _yearDAL.GetYears(offset, amount);

        public int GetTotal() => _yearDAL.GetTotalYears();

        public Year? GeById(int id)
        {
            Year? year = _yearDAL.GetYear(id);
            return year;
        }

        public int Save(Year year)
        {
            if (year.CareerYear == null) return 0;

            int currentId = _yearDAL.SaveYear(year);
            return currentId;
        }

        public int Delete(int id) 
        { 
            int rowsAffected = _yearDAL.DeleteYear(id);
            return rowsAffected;
        }

        public int Update(Year year)
        {
            if (year.Id == default) return 0;

            int rowsAffected = _yearDAL.UpdateYear(year);
            return rowsAffected;
        }

        public List<Year> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword)) return [];

            return _yearDAL.SearchYears(keyword);
        }
    }
}
