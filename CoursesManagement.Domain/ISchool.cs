namespace CoursesManagement.Domain;

public interface ISchool
{
	string Title { get; }

	IReadOnlyCollection<Student> Students { get; }
}
