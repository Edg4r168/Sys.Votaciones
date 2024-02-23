using SysVotaciones.DAL;
using SysVotaciones.EN;

namespace SysVotaciones.BLL
{
    public class StudentBLL
    {
        private readonly StudentDAL _studentDAL;

        public StudentBLL(string con)
        {
            _studentDAL = new StudentDAL(con);
        }

        public List<Student> GetAll() => _studentDAL.GetStudents();

        public Student? GeById(string studentCode)
        {
            Student? student = _studentDAL.GetStudent(studentCode);
            return student;
        }

        public int Save(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.StudentCode)) return 0;

            var studentToEvalueate = new
            {
                studentCode = student.StudentCode,
                careerYearId = student.CareerYearId,
                careerId = student.CareerId,
                password = student.Password
            };

            bool isValid = Helper.AllPropertiesHaveValue(studentToEvalueate);
            Console.WriteLine(isValid);
            if (!isValid) return 0;

            int rowsAffected = _studentDAL.SaveStudent(student);
            
            return rowsAffected;
        }

        public LoginResult Login(User user)
        {
            var response = new LoginResult();

            bool isValid = Helper.AllPropertiesHaveValue(user);
            if (!isValid) return response;

            bool existsStudent = _studentDAL.Login(user);
            if (!existsStudent) return response;

            string token = Helper.GenerateToken(user.StudentCode, "user");

            response.Logged = true;
            response.Token = token;

            return response;
        }
  

        public int Delete(string studentCode)
        {
            int rowsAffected = _studentDAL.DeleteStudent(studentCode);
            return rowsAffected;
        }

        public int Update(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.StudentCode)) return 0;

            int rowsAffected = _studentDAL.UpdateStudent(student);
            return rowsAffected;
        }
    }
}
