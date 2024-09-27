namespace CoursesManagement.Domain;

public class Student
{
	public string FirstName { get; }

	public string LastName { get; }

	public string Id { get; }

	public object FullName => FirstName + " " + LastName;


	public Student(string firstName, string lastName, string id)
	{
		FirstName = firstName;
		LastName = lastName;
		Id = id;
	}
}
