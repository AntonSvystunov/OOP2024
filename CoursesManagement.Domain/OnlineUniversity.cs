
namespace CoursesManagement.Domain;

public class OnlineUniversity : ISchool, IApplicable
{
	protected readonly List<Student> students;

	public OnlineUniversity(string title)
	{
		Title = title;

		students = new();
	}

	public string Title { get; }

	public IReadOnlyCollection<Student> Students => students;

	public void ApplyStudent(Student student)
	{
		if (!students.Contains(student))
		{
			students.Add(student);
			Console.WriteLine($"{student.FullName} applied to online university {Title}");
		}
	}
}
