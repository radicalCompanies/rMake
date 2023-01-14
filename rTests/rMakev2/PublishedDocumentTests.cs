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
        public void Create_PublishedDocument_Ok() {
            //Arrange
            App app = new App(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            Data data = new Data(app);
            Project project = new Project(data);

            Document document = new Document(project);
            document.Elements.First().Content = "This";
            document.AddElement(document).Content = "Is a";
            document.AddElement(document).Content = "Test";

            PublishProject publishProject = new PublishProject(project);

            //Act
            PublishDocument publishDocument = new PublishDocument(publishProject, document);

            //Assert
            Assert.AreEqual(publishProject.Id, publishDocument.ProjectId);
            Assert.AreEqual(document.Name, publishDocument.Name);
            Assert.AreEqual(document.Order, publishDocument.Order);
            Assert.AreEqual("html", publishDocument.ContentType);

            Assert.AreEqual("<section>This</section><section>Is a</section><section>Test</section>", publishDocument.Content);
        }

        [TestMethod]
        public void Create_PublishedDocument_RespectOrder() {
            //Arrange
            App app = new App(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            Data data = new Data(app);
            Project project = new Project(data);

            PublishProject publishProject = new PublishProject(project);

            Document document = new Document(project);

            Element Element1 = document.Elements.First();
            Element Element2 = document.AddElement(document);
            Element Element3 = document.AddElement(document);

            Element1.Content = "Is a";
            Element1.Order = 2;

            Element2.Content = "This";
            Element2.Order = 1;

            Element3.Content = "Test";
            Element3.Order = 3;

            //Act
            PublishDocument publishDocument = new PublishDocument(publishProject, document);

            //Assert
            Assert.AreEqual("<section>This</section><section>Is a</section><section>Test</section>", publishDocument.Content);
        }

        [TestMethod]
        public void Create_PublishedDocument_ElementIsEmpty() {
            //Arrange
            App app = new App(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            Data data = new Data(app);
            Project project = new Project(data);

            PublishProject publishProject = new PublishProject(project);

            Document document = new Document(project);

            //Act
            PublishDocument publishDocument = new PublishDocument(publishProject, document);

            //Assert
            Assert.AreEqual(String.Empty, publishDocument.Content);
        }


    }
}
