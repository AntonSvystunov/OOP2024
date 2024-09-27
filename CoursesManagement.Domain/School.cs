
namespace CoursesManagement.Domain;

public class School : SchoolBase, IApplicable, ISchool
{
	public int Capacity { get; }

	public School(string title, int capacity) : base(title)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(capacity);

		Capacity = capacity;
	}

	public override void ApplyStudent(Student student)
	{
		ArgumentNullException.ThrowIfNull(student);

		if (students.Count >= Capacity)
		{
            Console.WriteLine($"School {title} cannot apply students anymore");
			return;
        }

		if (!students.Contains(student))
		{
			students.Add(student);
			Console.WriteLine($"{student.FullName} applied to school {title}");
		}		
    }
}
