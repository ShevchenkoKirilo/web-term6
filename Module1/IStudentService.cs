namespace Module1
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> CreateStudentAsync(Student student);
    }
}
