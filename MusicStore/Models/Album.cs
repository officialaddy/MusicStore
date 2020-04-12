using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
