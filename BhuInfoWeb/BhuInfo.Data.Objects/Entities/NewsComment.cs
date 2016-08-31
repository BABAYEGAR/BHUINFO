using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhuInfo.Data.Objects.Entities
{
    public class NewsComment
    {
        public int NewsCommentId { get; set; }
        public string CommentBy { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int NewsId { get; set; }
        [ForeignKey("NewsId")]
        public virtual News News { get; set; }
        public  DateTime DateCreated { get; set; }

    }
}
