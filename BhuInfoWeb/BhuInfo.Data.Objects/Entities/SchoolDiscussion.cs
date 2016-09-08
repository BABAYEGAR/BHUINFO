using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhuInfo.Data.Objects.Entities
{
    public class SchoolDiscussion
    {
        public long SchoolDiscussionId { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        public string Topic { get; set; }
        [Required(ErrorMessage = "This field is compulsory")]
        public string Content { get; set; }
        public string Status { get; set; }
        public long CreatedById { get; set; }
        public long LastModifiedById { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public int DiscussionView { get; set; }
        public virtual ICollection<SchoolDiscussionComment> SchoolDiscussionComments { get; set; }
    }
}
