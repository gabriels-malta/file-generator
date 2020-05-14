using FileGenerator;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;

namespace FileGeneratorTest
{
    [TestFixture]
    public class TemplateTest
    {
        private ITemplate<Book> templateService { get; set; }

        [SetUp]
        public void Setup()
        {
            ServiceProvider _ServiceProvider =
                new ServiceCollection()
                .AddScoped(typeof(ITemplate<>), typeof(TemplateHandler<>))
                .BuildServiceProvider();

            templateService = _ServiceProvider.GetService(typeof(ITemplate<Book>)) as ITemplate<Book>;
        }

        [Test]
        public void Must_Mount_Header()
        {
            string content = templateService.GetHeader();

            Assert.AreEqual("Author;Price", content);
        }

        [Test]
        public void Must_Mount_OneLine()
        {
            Book book = new Book("Malta", 19.99M);

            string line = templateService.GetContent(book);

            Assert.AreEqual("Malta;19.99", line);
        }

        [Test]
        public void Must_Mount_EntireCotent()
        {
            IEnumerable<Book> books = new[]
            {
                new Book("Author_01", 13.75M),
                new Book("Author_02", 7.19M)
            };

            string content = templateService.GetContent(books);

            Assert.AreEqual($"Author;Price{"\r\n"}Author_01;13.75{"\r\n"}Author_02;7.19{"\r\n"}", content);
        }
    }


    internal class Book
    {
        public Book(string author, decimal price)
        {
            Author = author;
            Price = price;
        }

        public string Author { get; private set; }
        public decimal Price { get; private set; }
    }
}