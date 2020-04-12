using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MusicStore.Models
{
    public enum SongType
    {
        Pop,
        Rock,
        Country,
        Jazz,
        HeaveyMetal,
        HipHop,
        Rap
    }
    public class Song
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public SongType Type { get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
        public string Link { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
