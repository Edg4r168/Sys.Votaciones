using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVotaciones.DAL;
using SysVotaciones.EN;

namespace SysVotaciones.BLL
{
    public class CategoryBLL
    {
        private readonly CategoryDAL _categoryDAL;

        public CategoryBLL (string conn)
        {
            _categoryDAL = new CategoryDAL(conn);
        }

        public List<Category> GetAll() => _categoryDAL.GetCategories();

        public Category? GeById(int id)
        {
            Category? category = _categoryDAL.GetCategory(id);
            return category;
        }

        public int Save(Category category)
        {
            if (category.Name == null) return 0;

            int rowsAffected = _categoryDAL.SaveCategory(category);
            return rowsAffected;
        }

        public int Delete(int id)
        {
            int rowsAffected = _categoryDAL.DeleteCategory(id);
            return rowsAffected;
        }

        public int Update(Category  category)
        {
            if (category.Id == default) return 0;

            int rowsAffected = _categoryDAL.UpdateCategory(category);
            return rowsAffected;
        }
    }
}
