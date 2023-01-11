using rMakev2.DTOs;
using rMakev2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rTests.rMakev2
{
    [TestClass]
    public class PublishedDocumentTests
    {
        [TestMethod]
        public void Create_PublishedDocument_OK() { 
            //Arrange 
            Document document = new Document();
            document.Id = Guid.NewGuid().ToString();
            document.Name = "Test Doc";
            document.Order = 1;

            document.ProjectId = Guid.NewGuid().ToString();

            Element element1 = new Element(document);
            element1.Content = "This is";

            Element element2 = new Element(document);
            element2.Content = "a";

            Element element3 = new Element(document);
            element3.Content = "Test";

            //Act
            PublishDocument publish = new PublishDocument(document);

            //Assert
            Assert.AreEqual(document.Name, publish.Name);
            Assert.AreEqual(document.ProjectId, publish.ProjectId);
            Assert.AreEqual(DateTime.Now.Date, publish.PublicationDate);
            Assert.AreEqual(1, publish.Order);
            Assert.AreEqual("This is\na\nTest", publish.Content);

            Assert.IsTrue(publish.Authors.Count == 1 && publish.Owners.Count == 1);
            Assert.IsTrue(publish.Authors.First().Name == "TBD");
            Assert.IsTrue(publish.Owners.First().Name == "TBD");
        }

        [TestMethod]
        public void Create_PublishedDocument_RespectsElementOrder()
        {
            //Arrange 
            Document document = new Document();
            document.Id = Guid.NewGuid().ToString();
            document.Name = "Test Doc";
            document.Order = 1;

            document.ProjectId = Guid.NewGuid().ToString();

            Element element1 = new Element(document);
            element1.Content = "This is";
            element1.Order = 1;

            Element element2 = new Element(document);
            element2.Content = "Test";
            element2.Order = 3;

            Element element3 = new Element(document);
            element3.Content = "a";
            element3.Order = 2;

            //Act
            PublishDocument publish = new PublishDocument(document);

            //Assert
            Assert.AreEqual(document.Name, publish.Name);
            Assert.AreEqual(document.ProjectId, publish.ProjectId);
            Assert.AreEqual(DateTime.Now.Date, publish.PublicationDate);
            Assert.AreEqual(1, publish.Order);
            Assert.AreEqual("This is\na\nTest", publish.Content);

            Assert.IsTrue(publish.Authors.Count == 1 && publish.Owners.Count == 1);
            Assert.IsTrue(publish.Authors.First().Name == "TBD");
            Assert.IsTrue(publish.Owners.First().Name == "TBD");
        }

        [TestMethod]
        public void Create_PublishedDocument_ContentIsNull() {
            //Arrange 
            Document document = new Document();
            document.Id = Guid.NewGuid().ToString();
            document.Name = "Test Doc";
            document.Order = 1;

            document.ProjectId = Guid.NewGuid().ToString();

            Element element = new Element(document);
            element.Content = null;

            //Act
            PublishDocument publish = new PublishDocument(document);

            //Assert
            Assert.AreEqual(document.Name, publish.Name);
            Assert.AreEqual(document.ProjectId, publish.ProjectId);
            Assert.AreEqual(DateTime.Now.Date, publish.PublicationDate);
            Assert.AreEqual(1, publish.Order);
            Assert.AreEqual(string.Empty, publish.Content);

            Assert.IsTrue(publish.Authors.Count == 1 && publish.Owners.Count == 1);
            Assert.IsTrue(publish.Authors.First().Name == "TBD");
            Assert.IsTrue(publish.Owners.First().Name == "TBD");
        }
    }
}
