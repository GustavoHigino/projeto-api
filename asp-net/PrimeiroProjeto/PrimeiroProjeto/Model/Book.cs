using PrimeiroProjeto.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeiroProjeto.Model
{
    [Table("books")]
    public class Book : BaseEntity
    {//autor data preço titulo [author], [launch_date], [price], [title]
        //

        [Required]
        [Column("author",TypeName = "VARCHAR(MAX)")]
        public string Author {  get; set; }

        [Required]
        [Column("launch_date")]
        public DateTime Data {  get; set; }

        [Required]
        [Column("price",TypeName = "decimal(18,2)")]
        public decimal Price {  get; set; }

        [Required]
        [Column("title",TypeName = "VARCHAR(MAX)")]
        public string Title { get; set; }

    }

}
