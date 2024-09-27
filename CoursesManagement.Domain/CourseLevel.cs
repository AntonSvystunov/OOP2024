namespace CoursesManagement.Domain;

public record class CourseLevel(string Name, int SortOrder = 0)
{
	public static CourseLevel Beginner => new("Beginner");

	public static CourseLevel Intermediate => new("Intermediate", 1);

	public static CourseLevel Senior => new("Senior", 2);
}
