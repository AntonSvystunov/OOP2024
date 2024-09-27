namespace CoursesManagement.Domain;

public class Course
{
	public string Title { get; }

	public CourseLevel Level { get; }

	public IReadOnlyCollection<Student> Students => students;

	private readonly List<Student> students;

	public Course(string title, CourseLevel level)
	{
		ArgumentException.ThrowIfNullOrEmpty(title);

		Title = title; 
		Level = level;

		students = new List<Student>();
	}

	public void Enroll(Student student)
	{
		ArgumentNullException.ThrowIfNull(student);

		if (students.Contains(student))
		{
            Console.WriteLine($"Student {student.FullName} already has been enrolled to course {Title}");
        }
		else
		{
			students.Add(student);
			Console.WriteLine($"Student {student.FullName} enrolled to course {Title}");
		}		
    }
}
