using System.Data.Entity;
using Blog.DAL.Model;

namespace Blog.DAL.Infrastructure
{
    public class BlogContext : DbContext
    {
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Comment> Comments { get; set; }

        public BlogContext() : base("Blog")
        {
        }
    }
}
