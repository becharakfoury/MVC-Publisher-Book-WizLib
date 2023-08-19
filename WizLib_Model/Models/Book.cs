using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Model.Models
{
   public class Book
    {
        [Key]
        public int Book_Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(15)]
        public string ISBN { get; set; }
        [Required]
        public double Price { get; set; }


        [ForeignKey("BookDetail")]
        public int? BookDetail_id { get; set; }
        public BookDetail BookDetail { get; set; }


        [ForeignKey("Publisher")]
        public int? Publisher_id { get; set; }
        public Publisher Publisher { get; set; }
               

        public ICollection<BookAuthor> BookAuthors { get; set; }

        /*
        [ForeignKey("Category")]
        public int Category_id { get; set; }

        public Category Category { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string PriceRage { get; set; }*/



    }
}
