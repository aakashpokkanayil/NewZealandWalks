using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewZealandWalks.Models.Domain
{
    public class Image
    {
        public int Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public string Extension { get; set; }
        public long sizeinbytes { get; set; }
        public string path { get; set; }
    }
}
