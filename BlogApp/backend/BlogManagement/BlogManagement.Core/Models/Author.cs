using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogManagement.Core.Models
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Bio { get; set; }
        
        public byte[] ProfileImage { get; set; }

        [NotMapped]
        public List<Category> Categories { get; set; }

        [NotMapped]
        public List<Post> Posts { get; set; }
    }
}
