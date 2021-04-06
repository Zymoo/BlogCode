using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using Blog.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using TDD.DbTestHelpers.Core;
using System.Data.Entity.Validation;

namespace Blog.DAL.Tests
{
    [TestClass]
    public class RepositoryTests : DbBaseTest<BlogFixtures>
    {

        [TestInitialize]
        public void TestSetUp()
        {
            BaseSetUp();
        }

        [TestCleanup]
        public void CleanUp()
        {
            BaseTearDown();
        }

        [TestMethod]
        public void GetAllPost_OnePostInDb_ReturnsOnePost()
        {
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();
            context.Posts.ToList().ForEach(x => context.Posts.Remove(x));
            context.Posts.Add(new Post
            {
                Author = "test",
                Content = "test, test, test..."
            });
            context.SaveChanges();

            var result = repository.GetAllPosts();

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetAllPost_TwoPostsInDb_ReturnsTwoPosts()
        {
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();

            var result = repository.GetAllPosts();

            Assert.AreEqual(2, result.Count());
        }


        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddPost_NoAuthorGiven_ThrowsException()
        {
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();

            repository.AddNewPost(new Post { Id = 111, Content = "No author content" });
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddPost_NoContentGiven_ThrowsException()
        {
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();

            repository.AddNewPost(new Post { Id = 222, Author = "No content author" });
        }

        [TestMethod]
        public void GetAllComments_TwoCommentsInDb_ReturnsTwoComments()
        {
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();

            var comments = repository.GetAllComments();
          
            Assert.AreEqual(comments.Count(), 2);
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddComment_NoContentGiven_ThrowsException()
        {
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();
            var allPosts = repository.GetAllPosts();
            var id = allPosts.ElementAt(0).Id;

            repository.AddNewComment(new Comment { Id = 222, PostId = id });
        }

        [TestMethod]
        public void GetAllPostComments_AddNewComment_OneMorePostCommentReturned()
        {
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();
            var allPosts = repository.GetAllPosts();

            var chosenPost = allPosts.ElementAt(0);
            var commentsCount = repository.GetPostComments(chosenPost).Count();
            repository.AddNewComment(new Comment() { Post = chosenPost, Content = "Diamonds Are Forever" });
            var commentsCountAfter = repository.GetPostComments(chosenPost).Count();

            Assert.AreEqual(commentsCount + 1, commentsCountAfter);
        }

        [TestMethod]
        public void GetAllPostComments_AddNewCommentForOtherPost_SameNumberOfCommentsReturned()
        {
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();
            var allPosts = repository.GetAllPosts(); 

            var chosenPost = allPosts.ElementAt(0);
            var otherPost = allPosts.ElementAt(1);
            var commentsCount = repository.GetPostComments(chosenPost).Count();
            repository.AddNewComment(new Comment() { Post = otherPost, Content = "Tomorrow never dies" });
            var commentsCountAfter = repository.GetPostComments(chosenPost).Count();

            Assert.AreEqual(commentsCount + 2, commentsCountAfter);
        }

    }
}
