using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyWebApplication.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(128)")]
        public string Login { get; set; }

        [Column(TypeName = "varchar(128)")]
        public string Password { get; set; }
    }
}
