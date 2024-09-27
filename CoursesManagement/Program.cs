using CoursesManagement.Domain;
/*
var student = new Student("Foo", "Bar", "123");

var course = new Course("OOP", 1);

course.Enroll(student);
course.Enroll(student);
course.Enroll(student);
course.Enroll(student);
course.Enroll(student);*/
/*

foreach (var s in course.Students)
{
	Console.WriteLine(s.FullName);
}
*/

var schools = new List<IApplicable>()
{
	new OnlineUniversity("ABC"),
	new School("School 123", 5),
	new OnlineUniversity("BCD")
};

for (int i = 0; i < 10; i++)
{
	var student = new Student("Foo", "Bar " + i, i + "i");

	BulkApply(schools.OfType<IApplicable>(), student);
}

PrintInfos(schools.OfType<ISchool>());


void BulkApply(IEnumerable<IApplicable> applicables, Student student)
{
	foreach (var school in schools)
	{
		school.ApplyStudent(student);
	}
}

void PrintInfos(IEnumerable<ISchool> schools)
{
	foreach (var school in schools)
	{
        Console.WriteLine($"{school.Title} -- {school.Students.Count} students.");
    }
}