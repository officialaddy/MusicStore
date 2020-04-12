using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
