using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Product
{
    public class Photos
    {
        [Key]
        public int PhotoId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [StringLength(255)]
        public string PhotoName { get; set; }

        public virtual Product Product { get; set; }


        public Photos(string path)
        {
            PhotoName = path;
        }

        private Photos() { }
    }
}