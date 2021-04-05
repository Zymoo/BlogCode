using System.Collections.Generic;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using System;
using System.Linq;

namespace Blog.DAL.Repository
{
    public class BlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository()
        {
            _context = new BlogContext();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts;
        }

        public void AddNewPost(Post newPost)
        {
            _context.Posts.Add(newPost);
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments;
        }

        public void AddNewComment(Comment newComment)
        {
            _context.Comments.Add(newComment);
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetPostComments(Post post)
        {
            return _context.Comments.Where(comment => comment.PostId == post.Id);
        }
    }
}
