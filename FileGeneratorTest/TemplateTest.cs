using FileGenerator;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;

namespace FileGeneratorTest
{
    [TestFixture]
    public class TemplateTest
    {
        private ServiceProvider _ServiceProvider { get; set; }

        [SetUp]
        public void Setup()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped(typeof(ITemplate<>), typeof(TemplateHandler<>));
            _ServiceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void Must_Mount_Header()
        {            
            ITemplate<Book> templateService = _ServiceProvider.GetService(typeof(ITemplate<Book>)) as ITemplate<Book>;
            
            string content = templateService.GetHeader();
            
            Assert.AreEqual("Author;Price", content);
        }

        [Test]
        public void Must_Mount_OneLine()
        {
            Book book = new Book("Malta", 19.99M);
            ITemplate<Book> templateService = _ServiceProvider.GetService(typeof(ITemplate<Book>)) as ITemplate<Book>;

            string line = templateService.GetContent(book);

            Assert.AreEqual("Malta;19.99", line);
        }
        
        [Test]
        public void Must_Mount_TwoLines()
        {
            IEnumerable<Book> books = new[]
            {
                new Book("Author_01", 13.75M),
                new Book("Author_02", 7.19M)
            };
            ITemplate<Book> templateService = _ServiceProvider.GetService(typeof(ITemplate<Book>)) as ITemplate<Book>;

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