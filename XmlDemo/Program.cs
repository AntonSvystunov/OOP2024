// See https://aka.ms/new-console-template for more information
using System.Xml;
using System.Xml.Serialization;
using static XmlSerializerParser;

/*var parser = new XmlSerializerParser();
ParseAndPrint(parser, "books.xml");*/

var xmlSerializer = new XmlSerializer(typeof(Book));

using var textReader = File.OpenText("book.xml");

var book = (Book)xmlSerializer.Deserialize(textReader);

Console.WriteLine(book.Id);
Console.WriteLine(book.Name);
Console.WriteLine(book.AuthorName);


void ParseAndPrint(IBookParser parser, string filename)
{
    var books = parser.ReadBooks(filename).ToList();

    foreach (var book in books)
    {
        Console.WriteLine(book);
    }

    books.Add(
        new Book(Random.Shared.Next().ToString(), 
                "123" + Random.Shared.Next().ToString(), 
                "Author 1"));

    // parser.WriteBook(filename, books);
}

class DOMBookParser : IBookParser
{
    public IEnumerable<Book> ReadBooks(string filename)
    {
        var document = new XmlDocument();
        document.Load(filename);

        var bookList = document.SelectSingleNode("Books");
        if (bookList is null)
        {
            throw new Exception($"Books tag hasn't been found in the document {filename}");
        }

        var result = new List<Book>();

        foreach (XmlNode bookNode in bookList)
        {
            var bookAttributes = bookNode.Attributes;

            if (bookAttributes is null)
            {
                throw new Exception($"Book has no attributes");
            }

            var idAttribute = bookAttributes["Id"];
            if (idAttribute is null)
            {
                throw new Exception($"Book has no attribute \"Id\"");
            }

            var id = idAttribute.Value;

            var name = bookNode.SelectSingleNode("Name").InnerText;
            var authorName = bookNode.SelectSingleNode("AuthorName").InnerText;

            result.Add(new Book(id, name, authorName));
        }

        return result;
    }

    public void WriteBook(string filename, IEnumerable<Book> books)
    {
        var document = new XmlDocument();
        document.Load(filename);

        var bookList = document.SelectSingleNode("Books");
        if (bookList is null)
        {
            throw new Exception($"Books tag hasn't been found in the document {filename}");
        }

        bookList.RemoveAll();

        foreach (var book in books)
        {
            var bookNode = document.CreateElement("Book");

            var idAttribute = document.CreateAttribute("Id");
            idAttribute.InnerText = book.Id;

            bookNode.Attributes.Append(idAttribute);

            var nameNode = document.CreateElement("Name");
            nameNode.InnerText = book.Name;
            bookNode.AppendChild(nameNode);

            var authorNode = document.CreateElement("AuthorName");
            authorNode.InnerText = book.AuthorName;
            bookNode.AppendChild(authorNode);

            bookList.AppendChild(bookNode);
        }

        document.Save(filename);
    }
}


public class XmlSerializerParser : IBookParser
{
    public class BookContainer
    {
        [XmlArrayItem]
        public List<Book> Books { get; set; }
    }

    public IEnumerable<Book> ReadBooks(string filename)
    {
        var xmlSerializer = new XmlSerializer(typeof(BookContainer));

        using var textReader = File.OpenText(filename);

        var container = (BookContainer)xmlSerializer.Deserialize(textReader);

        return container.Books;
    }

    public void WriteBook(string filename, IEnumerable<Book> books)
    {
        throw new NotImplementedException();
    }
}


interface IBookParser
{
    IEnumerable<Book> ReadBooks(string filename);

    void WriteBook(string filename, IEnumerable<Book> books);
}


public class Book
{

    [XmlElement]
    public string? Id { get; set; }


    [XmlElement]
    public string? Name { get; set; }


    [XmlElement]
    public string? AuthorName { get; set; }

    public Book()
    {

    }

    public Book(string? id, string? name, string? authorName)
    {
        Id = id;
        Name = name;
        AuthorName = authorName;
    }
};