using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Model
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Content { get; set; }

        public long PostId { get; set; }
        public Post Post { get; set; }
    }
}
