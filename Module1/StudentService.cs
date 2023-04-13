using Microsoft.EntityFrameworkCore;

namespace Module1
{
    public class StudentService : IStudentService
    {
        private readonly Context _context;
        public StudentService(Context context) 
        { 
            _context = context;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var students = await _context.Students.ToListAsync();
            return students;
        }

        public async Task<Student> CreateStudentAsync(Student user)
        {
            if(_context.Students.Any(u => u.Nick == user.Nick)) 
            {
                return new Student();
            }

            _context.Students.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
