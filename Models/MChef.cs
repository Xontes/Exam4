using Exam4.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam4.Models
{
    public class MChef : BaseEntity
    {
        public string FullName { get; set; }
        public string Designation {  get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile formFile { get; set; }
    }
}
