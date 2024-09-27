namespace CoursesManagement.Domain;

public abstract class SchoolBase
{
	protected readonly string title;

	protected readonly List<Student> students;

	public SchoolBase(string title)
	{
		this.title = title;

		students = new List<Student>();
	}

	public string Title => title;

	public IReadOnlyCollection<Student> Students => students;

	public abstract void ApplyStudent(Student student);
}
